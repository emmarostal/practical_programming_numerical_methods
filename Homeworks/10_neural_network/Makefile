DIR = ../../library
MINDIR = ../09_minimisation
INTDIR = ../06_integration
GDIR = ../../Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt Training.svg Plot.svg

Out.txt:  main.exe  
	mono main.exe > Out.txt


main.exe : main.cs matrix.dll ann.dll genlist.dll integrator.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^

minimizer.dll: $(MINDIR)/minimizer.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

integrator.dll: $(INTDIR)/integrator.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll,genlist.dll

ann.dll : ann.cs matrix.dll minimizer.dll
	mcs -target:library -out:$@ $(CODE) $(DLLS)

Training.svg : training.gpi
	gnuplot $<
Plot.svg : derivatives.gpi
	gnuplot $<

clean:
	rm -f main.exe Out.txt ann.dll matrix.dll minimizer.dll trainingPoints.txt Training.svg \
	10neuronInterp.txt firstD.txt secondD.txt antiD.txt genlist.dll integrator.dll Plot.svg
