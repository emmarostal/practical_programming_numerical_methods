out.txt : main.exe 
	mono main.exe > Out.txt

main.exe: main.cs cmath.dll
	mcs -target:exe -out:$@ $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))

DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/Exercises/6-complex
cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^