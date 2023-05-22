DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library
Out.txt   :   main.exe
	mono main.exe > Out.txt

main.exe : main.cs QRGS.dll matrix.dll
	mcs -t:exe $(CODE) $(DLLS)

QRGS.dll: QRGS.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^
