using System;
using static System.Console;
using static System.Math; 

public static class interpolation{
    public static double linterp(double[] x, double[] y, double z){
        int i=binsearch(x,z);
        double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("uups...");
        double dy=y[i+1]-y[i];
        double p =dy/dx;
        return y[i]+p*(z-x[i]);
        }
    
    public static int binsearch(double[] x, double z)
        {/* locates the interval for z by bisection */ 
        if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
        int i=0, j=x.Length-1;
        while(j-i>1){
            int mid=(i+j)/2;
            if(z>x[mid]) i=mid; else j=mid;
            }
        return i;
        }
    public static double linterpInteg(double[] x, double[] y, double z){
        int i = binsearch(x,z);
        double sum = 0;
        double [] dx= new double [x.Length];
        double [] dy= new double [x.Length];
        double [] p = new double [x.Length];

        for(int j=0; j<x.Length-1; j++) {
            dx[j] = x[j+1] - x[j];
            if (!(dx[j]>0)) throw new System.Exception("dx_i not larger than 0");
            dy[j] = y[j+1] - y[j];
            p[j] = dy[j]/dx[j];}

        for(int k = 0; k<=i; k++){
            if(k!=i){
                sum += y[k]*dx[k] + p[k]*dx[k]*dx[k]/2;
            }
            else {
                sum += y[k]*(z-x[i]) + p[k]*(z-x[i])*(z-x[i])/2;
            }
        }
        return sum;
    }

}
