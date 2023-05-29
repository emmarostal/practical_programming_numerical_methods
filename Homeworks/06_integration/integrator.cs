using System;
using static System.Console;
using static System.Math; 

public static class integrator{
    public static int evals=0;
    public static double integrate(Func<double,double> f, double a, double b,
    double δ=0.001, double ε=0.001, double f2 = double.NaN, double f3 = double.NaN){
    double h=b-a;
    if(double.IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6);evals = 0; } // first call, no points to reuse
    double f1=f(a+h/6), f4=f(a+5*h/6);
    evals += 1;
    double Q = (2*f1+f2+f3+2*f4)/6*(b-a); // higher order rule
    double q = (f1+f2+f3+f4)/4*(b-a); // lower order rule
    double err = Abs(Q-q);
    if (err <= δ+ε*Abs(Q)) return Q;
    else return integrate(f,a,(a+b)/2,δ/Sqrt(2),ε,f1,f2)+
                integrate(f,(a+b)/2,b,δ/Sqrt(2),ε,f3,f4);
        }

    

    public static double integrateClenCur(Func<double,double> f, double a, double b, double delta=0.001, double eps=0.001, double f2=double.NaN, double f3=double.NaN){
            Func<double,double> ftransformed = x => f((a+b)/2 + (b-a)/2*Cos(x)) * Sin(x)*(b-a)/2;
            return integrate(ftransformed, 0, PI, delta, eps, f2, f3);
        }
}