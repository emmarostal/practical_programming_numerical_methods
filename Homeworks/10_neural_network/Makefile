DIR = ../../library
MINDIR = ../09_minimisation
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt Training.svg

Out.txt:  main.exe  
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll ann.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

minimizer.dll: $(MINDIR)/minimizer.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

ann.dll : ann.cs matrix.dll minimizer.dll
	mcs -target:library -out:$@ $(CODE) $(DLLS)

Training.svg : training.gpi
	gnuplot $<

clean:
	rm -f main.exe Out.txt ann.dll matrix.dll minimizer.dll trainingPoints.txt Training.svg \
	5neuronInterp.txt
