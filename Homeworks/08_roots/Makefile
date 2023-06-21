DIR = ../../library
QRGSDIR = ../01_linear_equations
ODEDIR = ../05_ode
GDIR = ../../Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt Hydrogen.svg Convergence.svg

Out.txt:  main.exe  
	mono main.exe > Out.txt

Hydrogen.svg: hydrogen.gpi 
	gnuplot $<

Convergence.svg: convergence.gpi rmin.txt rmax.txt absacc.txt epsacc.txt 
	gnuplot $<

main.exe : main.cs matrix.dll QRGS.dll genlist.dll ode.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

QRGS.dll: $(QRGSDIR)/QRGS.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^

ode.dll: $(ODEDIR)/ode.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll,genlist.dll

clean:
	rm -f Out.txt main.exe ode.dll genlist.dll matrix.dll QRGS.dll \
	Hydrogen.svg Convergence.svg rmin.txt rmax.txt epsacc.txt absacc.txt \
	wavefunctions.txt