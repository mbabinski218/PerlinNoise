; Temat:  Szum Perlina
; Opis:	  Algorytm generuje wartość szumu Perlina dla konkretnego piksela
; Autor:  Mateusz Babiński, Informatyka, rok 3, sem. 5, gr. 5, data: 24.10.2022
; Wersja: 0.4

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.data?
	seed DWORD ?  ; wartość używana przy wyliczaniu gradientu w celu nadania losowości generowanym wynikom.

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.const
	vector11	   REAL4 1.0, 1.0		    ; vector2d z x = 1, y = 1
	pi			   REAL8 3.141592653589793  ; pi
	intMaxPlusOne  REAL8 2147483648.0       ; maksymalna wartość integera + 1
	three		   REAL4 3.0
	two			   REAL4 2.0
	zeroPointFive  REAL4 0.5

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.code

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; Interpolacja za pomocą wzoru: (y - x) * (3 - 2 * w) * w * w + x, gdzie x i y to współrzędne piksela a parametr w to waga interpolacji.
; Piksel znajduje się w rejestrze xmm0 (xmmo[0-31] = x i xmm0[32-63] = y) oraz parametr w znajduje się w rejestrze xmm15.
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Interpolate PROC
	movss xmm12, xmm15

	movss  xmm14, xmm0  ; xmm14[0-31] = x
	psrldq xmm0, 4		; xmm0[0-31]  = y
	subss  xmm0, xmm14	; xmm0[0-31]  = y - x

	movss xmm13, three  ; xmm13[0-31] = 3
	mulss xmm15, two	; xmm15[0-31] = 2 * w
	subss xmm13, xmm15	; xmm13[0-31] = 3 - 2 * w
	mulss xmm13, xmm12	; xmm13[0-31] = (3 - 2 * w) * w
	mulss xmm13, xmm12  ; xmm13[0-31] = (3 - 2 * w) * w * w
	mulss xmm0, xmm13   ; xmm0[0-31]  = (y - x) * (3 - 2 * w) * w * w
	addss xmm0, xmm14   ; xmm0[0-31]  = (y - x) * (3 - 2 * w) * w * w + x

	ret	; wynik zwracany w xmm0
