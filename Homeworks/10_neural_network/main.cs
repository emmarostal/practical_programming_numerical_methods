using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){
        string border = new string('-',70);
        WriteLine(border);
        WriteLine("Part A");
        WriteLine(border);

        int dataSize = 20;
		vector xs = new vector(dataSize);
		vector ys = new vector(dataSize);

        WriteLine("Making training points");
		string toWrite = "";
		for(int i = 0; i < dataSize; i++){
			double x = 2.0 * i / dataSize - 1;
			xs[i] = x;
			ys[i] = Wavelet(x);
			toWrite += $"{xs[i]}\t{ys[i]}\n";
		}
		File.WriteAllText("trainingPoints.txt",toWrite);

        WriteLine("Interpolating using 10 neurons and 100 interpolation points");
		ann myNN = new ann(10);
		myNN.train(xs,ys);

		toWrite = "";
		int interpolationPoints = 1000;
		for(int i = 0; i < interpolationPoints; i++){
			double x = 2.0 * i / interpolationPoints - 1;
			toWrite += $"{x}\t{myNN.response(x)}\n";
		}
		File.WriteAllText("10neuronInterp.txt",toWrite);

		WriteLine("The result can be seen in Training.svg.");
		WriteLine(border);
		WriteLine("Part B");
        WriteLine(border);


		toWrite = "";
        for(int i = 0; i < interpolationPoints; i++){
            double x = 2.0 * i / interpolationPoints - 1;
            toWrite += $"{x}\t{myNN.firstDResponse(x)}\t{firstD(x)}\n";
        }
        File.WriteAllText("firstD.txt",toWrite);

		toWrite = "";
        for(int i = 0; i < interpolationPoints; i++){
            double x = 2.0 * i / interpolationPoints - 1;
            toWrite += $"{x}\t{myNN.secondDResponse(x)}\t{secondD(x)}\n";
        }
        File.WriteAllText("secondD.txt",toWrite);

		toWrite = "";
        for(int i = 0; i < interpolationPoints; i++){
            double x = 2.0 * i / interpolationPoints - 1;
            toWrite += $"{x}\t{myNN.integral(0,x)}\t{intg(x,0)}\n";
        }
        File.WriteAllText("antiD.txt",toWrite);
		WriteLine("The first and second derivatives and the anti-derivative");
		WriteLine("can be seen in Plot.svg along with the analytical solutions.");
		WriteLine(border);
    }

    public static double Wavelet(double x){
        return Cos(5*x)*Exp(-Pow(x,2)); 
    }
	public static double firstD(double x){
        return -2.0*Exp(-x*x)*(2.5*Sin(5*x-1) + x * Cos(5*x-1));
    }

    public static double secondD(double x){
        return Exp(-x*x)*((4*x*x-27)*Cos(5*x-1) + 20*x*Sin(5*x-1));
    }

    public static double intg(double x, double a){
        return integrator.integrate(Wavelet, a, x);
    }


}