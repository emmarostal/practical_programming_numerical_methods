Out.txt     :     main.exe
	mono main.exe > Out.txt
main.exe : main.cs vec.dll
	mcs main.cs -reference:vec.dll
vec.dll : vec.cs
	mcs -target:library -out:vec.dll vec.cs 
clean: 
	rm -f Out.txt main.exe vec.dll