Interpolate ENDP

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; Funkcja oblicza losowy wektor jednostkowy - cały obraz składu się z wielu pikseli a te w zależności 
; od wartości otrzymanego wersora będą rosły lub malały. 
; Wartość pierwsza znajduje się w rejestrze xmm13 a druga w xmm14.
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
RandomGradient PROC
	local temp : REAL4  ; zmienna lokalna używana w późniejszej fazie do operacji na rejestrach zmiennoprzecinkowych
	
	; wykonanie szeregu obliczeń według wzoru, aby otrzymać losową wartość, z której będzie liczony wektor jednostkowy
	pmuldq xmm13, seed   ; pomnożenie pierwszej wartości przez seed, w celu zwiększenia losowości końcowego wektora
	movaps xmm15, xmm13
	movaps xmm0, xmm13
	pslld  xmm0, 16      ; xmm13 << 4 * sizeof DWORD
	psrad  xmm15, 16     ; xmm13 >> 4 * sizeof DWORD
	por    xmm15, xmm0
	pxor   xmm14, xmm15  ; xmm14 = a << 4 * sizeof DWORD | a >> 4 * sizeof DWORD
	pmuldq xmm14, seed	 ; xmm14 * seed, w celu zwiększenia losowości końcowego wektora
	
	movaps xmm15, xmm14
	movaps xmm0, xmm14
	pslld  xmm0, 16      ; xmm13 << 4 * sizeof DWORD
	psrad  xmm15, 16     ; xmm13 >> 4 * sizeof DWORD
	por    xmm15, xmm0	 
	pxor   xmm13, xmm15  ; xmm13 = b << 4 * sizeof DWORD | b >> 4 * sizeof DWORD
	pmuldq xmm13, seed   ; xmm13 * seed w celu zwiększenia losowości końcowego wektora

	movsd    xmm15, pi
	movsd    xmm0, intMaxPlusOne
	divsd    xmm15, xmm0		  ; w dolnych 64 bitach xmm15 znajduje się wartość pi/intMaxPlusOne
	cvtdq2pd xmm0, xmm13		  ; wpisanie w dolne 64 bity xmm0 wartości z xmm13
	mulsd	 xmm15, xmm0		  ; pomnożenie pierwszych 64 bitów xmm15 przez dolne 64 bity xmm0 oraz zapisanie wyniku w xmm15
	cvtsd2ss xmm15, xmm15		  ; konwersja pierwszych 64 bitów xmm15 na wartość zmiennoprzecinkową pojedyńczej precyzji i wpisanie jej w pierwsze 32 bity rejestru xmm15

	movd   temp, xmm15 
	fld    temp			; ustawienie wartości st(0) na wyliczoną losową długość
	fsin	            ; obliczenia cosinusa z wartości w st(0), aby otrzymać liczbę w przedziale od -1 do 1
	fstp   temp	        ; pobranie wartości z st(0)	i przypisanie jej do zmiennej temp
	movss  xmm0, temp   ; wpisanie pobranej wartości pod pierwsze 32 bity w xmm0
	pslldq xmm0, 4      ; przesunięcie bitowe w lewo, aby otrzymana wartość znalazła się jak druga (czyli w xmm0[32-63])
	
	movd  temp, xmm15 
	fld   temp		   ; ustawienie wartości st(0) na wyliczoną losową długość
	fcos			   ; tym razem obliczanie wartości przy pomocy sinusa, aby otrzymać ponownie liczbę w przedziale od -1 do 1, ale inną niż przy pomocy cosinusa
	fstp  temp		   ; pobranie wartości z st(0) i przypisanie jej do zmiennej temp

	; wpisanie najpierw wyliczonej wartości pod dolne 32 bity w xmm15 a następnie z xmm15 wpisanie tej liczby do xmm0, ponieważ przy użyciu instrukcji
	; movss xmm0, temp wartość, która została tam wcześniej przypisana pod bity [32-63] była czyszczona
	movss xmm15, temp 
	movss xmm0, xmm15 

	ret ; losowy wersor zwracany jest w xmm0
RandomGradient ENDP

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; Obliczanie iloczynu skalarnego z losowego gradientu i współrzędnych aktualnego piksela.
; W xmm1 znajduje się wartość aktualnego piksela a w xmm13 i xmm14 wartości potrzebne do poprawnego wywołania funckji RandomGradient.
DotGridGradient PROC
	cvtdq2ps xmm12, xmm14
	pslldq   xmm12, 4
	cvtdq2ps xmm15, xmm13
	movss    xmm12, xmm15

	call   RandomGradient  ; wywołanie funkcji RandomGradient, która w xmm0 zwróci losowy wektor jednostkowy
	movaps xmm15, xmm1	   ; wpisanie do rejestru xmm15 koordynat aktualnego piksela
	subps  xmm15, xmm12	   ; odjęcie od aktualnego koordynata zwróconego wyżej wersora i wpisanie powstałego w ten sposób wektora odległości do rejestru xmm15

	; obliczenie iloczynu skalarnego z rejestrów xmm0 i xmm15, trzecim parametrem jest 49, czyli 0011 0001 binarnie, gdzie 0011 oznacza, że pod uwagę przy obliczaniu 
	; będą brane wartości [0-31] i [32-63] dla obu rejestrów a 0001, że wynik zostanie zapisany w xmm0 na dolnych 32 bitach
	dpps   xmm0, xmm15, 49 
	ret					   ; zwrócenie wyniku w xmm0
