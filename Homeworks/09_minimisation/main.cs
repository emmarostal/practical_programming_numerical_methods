using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){
        string border = new string('-',96);
        WriteLine(border);
        WriteLine("Part A");
        WriteLine(border);

        WriteLine("We wish to find the minimum of the Rosenbrock's Valley function: f(x,y)=(1-x)^2+100(y-x^2)^2.");
        
        vector res1 = minimizer.qnewton(Rosen, new vector(2,2)).Item1;
        int nsteps1 = minimizer.qnewton(Rosen, new vector(2,2)).Item2;
        
        WriteLine($"The minimizer routine found the minimum at: ({res1[0]}, {res1[1]}) in {nsteps1} steps.");
        WriteLine("The mimimum should be located at (1,1).\n");

        WriteLine("Now we wish to find the minimum of the Himmelblau's function: f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2.");

        vector res2 = minimizer.qnewton(Himmelblau, new vector(3.1,2.1)).Item1;
        int nsteps2 = minimizer.qnewton(Himmelblau, new vector(3.1,2.1)).Item2;
        WriteLine("With start guess of (3.1, 2,1) the minimizer routine found the minimum at:");
        WriteLine($"({res2[0]}, {res2[1]}) in {nsteps2} steps.");
        WriteLine("The minimum should be located at (3,2).\n");
        WriteLine("There are three other minima, which can be found by changing the starting guess:");

        vector res3 = minimizer.qnewton(Himmelblau, new vector(-2,3)).Item1;
        int nsteps3 = minimizer.qnewton(Himmelblau, new vector(-2,3)).Item2;
        WriteLine("With start guess of (-2, 3) the minimizer routine found the minimum at:");
        WriteLine($"({res3[0]}, {res3[1]}) in {nsteps3} steps.");
        WriteLine("The minimum should be located at (-2.805118,3.131312).\n");

        vector res4 = minimizer.qnewton(Himmelblau, new vector(-3,-3)).Item1;
        int nsteps4 = minimizer.qnewton(Himmelblau, new vector(-3,-3)).Item2;
        WriteLine("With start guess of (-3, -3) the minimizer routine found the minimum at:");
        WriteLine($"({res4[0]}, {res4[1]}) in {nsteps4} steps.");
        WriteLine("The minimum should be located at (-3.779310,-3.283186).\n");

        vector res5 = minimizer.qnewton(Himmelblau, new vector(3,-2)).Item1;
        int nsteps5 = minimizer.qnewton(Himmelblau, new vector(3,-2)).Item2;
        WriteLine("With start guess of (3, -2) the minimizer routine found the minimum at:");
        WriteLine($"({res5[0]}, {res5[1]}) in {nsteps5} steps.");
        WriteLine("The minimum should be located at (3.584428,-1.848126).\n");
        WriteLine(border);
        WriteLine("Part B");
        WriteLine(border);

        var energy = new List<double>();//Read Higgs data
		var signal = new List<double>();
		var error  = new List<double>();
		var separators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
		do{
				string line=Console.In.ReadLine();
				if(line==null)break;
				string[] words=line.Split(separators,options);
				energy.Add(double.Parse(words[0]));
				signal.Add(double.Parse(words[1]));
				error .Add(double.Parse(words[2]));
		}while(true);

        WriteLine("Fitting Breit-Wigner to Higgs data with initial parameter guesses A = 16, m = 126, Γ = 2");
        vector guesses = new vector(new double[3]{16,126,2});//initial guesses for parameters
        vector fitparams = minimizer.fit(BW,guesses,energy, signal, error, 1e-4);
		WriteLine($"Fit concluded with parameters A = {fitparams[0]}, m = {fitparams[1]}, Γ = {fitparams[2]}");
		WriteLine("The resulting plot can be seen in BW_fit.svg");

		string toWrite = "";
		for(int i = 0; i < 1000; i++){
			double x = energy[0] + (energy[energy.Count-1] - energy[0])*i/1000.0;
			vector parameters = new vector(new double[4]{x,fitparams[0],fitparams[1],fitparams[2]});
			double y = BW(parameters);
			toWrite += $"{x}\t{y}\n";
		}
		File.WriteAllText("bw_fitteddata.txt",toWrite);
        WriteLine(border);

    }


    

    static double Rosen(vector x){
        return Pow(1-x[0],2)+100*Pow(x[1]- x[0]*x[0],2);
    }

    public static double Himmelblau(vector x){
		return Pow(x[0]*x[0]+x[1]-11,2)+Pow(x[0]+x[1]*x[1]-7,2);
	}

    public static double BW(vector par){
		double x = par[0];
		double A = par[1];
		double xm = par[2]; 
		double d = par [3];
		return A/( (x-xm)*(x-xm) + d*d/4  );
	}

}