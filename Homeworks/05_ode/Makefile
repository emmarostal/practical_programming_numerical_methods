DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library
GDIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt Sine.svg Dampened.svg Dampened_end.svg LV.svg
Out.txt:  main.exe  
	mono main.exe > out.txt
	
Sine.svg: sine.gpi
	gnuplot $<
Dampened.svg: dampened.gpi
	gnuplot $<
Dampened_end.svg: dampened_endpoints.gpi
	gnuplot $<
LV.svg: lv.gpi
	gnuplot $<


main.exe : main.cs matrix.dll ode.dll genlist.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

ode.dll : ode.cs matrix.dll genlist.dll
	mcs -target:library -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^