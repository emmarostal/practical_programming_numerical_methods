using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{
    static void Unitcircle()
	{
		vector a = new vector(0,0);
		vector b = new vector(1,2*PI);
		Func<vector,double> UnitCircle = x => x[0]; // area of circle is integral of r dr d(theta), so the function is just r (= x[0])
		int resolution = 60;
		double  min = 1;
		double  max = 1e5;
		Directory.CreateDirectory("data");
		double[] realError = new double[resolution];
		int[] Ns = new int[resolution];
		using (StreamWriter output = new StreamWriter("data/uc.data"))
		{
			for(int i=0;i<resolution;i++)
			{
				double n = Log10(min) + (Log10(max)-Log10(min))/(resolution-1)*i;
				int N = (int)Pow(10,n);
				Ns[i] = N;
				(double integral, double error) = MC.plain_mc(UnitCircle,a,b,N);
				(double qIntegral, double qError) = MC.quasi_mc(UnitCircle,a,b,N);
				realError[i] = Abs(PI - integral);
				output.WriteLine($"{n} {N} {integral} {error} {realError[i]} {1/Sqrt(N)} {qIntegral} {qError}"); 
			}
		}	
    }

    public static void Main(){
        string border = new string('-',83);
        WriteLine(border);
        WriteLine("Part A");
        WriteLine(border);
        WriteLine("The area of a unit circle was calculated using simple Monte Carlo integration.");
        WriteLine("The result can be seen in UnitCircle.svg.");
        WriteLine("The estimated error and the actual error are plotted as functions of the number of");
        WriteLine("sampling points N in UnitCircleErrors.svg to be compared with 1/Sqrt(N).");
        

        Unitcircle();

        var a = new vector(0, 0, 0);  
        var b = new vector(PI, PI, PI);
        int N = 10000000;
        var (result, error) = MC.plain_mc(x => 1/(PI*PI*PI) * 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2])), a, b, N);
        WriteLine("Complicated integral 1-(cos(x)*cos(y)*cos(z))^-1 with respect to x, y and z,");
        WriteLine($"all from 0 to PI is calculated as: {result} +- {error},");
        WriteLine($"using N = {N} in simple Monte Carlo integration." );
		WriteLine($"It should return Gamma(1/4)^4 / (5*PI^3) = 1.3932039296856768591842462603255");
        WriteLine(border);

        WriteLine("Part B");
        WriteLine(border);
        WriteLine(" A multidimensional quasi-random Monte-Carlo integrator has been implemented.");
        WriteLine(" The scaling of the error is compared to the pseudo-random Monte-Carlo integrator");
        WriteLine(" in UnitCircleErrorsCompare.svg.");
        WriteLine(" The calculated area of the unit circle using this integration scheme is plotted in");
        WriteLine(" UnitCircle.svg.");
        WriteLine(border);


    }

}