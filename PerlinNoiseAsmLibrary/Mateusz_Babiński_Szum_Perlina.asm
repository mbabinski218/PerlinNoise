; Temat:  Szum Perlina
; Opis:	  Algorytm generuje wartość szumu Perlina dla konkretnego piksela
; Autor:  Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
; Wersja: 0.1

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.data?
	seed DWORD ?

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.const
	vector11	   REAL4 1.0, 1.0		    ; vector2d z x = 1, y = 1
	w			   DWORD 8 * sizeof DWORD	; 8 * sizeof(int)
	s			   DWORD 4 * sizeof DWORD	; w / 2 - szerokość obrotu
	pi			   REAL8 3.141592653589793  ; pi
	intMaxPlusOne  REAL8 2147483648.0       ; maksymalna wartość integera + 1

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.code

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; Vector2 RandomGradient(float value1, float value2)
; 
;Funkcja oblicza losowy wektor jednostkowy - cały obraz składu się z wielu pikseli a te w zależności 
;od wartości otrzymanego wersora będą rosły lub malały. 
;Wartość pierwsza znajduje się w rejestrze eax a druga w ecx.
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
RandomGradient PROC
	local temp : REAL4  ; zmienna lokalna używana w późniejszej fazie do operacji na rejestrach zmiennoprzecinkowych
	local ws : DWORD

	mul  seed	   ; teraz w eax będzie znajdować się wartość pierwsza pomnożona przez seed, dzięki czemu szum będzie wyglądał za każdym razem inaczej
	mov  edx, eax  
	push rax	      
	push rcx	   
	
	; wykonanie szeregu obliczeń według wzoru, aby otrzymać losową wartość, z której będzie liczony wektor jednostkowy
	mov ecx, s    
	shl eax, cl   ; eax << s
	mov ecx, s
	shr edx, cl   ; edx >> (w - s)
	or  edx, eax  ; edx = edx | eax
	pop rax		  ; pobranie wartości drugiego parametru
	xor eax, edx  
	mul seed	  ; pomnożenie otrzymanej wartości przez seed, w celu zwiększenia losowości końcowego wektora
	mov ecx, s
	shl eax, cl   ; eax << s
	mov ecx, s
	shr edx, cl   ; edx >> (w - s)
	or  edx, eax  ; edx = edx | eax
	pop rax		  ; pobranie pierwszego wartości, która była wcześniej pomnożona przez wartość seeda
	xor eax, edx  
	mul seed
	movsd    xmm15, pi
	movsd    xmm0, intMaxPlusOne 	
	divsd    xmm15, xmm0		  ; w dolnych 64 bitach xmm15 znajduje się wartość pi/2147483648.0
	cvtsi2sd xmm0, eax			  ; wpisanie w dolne 64 bity xmm0 wartości z eax
	mulsd	 xmm15, xmm0		  ; pomnożenie pierwszych 64 bitów xmm15 przez dolne 64 bity xmm0 oraz zapisanie wyniku w xmm15
	cvtsd2ss xmm15, xmm15		  ; konwersja pierwszych 64 bitów xmm15 na wartość zmiennoprzecinkową pojedyńczej precyzji i wpisanie jej w pierwsze 32 bity rejestru xmm15

	movd   temp, xmm15 
	fld    temp			; ustawienie wartości st(0) na wyliczoną losową długość
	fcos	            ; obliczenia cosinusa z wartości w st(0), aby otrzymać liczbę w przedziale od -1 do 1
	fstp   temp	        ; pobranie wartości z st(0)	i przypisanie jej do zmiennej temp
	movss  xmm0, temp   ; wpisanie pobranej wartości pod pierwsze 32 bity w xmm0
	pslldq xmm0, 4      ; przesunięcie bitowe w lewo, aby otrzymana wartość znalazła się jak druga (czyli w xmm0[32-63])
	
	movd  temp, xmm15 
	fld   temp		  ; ustawienie wartości st(0) na wyliczoną losową długość
	fsin			  ; tym razem obliczanie wartości przy pomocy sinusa, aby otrzymać ponownie liczbę w przedziale od -1 do 1, ale inną niż przy pomocy cosinusa
	fstp  temp		  ; pobranie wartości z st(0) i przypisanie jej do zmiennej temp

	; wpisanie najpierw wyliczonej wartości pod dolne 32 bity w xmm15 a następnie z xmm15 wpisanie tej liczby do xmm0, ponieważ przy użyciu instrukcji
	; movss xmm0, temp wartość, która została tam wcześniej przypisana pod bity [32-63] była czyszczona
	movss xmm15, temp 
	movss xmm0, xmm15 

	ret ; losowy wersor zwracany jest w xmm0
RandomGradient ENDP

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; float DotGridGradient(float value1, float value2, Vector2 pixel)
; 
; Obliczanie iloczynu skalarnego z losowego gradientu i współrzędnych aktualnego piksela
DotGridGradient PROC
	call   RandomGradient ; wywołanie funkcji RandomGradient, która w xmm0 zwróci losowy wektor jednostkowy
	movaps xmm15, xmm1	  ; wpisanie do rejestru xmm15 koordynat aktualnego piksela
	subps  xmm15, xmm0	  ; odjęcie od aktualnego koordynata zwróconego wyżej wersora i wpisanie powstałego w ten sposób wektora odległości do rejestru xmm15

	; obliczenie iloczynu skalarnego z rejestrów xmm0 i xmm15, trzecim parametrem jest 49, czyli 0011 0001 binarnie, gdzie 0011 oznacza, że pod uwagę przy obliczaniu 
	; będą brane wartości [0-31] i [32-63] dla obu rejestrów a 0001, że wynik zostanie zapisany w xmm0 na dolnych 32 bitach
	dpps   xmm0, xmm1, 49 
	ret					  ; zwrócenie wyniku w xmm0
DotGridGradient ENDP

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; float CalculatePerlinNoiseValueForPixel(int seed, Vector2 pixel)
;
; Obliczanie wartości szumu Perlina dla konkretnych punktów x, y.
; Pierwszy parametr, czyli seed będzie w rejestrze ecx a piksel w rejestrze xmm0
CalculatePerlinNoiseValueForPixel PROC
	mov      seed, ecx		   
	movaps   xmm1, xmm0		   
	roundps  xmm2, xmm1, 1     ; wpisanie zaokrąglonej w dół wartości z xmm1 do rejestru xmm2
	movaps   xmm3, xmm2		   
	addps    xmm3, vector11    ; dodanie wektor z x = 1, y = 1 do xmm2 i zapisanie w rejestrze xmm3
	movaps   xmm4, xmm1
	subps    xmm4, xmm2		   ; obliczanie wag interpolacji

	; obliczanie wartości x dla wektora n1
	movaps   xmm15, xmm2   
	cvtss2si eax, xmm15        ; wpisanie do eax dolnych 32 bitów xmm2 przekonwertowanych na integera
	psrldq   xmm15, 4		   ; przesunięcie bitowe w prawo o 4, żeby wartość spod xmm2[32-63] znalazła się pod xmm2[32-63]
	cvtss2si ecx, xmm15		   ; wpisanie do ecx dolnych 32 bitów xmm2 przekonwertowanych na integera
	call     DotGridGradient

	ret	
CalculatePerlinNoiseValueForPixel ENDP

END