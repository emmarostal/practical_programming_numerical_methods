DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library

all:  radial.svg fixed_rmax.svg fixed_dr.svg Out.txt
fixed_rmax.svg : fixed_rmax.gpi fixed_rmax.data
	gnuplot $<
fixed_dr.svg : fixed_dr.gpi fixed_dr.data 
	gnuplot $<
radial.svg : radial.gpi radial.data 
	gnuplot $<

Out.txt   :   main.exe
	mono main.exe  -type: > Out.txt

fixed_rmax.data : main.exe
	mono main.exe -type:rmax > fixed_rmax.data

fixed_dr.data : main.exe
	mono main.exe -type:dr > fixed_dr.data

radial.data : main.exe
	mono main.exe -type:radial > radial.data

main.exe : main.cs matrix.dll jacobi.dll
	mcs $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

jacobi.dll: jacobi.cs matrix.dll
	mcs -target:library -out:$@ $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))



