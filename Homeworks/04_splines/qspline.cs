using System;
using static System.Math;
public class qspline{
    public vector x,y,b,c;
    double[] p,dx,dy;
    public qspline(vector xs, vector ys){
        if (xs.size != ys.size) throw new ArgumentException("x and y arrays must have same length");
        if (xs.size < 2) throw new ArgumentException("x and y arrays must have at least two elements");
        for (int i = 1; i < xs.size; i++) {
            if (xs[i] <= xs[i - 1]) throw new ArgumentException("x array must be strictly increasing");
        }
        this.x = xs;
        this.y = ys;
        this.b = new vector(xs.size-1);
        this.c = new vector(xs.size-1);
        // Vector c
        c[0] = 0;
        this.p = new double[x.size-1];
        this.dx = new double[x.size-1];
        this.dy = new double[x.size-1];
        // Forwards recursion
        for(int i=0; i<x.size-1; i++) {
            dx[i] = x[i+1] - x[i];
            if (!(dx[i]>0)) throw new System.Exception("dx_i not larger than 0");
            dy[i] = y[i+1] - y[i];
            p[i] = dy[i]/dx[i];
        }
        for(int i = 0; i<x.size-2; i++){
            c[i+1] = (p[i+1]-p[i]-c[i]*dx[i])/dx[i+1];
        }
        c[x.size-2] /= 2; // In order to average both recursions (see note)
        // Backwards recursion
        for(int i = x.size-3; i>=0; i--){
            c[i] = (p[i+1] - p[i] - c[i+1]*dx[i+1])/dx[i];

        }
        // Vector b
        for(int i = 0; i<b.size; i++){
            b[i] = p[i] - c[i]*dx[i];
        }
    }


    public double evaluate(double z){
        int i = binsearch(x,z);
        return y[i] + b[i]*(z-x[i])+c[i]*(z-x[i])*(z-x[i]);
    }

    public double integral(double z){
        int i = binsearch(x,z);
        double sum = 0;
        for(int j = 0; j<=i; j++){
            if(j!=i){ // Every normal step
                sum += y[j]*dx[j] + b[j]*dx[j]*dx[j]/2 + c[j]*Pow(dx[j],3)/3;    
            }
            else{ //The last "half step"  
                sum += y[i]*(z-x[i]) + b[i]*(z-x[i])*(z-x[i])/2 + c[i]*Pow((z-x[i]),3)/3;
            }
        }
        return sum;
    }

    public double derivative(double z){
        int i = binsearch(x,z);
        return b[i] + 2.0*c[i]*(z-x[i]);
    }

    public static int binsearch(vector x, double z){/* locates the interval for z by bisection */ 
        if(!(x[0]<=z && z<=x[x.size-1])) throw new Exception("binsearch: bad z");
        int i=0, j=x.size-1;
        while(j-i>1){
            int mid=(i+j)/2;
            if(z>x[mid]) i=mid; else j=mid;
            }
        return i;
	}

}