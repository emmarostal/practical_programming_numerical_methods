DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = ../../library
Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll QRGS.dll 
	mcs -t:exe $(CODE) $(DLLS)

QRGS.dll: QRGS.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

clean:
	rm -f main.exe matrix.dll QRGS.dll Out.txt