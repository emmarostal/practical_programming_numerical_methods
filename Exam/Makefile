DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = ../library
GDIR = ../Exercises/7-generic_list
ODEDIR = ../Homeworks/05_ode
QRGSDIR = ../Homeworks/01_linear_equations

all: Out.txt Sine.svg Dampened.svg
Out.txt:  main.exe  
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll genlist.dll QRGS.dll rootfinder.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^

QRGS.dll: $(QRGSDIR)/QRGS.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

rootfinder.dll: rootfinder.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll,QRGS.dll

Sine.svg: sine.gpi
	gnuplot $<

Dampened.svg: dampened.gpi
	gnuplot $<

clean:
	rm -f genlist.dll matrix.dll QRGS.dll rootfinder.dll main.exe Out.txt Sine.svg harmonic_diff.txt