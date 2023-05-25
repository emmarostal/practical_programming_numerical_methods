using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{

	public static void Main(){
        string border = new string('-',65);
		WriteLine(border);
		WriteLine("Part A and B");
		//we're fitting to ln(y)=ln(a)-λt with errors δln(y)=δy/y.
		var fs = new Func<double,double>[2];
		fs[0] = x => 1;
		fs[1] = x => -x; 
		string data = File.ReadAllText("data.txt");
		string[] points = data.Split("\n");
		int nPoints = points.Length - 1; //last line is empty
		vector xs = new vector(nPoints);
		vector ys = new vector(nPoints);
		vector dys = new vector(nPoints); 

		for(int i = 0; i < nPoints; i++){
			string[] splitted = points[i].Split("\t");
			double currentY = double.Parse(splitted[1]);
			xs[i] = double.Parse(splitted[0]);
			ys[i] = Log(currentY);
			dys[i] = double.Parse(splitted[2])/currentY;
		}

		var poptpcov = fit.lsfit(fs,xs,ys,dys);
		var c = poptpcov.Item1;
		var pcov = poptpcov.Item2;

		WriteLine("A fit to Rutherford and Soddy's data can be seen in fit.svg");
		WriteLine("Covariance matrix:");
		pcov.print();
		double dlambda = Sqrt(pcov[1,1]);
		double doffset = Sqrt(pcov[0,0]);
        
		WriteLine($"\nFitted T1/2 = {Log(2)/c[1]} +- {Log(2)*dlambda/c[1]/c[1]} d,");
		WriteLine("the modern value is 3.6319+-0.0023 d, which means that the value");
        WriteLine("from the fit does not agree with the modern value.");

		WriteLine(border);
		WriteLine("Part C");
		WriteLine("Exponential decay data with fitting parameters shifted by the ");
        WriteLine("uncertainties from the covariance matrix are plotted and can ");
        WriteLine("be seen in fitshift.svg");
		//Write fitted function data to txt file
		string toWrite = "";
		for(int i = 0; i < 1000; i++){
			double t = i/1000.0*(xs[nPoints-1]+1);
			double n = 0;
            double np = 0;
            double nm = 0;

			for(int j = 0; j < fs.Length; j++){
				n+=c[j]*fs[j](t);
			}
            nm+=(c[0]-doffset)*fs[0](t);
            nm+=(c[1]+dlambda)*fs[1](t);
            np+=(c[0]+doffset)*fs[0](t);
            np+=(c[1]-dlambda)*fs[1](t);
			n = Exp(n);
            nm = Exp(nm);
            np = Exp(np);
			toWrite += $"{t}\t{n}\t{nm}\t{np}\n";
		}
		File.WriteAllText("evaluatedFit.txt", toWrite);
        	}
}