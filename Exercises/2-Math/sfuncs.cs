using System;

public static class sfuncs
{
    public static double gamma(double x){
///single precision gamma function (formula from Wikipedia)
        if(x<0)return Math.PI/Math.Sin(Math.PI*x)/gamma(1-x); // Euler's reflection formula
        if(x<9)return gamma(x+1)/x; // Recurrence relation
        double lngamma=x*Math.Log(x+1/(12*x-1/x/10))-x+Math.Log(2*Math.PI/x)/2;
        return Math.Exp(lngamma);
}
    public static double lngamma(double x){
///single precision gamma function (formula from Wikipedia)
        if(x<0)return 0; // Euler's reflection formula
        if(x<9)return Math.Log(gamma(x)); // Recurrence relation
        double mylngamma=x*Math.Log(x+1/(12*x-1/x/10))-x+Math.Log(2*Math.PI/x)/2;
        return mylngamma;
}
	
}