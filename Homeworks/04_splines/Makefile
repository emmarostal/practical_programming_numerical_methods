DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = ../../library

all: main.exe Out.txt linear.svg quadratic.svg qsine.svg
Out.txt   :   main.exe
	mono main.exe > Out.txt

linear.svg : linear.gpi
	gnuplot $<

quadratic.svg : quadratic.gpi
	gnuplot $<

qsine.svg : qsine.gpi
	gnuplot $<

main.exe : main.cs matrix.dll interpolation.dll 
	mcs -t:exe $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

interpolation.dll: interpolation.cs qspline.cs 
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

clean:
	rm -f Out.txt main.exe interpolation.dll matrix.dll quadratic.svg qsine.svg linear.svg \
	linterp.txt qinterp.txt qsine.txt
