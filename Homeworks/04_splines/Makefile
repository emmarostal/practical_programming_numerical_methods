DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library

all: main.exe linear.svg Out.txt
Out.txt   :   main.exe
	mono main.exe > Out.txt

linear.svg : linear.gpi
	gnuplot $<

main.exe : main.cs interpolation.dll
	mcs -t:exe $(CODE) $(DLLS)

interpolation.dll: interpolation.cs
	mcs -target:library -out:./$@ $^ 

