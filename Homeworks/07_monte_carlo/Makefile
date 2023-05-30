DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library
GDIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt main.exe UnitCircle.svg UnitCircleErrors.svg UnitCircleErrorsCompare.svg

Out.txt:  main.exe  
	mono main.exe > Out.txt

UnitCircle.svg: unit_circle.gpi 
	gnuplot $<
UnitCircleErrors.svg: unit_circle_errors.gpi data/uc.data
	gnuplot $<
UnitCircleErrorsCompare.svg: unit_circle_errors_quasi.gpi data/uc.data
	gnuplot $<

main.exe : main.cs matrix.dll mc.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

mc.dll: mc.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll
