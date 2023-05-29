DIR = $(HOME)/prak_prog/repos/practical_programming_numerical_methods/library
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt ErfResiduals.svg Erf.svg erf_tab.data Makefile

Out.txt:  main.exe Makefile erf_tab.data 
	mono main.exe > Out.txt

Erf.svg: erf.gpi erf_tab.data calculated_erf.txt
	gnuplot $<

ErfResiduals.svg: erf_residuals.gpi calculated_erf.txt
	gnuplot $<

main.exe : main.cs integrator.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

integrator.dll : integrator.cs
	mcs -target:library -out:$@ $(CODE) $(DLLS)

erf_tab.data : Makefile
	>$@
	echo 0 0 >>$@
	echo 0.2 0.222702589 >>$@
	echo 0.4 0.428392355 >>$@
	echo 0.6 0.603856091 >>$@
	echo 0.8 0.742100965 >>$@
	echo 1 0.842700793 >>$@
	echo 1.2 0.910313978 >>$@
	echo 1.4 0.952285120 >>$@
	echo 1.6 0.976348383 >>$@
	echo 1.8 0.989090502 >>$@
	echo 2 0.995322265 >>$@
	echo 3 0.999977910 >>$@