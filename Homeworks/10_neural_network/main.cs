using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){
        string border = new string('-',40);
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

        WriteLine("Interpolating using 5 neurons");
		ann myNN = new ann(5);
		myNN.train(xs,ys);

		//create interpolation set
		toWrite = "";
		int interpolationPoints = 100;
		for(int i = 0; i < interpolationPoints; i++){
			double x = 2.0 * i / interpolationPoints - 1;
			toWrite += $"{x}\t{myNN.response(x)}\n";
		}
		File.WriteAllText("5neuronInterp.txt",toWrite);
		WriteLine("The result can be seen in Training.svg.");
		WriteLine(border);
		
    }

    public static double Gaussian(double x){
        return Exp(-Pow(x,2));
    }
    public static double GaussianW(double x){
        return x*Exp(-Pow(x,2));
    }
    public static double Wavelet(double x){
        return Cos(5*x)*Exp(-Pow(x,2)); 
    }


}