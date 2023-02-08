using System;
using static sfuncs;

public class MathExercises
{
   public static void Main()
   {
      double sqrt2=Math.Sqrt(2.0);
	  double ePi=Math.Exp(Math.PI);
	  double Pie=Math.Pow(Math.PI,Math.E);
	  double two_onefifth=Math.Pow(2,1.0/5);
      System.Console.Write($"sqrt(2) = {Math.Round(sqrt2,4)}\n");
	  System.Console.Write($"sqrt2*sqrt2 = {Math.Round(sqrt2*sqrt2,4)} (should be equal to 2)\n");
	  System.Console.Write($"exp(pi) = {Math.Round(ePi,4)}\n");
	  System.Console.Write($"ln(exp(pi)) = {Math.Round(Math.Log(ePi),4)} (should be equal to pi)\n");
	  System.Console.Write($"pi^(e) = {Math.Round(Pie,4)}\n");
	  System.Console.Write($"(pi^(e))^(1/e) = {Math.Round(Math.Pow(Pie,(1.0/Math.E)),4)} (should be equal to pi)\n");
	  System.Console.Write($"2^(1/5) = {Math.Round(two_onefifth,4)}\n");
	  System.Console.Write($"(2^(1/5))^(5) = {Math.Round(Math.Pow(two_onefifth,5),4)} (should be equal to 2)\n\n");

	  System.Console.Write($"Stirling approximation\n");
	  double gamma_one=sfuncs.gamma(1.0);
	  System.Console.Write($"gamma(1) = {gamma_one}\n");
	  double gamma_two=sfuncs.gamma(2.0);
	  System.Console.Write($"gamma(2) = {gamma_two}\n");
	  double gamma_three=sfuncs.gamma(3.0);
	  System.Console.Write($"gamma(3) = {gamma_three}\n");
	  double gamma_ten=sfuncs.gamma(10.0);
	  System.Console.Write($"gamma(10) = {gamma_ten}\n\n");

	  System.Console.Write($"Ln of Stirling approximation\n");
	  double lngamma_one=sfuncs.lngamma(1.0);
	  System.Console.Write($"ln(gamma(1)) = {lngamma_one}\n");
	  double lngamma_two=sfuncs.lngamma(2.0);
	  System.Console.Write($"ln(gamma(2)) = {lngamma_two}\n");
	  double lngamma_three=sfuncs.lngamma(3.0);
	  System.Console.Write($"ln(gamma(3)) = {lngamma_three}\n");
	  double lngamma_ten=sfuncs.lngamma(10.0);
	  System.Console.Write($"ln(gamma(10)) = {lngamma_ten}\n\n");
	  double lngamma_mil=sfuncs.lngamma(1000000.0);
	  System.Console.Write($"ln(gamma(1M)) = {lngamma_mil}\n\n");


	  System.Console.Write($"Exp of the above\n");
	  System.Console.Write($"exp(ln(gamma(1))) = {Math.Exp(lngamma_one)}\n");
	  System.Console.Write($"exp(ln(gamma(2))) = {Math.Exp(lngamma_two)}\n");
	  System.Console.Write($"exp(ln(gamma(3))) = {Math.Exp(lngamma_three)}\n");
	  System.Console.Write($"exp(ln(gamma(10))) = {Math.Exp(lngamma_ten)}\n");
	  System.Console.Write($"exp(ln(gamma(1M))) = {Math.Exp(lngamma_mil)}\n");

	  double gamma_neg_one=sfuncs.lngamma(-1.0);
	  System.Console.Write($"gamma(-1) = {gamma_neg_one}\n");

	  
   }
}


