DIR = ../../library
QRGSDIR = ../01_linear_equations
ODEDIR = ../05_ode
GDIR = ../../Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt BW_fit.svg

Out.txt:  main.exe  
	mono main.exe < Higgsdata.txt 1> Out.txt

main.exe : main.cs matrix.dll QRGS.dll genlist.dll ode.dll minimizer.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

BW_fit.svg: bw_fit.gpi bw_fitteddata.txt
	gnuplot $<
minimizer.dll: minimizer.cs 
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

QRGS.dll: $(QRGSDIR)/QRGS.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^

ode.dll: $(ODEDIR)/ode.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll,genlist.dll

clean:
	rm -f main.exe Out.txt matrix.dll QRGS.dll ode.dll genlist.dll minimizer.dll BW_fit.svg bw_fitteddata.txt

