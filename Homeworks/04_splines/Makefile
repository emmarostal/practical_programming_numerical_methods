DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library

all: main.exe linear.svg Out.txt quadratic.svg qsine.svg
Out.txt   :   main.exe
	mono main.exe > Out.txt

linear.svg : linear.gpi
	gnuplot $<

quadratic.svg : quadratic.gpi
	gnuplot $<

qsine.svg : qsine.gpi
	gnuplot $<

main.exe : main.cs interpolation.dll
	mcs -t:exe $(CODE) $(DLLS) -reference:matrix.dll

interpolation.dll: interpolation.cs qspline.cs 
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

