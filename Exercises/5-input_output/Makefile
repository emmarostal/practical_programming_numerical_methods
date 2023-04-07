Out.txt: main.exe input.txt Makefile inputfile.txt
	mono main.exe -numbers:1,2,3,4,5 -input:input.txt -output:Out.txt < inputfile.txt > $@


main.exe: main.cs 
	mcs main.cs


inputfile.txt : Makefile
	echo -1.1e-1 >>$@
	echo 2.5  >>$@
	echo -3e-5 >>$@

input.txt: Makefile
	echo 5 >>$@
	echo 6 >>$@
	echo 7 >>$@
	echo 8 >>$@
clean:                          
	rm --force Out.txt main.exe