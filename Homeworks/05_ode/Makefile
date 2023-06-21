DIR = ../../library
GDIR = ../../Exercises/7-generic_list
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt Sine.svg Dampened.svg Dampened_end.svg LV.svg
Out.txt:  main.exe  
	mono main.exe > out.txt
	
Sine.svg: sine.gpi
	gnuplot $<
Dampened.svg: dampened.gpi
	gnuplot $<
Dampened_end.svg: dampened_endpoints.gpi
	gnuplot $<
LV.svg: lv.gpi
	gnuplot $<


main.exe : main.cs matrix.dll ode.dll genlist.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

ode.dll : ode.cs matrix.dll genlist.dll
	mcs -target:library -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

genlist.dll: $(GDIR)/genlist.cs
	mcs -target:library -out:./$@ $^

clean:
	rm -f main.exe out.txt genlist.dll matrix.dll ode.dll \
	Dampened_end.svg Dampened.svg LV.svg Sine.svg \
	harmonic_diff.txt dampened_endpoints.txt dampened.txt lv.txt
