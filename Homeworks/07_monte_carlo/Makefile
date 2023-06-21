DIR = ../../library
DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt main.exe UnitCircle.svg UnitCircleErrors.svg UnitCircleErrorsCompare.svg

Out.txt:  main.exe  
	mono main.exe > Out.txt

UnitCircle.svg: unit_circle.gpi 
	gnuplot $<
UnitCircleErrors.svg: unit_circle_errors.gpi uc.data
	gnuplot $<
UnitCircleErrorsCompare.svg: unit_circle_errors_quasi.gpi uc.data
	gnuplot $<

main.exe : main.cs matrix.dll mc.dll
	mcs -target:exe -out:$@ $(CODE) $(DLLS)

matrix.dll: $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./$@ $^

mc.dll: mc.cs
	mcs -target:library -out:./$@ $^ -reference:matrix.dll

clean:
	rm -f Out.txt main.exe matrix.dll mc.dll UnitCircle.svg UnitCircleErrors.svg \
	UnitCircleErrorsCompare.svg uc.data