DotGridGradient ENDP

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; Obliczanie wartości szumu Perlina dla konkretnych punktów x, y.
; Pierwszy parametr, czyli seed będzie w rejestrze ecx a piksel w rejestrze xmm0.
CalculatePerlinNoiseValueForPixel PROC
	mov      seed, ecx		   
	movaps   xmm1, xmm0		 ; xmm1 = pixel  
	roundps  xmm2, xmm1, 1	 ; xmm2	= vec1
	movaps   xmm3, xmm2		 
	addps    xmm3, vector11  ; xmm3 = vec2  
	movaps   xmm4, xmm1      
	subps    xmm4, xmm2	     ; xmm4 = lerpWeights	   

	; obliczanie wartości y dla wektora n1
	movss    xmm15, xmm3      
	cvtps2dq xmm13, xmm15     ; xmm13 = vec2.x  
	movaps   xmm15, xmm2      
	psrldq   xmm15, 4		  ; xmm15[0-31] = xmm15[32-63]
	cvtps2dq xmm14, xmm15	  ; xmm14 = vec1.y
	call     DotGridGradient  
	movaps   xmm5, xmm0       
	pslldq   xmm5, 4          ; wartość xmm5[32-63] jest równa wartości zwróconej z procedury DotGridGradient z parametrami vec2.x i vec1.y 

	; obliczanie wartości x dla wektora n1
	movss    xmm15, xmm2      
	cvtps2dq xmm13, xmm15	  ; xmm13 = vec1.x  
	movaps   xmm15, xmm2	  
	psrldq   xmm15, 4		  ; xmm15[0-31] = xmm15[32-63]
	cvtps2dq xmm14, xmm15	  ; xmm14 = vec1.y 
	call     DotGridGradient     
	movss    xmm5, xmm0		  ; wartość xmm5[0-31] jest równa wartości zwróconej z procedury DotGridGradient z parametrami vec1.x i vec1.y
							  
	; obliczanie wartości y dla wektora n2
	movss    xmm15, xmm3   	  
	cvtps2dq xmm13, xmm15	  ; xmm13 = vec2.x  
	movaps   xmm15, xmm3	  
	psrldq   xmm15, 4		  ; xmm15[0-31] = xmm[32-63]
	cvtps2dq xmm14, xmm15	  ; xmm14 = vec2.y 
	call     DotGridGradient     
	movaps   xmm6, xmm0		  
	pslldq   xmm6, 4		  ; wartość xmm6[32-63] jest równa wartości zwróconej z procedury DotGridGradient z parametrami vec2.x i vec2.y

	; obliczanie wartości x dla wektora n2
	movss    xmm15, xmm2   
	cvtps2dq xmm13, xmm15     ; xmm13 = vec1.x    
	movaps   xmm15, xmm3
	psrldq   xmm15, 4		  ; xmm15[0-31] = xmm15[32-63] 
	cvtps2dq xmm14, xmm15	  ; xmm14 = vec2.y    
	call     DotGridGradient     
	movss    xmm6, xmm0		  ; wartość xmm5[0-31] jest równa wartości zwróconej z procedury DotGridGradient z parametrami vec1.x i vec2.y

	; obliczanie wartości y dla wektora lerpBetweenPointsAndGradVec
	pxor xmm15, xmm15   ;xmm15 = 0
	movss xmm15, xmm4   ;xmm15 = lerpWeights.x
	movaps xmm0, xmm6   ;xmm0 = n2
	call Interpolate
	movaps xmm2, xmm0   ;xmm2[0-31] = wynik interpolacji dla y
	pslldq xmm2, 4		;xmm2[31-62] = wynik interpolacji dla y
	
	; obliczanie wartości x dla wektora lerpBetweenPointsAndGradVec
	pxor xmm15, xmm15   ;xmm15 = 0
	movss xmm15, xmm4   ;xmm15 = lerpWeights.x
	movaps xmm0, xmm5   ;xmm0 = n1
	call Interpolate
	movss xmm2, xmm0    ;xmm2[0-31] = wynik interpolacji dla x

	; obliczanie wartości końcowej szumu Perlina
	psrldq xmm4, 4
	movss  xmm15, xmm4  ;xmm15 = lerpWeights.y	
	movaps xmm0, xmm2	;xmm0 = lerpBetweenPointsAndGradVec
	call Interpolate

	; pomnożenie zwracanego wyniku przez 0.5 a nastepnie dodanie 0.5, aby końcowy wynik był z przedziału od 0 do 1.
	mulss xmm0, zeroPointFive
	addss xmm0, zeroPointFive

	ret	; wynik zwracany w xmm0 
CalculatePerlinNoiseValueForPixel ENDP

END