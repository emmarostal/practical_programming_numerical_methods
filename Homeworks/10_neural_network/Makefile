DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

all: Out.txt

Out.txt:  main.exe  
	mono main.exe

main.exe : main.cs 
	mcs -target:exe -out:$@ $(CODE) $(DLLS)


