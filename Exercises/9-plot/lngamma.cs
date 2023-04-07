using System;
using static System.Math;

public static partial class sfuns{

    public static double lngamma(double x){
    if(x<=0) throw new ArgumentException("lngamma: x<=0");
    if(x<9) return lngamma(x+1)-Log(x);
    return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
    }

}