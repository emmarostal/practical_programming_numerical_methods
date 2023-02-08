Out.txt     :     main.exe
	mono main.exe > Out.txt
main.exe : main.cs sfuncs.dll
	mcs main.cs -reference:sfuncs.dll
sfuncs.dll : sfuncs.cs
	mcs -target:library -out:sfuncs.dll sfuncs.cs 
clean: 
	rm --force Out.txt main.exe

