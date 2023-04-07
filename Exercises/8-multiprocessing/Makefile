N = 1e8
TIME = /usr/bin/time -p -a -o $@

Out.txt : main.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@

main.exe : main.cs
	mcs main.cs