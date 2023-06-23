all: Gamma.svg Erf.svg Lngamma.svg

Gamma.svg : gamma.gpi gamma.data factorials_tab.data
	gnuplot $<
Erf.svg : erf.gpi erf.data erf_tab.data
	gnuplot $<
Lngamma.svg : lngamma.gpi lngamma.data lngamma_tab.data
	gnuplot $<


gamma.data : main.exe 
	mono main.exe -type:gamma > $@
erf.data : main.exe 
	mono main.exe -type:error > $@
lngamma.data : main.exe 
	mono main.exe -type:lngamma > $@


main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ $(filter %.cs,$^) $(addprefix -reference:,$(filter %.dll,$^))

sfuns.dll : gamma.cs erf.cs lngamma.cs 
	mcs -target:library -out:$@ $^

factorials_tab.data : Makefile
	>$@
	echo 0 1 >>$@
	echo 1 1 >>$@
	echo 2 2 >>$@
	echo 3 6 >>$@
	echo 4 24 >>$@
	echo 5 120 >>$@
erf_tab.data : Makefile
	>$@
	echo 0 0 >>$@
	echo 1 0.842 >>$@
	echo 2 0.995 >>$@
	echo 3 0.999 >>$@
lngamma_tab.data : Makefile
	>$@
	echo 0 0 >>$@ /log 1
	echo 1 0 >>$@ /log 1
	echo 2 0.693 >>$@ /log 2
	echo 3 1.79 >>$@ /log 6
	echo 4 3.178 >>$@ /log 24 

clean:
	rm -f main.exe sfuns.dll factorials_tab.data erf_tab.data lngamma_tab.data \
	Lngamma.svg Gamma.svg Erf.svg gamma.data erf.data lngamma.data