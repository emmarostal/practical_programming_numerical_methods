DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)
DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library
DIRQR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/Homeworks/01_linear_equations

all: fitshift.svg Out.txt fit.svg
fitshift.svg : fitshift.gpi data.txt
	gnuplot $<

Out.txt   :   main.exe
	mono main.exe > Out.txt

fit.svg : fit.gpi data.txt
	gnuplot $<

main.exe : main.cs matrix.dll fit.dll QRGS.dll
	mcs -t:exe $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

QRGS.dll: $(DIRQR)/QRGS.cs 
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

fit.dll: fit.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll -reference:QRGS.dll

data.txt : Makefile
	>$@
	echo 1\\t117\\t5 >> $@
	echo 2\\t100\\t5 >> $@
	echo 3\\t88\\t5 >> $@
	echo 4\\t72\\t4 >> $@
	echo 6\\t53\\t4 >> $@
	echo 9\\t29.5\\t3 >> $@
	echo 10\\t25.2\\t3 >> $@
	echo 13\\t15.2\\t2 >> $@
	echo 15\\t11.1\\t2 >> $@
