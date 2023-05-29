using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{

    public static double f1(double x){
		return 1/Sqrt(x);
	}
    public static double f2(double x){
		return 4*Sqrt(1-x*x);
	}
    public static double f3(double x){
		return Log(x)/(Sqrt(x));
	}

    public static double erf(double x){
		if(x < 0) return -erf(-x);
		double absErf = 0;
		if(x <= 1){
			absErf = 2/Sqrt(PI)*integrator.integrate(z => Exp(-z*z),0,x,1e-7,1e-6);
		}
		else{
			absErf = 1-2/Sqrt(PI)*integrator.integrate(t => Exp(-Pow(x+(1-t)/t,2))/t/t,0,1,1e-7,1e-6);
		}
		return absErf;
	}

    public static double erf_approx(double x){
        /// single precision error function (Wiki Abramowitz and Stegun)
        if(x<0) return -erf_approx(-x);
        double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
        double t=1/(1+0.3275911*x);
        double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
        return 1-sum*Exp(-x*x);
    } 

    public static void Main(){
        string border = new string('-',123);
        WriteLine(border);
        WriteLine("Part A");
        WriteLine(border);
        WriteLine("The integrator is tested on a selection of functions:");
        WriteLine($"Sqrt(x)dx from x = 0 to x = 1.         Expected result: 2/3 ~ 0.667,   Result: {integrator.integrate(Sqrt,0,1)}");
        WriteLine($"1/Sqrt(x)dx from x = 0 to x = 1.       Expected result: 2,             Result: {integrator.integrate(f1,0,1)}");
        WriteLine($"4*Sqrt(1-x^2)dx from x = 0 to x = 1.   Expected result: pi,            Result: {integrator.integrate(f2,0,1)}");
        WriteLine($"ln(x)/Sqrt(x)dx from x = 0 to x = 1.   Expected result: -4,            Result: {integrator.integrate(f3,0,1)}");
        WriteLine(border);
        WriteLine("Calculation of the error function with absolute integration error 1e-7 and relative error 1e-6 has been plotted on Erf.svg.");

        
		string toWrite = "";
		for(int i = 0; i < 1000; i++){
			double x = -5+10.0*i/1000;
			toWrite += $"{x}\t{erf(x)}\t{erf_approx(x)}\t{Abs(erf(x)-erf_approx(x))}\n";
		}
		File.WriteAllText("calculated_erf.txt", toWrite);

        toWrite = "";
        string tabPoints = File.ReadAllText("erf_tab.data");
		string[] lines = tabPoints.Split("\n");
		for(int i = 0; i < lines.Length-1; i++){
			double x = double.Parse(lines[i].Split(" ")[0]);
			double tabVal = double.Parse(lines[i].Split(" ")[1]);
			toWrite += $"{x}\t{Abs(erf(x)-tabVal)}\t{Abs(erf_approx(x)-tabVal)}\t{Pow(erf(x)-tabVal,2)-Pow(erf_approx(x)-tabVal,2)}\n";
		}
		File.WriteAllText("erf_residuals.txt", toWrite);
        
        WriteLine("To determine whether the integral error-function is more accurate than the approximate error-function, tabulated values ");
        WriteLine("from wikipedia are used. The squared value of the difference to the tabulated values for each model is calculated and ");
        WriteLine("the approximate result is subtracted from the integral result. The sign of the difference determines which model is more");
        WriteLine("accurate. The negative values indicate that the integral erf is more accurate. The result can be seen in ErfResiduals.svg.");
        WriteLine(border);
        WriteLine("Part B");
        WriteLine(border);
        WriteLine("Testing number of evalutations needed in the different integrators");
        WriteLine("Integral from x = 0 to x = 1 of 1/sqrt(x), should be equal to: 2");
        double resultnormalA = integrator.integrate(f1, 0, 1);
        WriteLine($"Normal adaptive quadratures gives:             {resultnormalA} in {integrator.evals} evaluations");
        double resultCCA = integrator.integrateClenCur(f1, 0, 1);
        WriteLine($"Clenshaw-Curtis variable transformation gives: {resultCCA} in {integrator.evals} evaluations");

        WriteLine();
        WriteLine($"Integral from x = 0 to x = 1 of 4*sqrt(1-x^2), should be equal to: {-4}");
        double resultnormalB = integrator.integrate(f3, 0, 1);
        WriteLine($"Normal adaptive quadratures gives:             {resultnormalB} in {integrator.evals} evaluations");
        double resultCCB = integrator.integrateClenCur(f3, 0, 1);
        WriteLine($"Clenshaw-Curtis variable transformation gives: {resultCCB} in {integrator.evals} evaluations");
      

        WriteLine();
        WriteLine("The python script finds the results with the same tolerance in 231 and 315 evaluations, respectivly.");
        WriteLine(border);











    }





}