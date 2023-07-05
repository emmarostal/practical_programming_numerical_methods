using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;
public class ann{
    public int n; /* number of hidden neurons */
    public Func<double,double> f = x => x*Exp(-x*x); /* activation function */
    public Func<double,double> firstD = x => (1-2*x*x)*Exp(-x*x); /* derivative of activation function */
    public Func<double,double> secondD= x => Exp(-x*x)*(4*x*x*x-6*x); /* 2nd derivative of activation function */
    public Func<double,double> antiD = x => -0.5*Exp(-x*x); /* first antiderivative of activation function */
    public vector p; /* network parameters */
    public int nsteps;
    public ann(int n){
        this.n = n; 
        int numParams = 3*n; //n a_i, n b_i, n w's
        p = new vector(numParams);
    }
    public double response(double x){
        double result = 0;
        for(int i = 0; i < n; i++){
            double a_i = p[i];
            double b_i = p[n+i];
            double w_i = p[2*n+i];
            result += w_i * f( (x-a_i)/b_i );
        }
        return result;
    }
    public double firstDResponse(double x){
        /* return the first derivative of the response of the network to the input signal x */
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * firstD((x-ai)/bi)/bi;
        }
        return result;
    }

    public double secondDResponse(double x){
        /* return the second derivative of the response of the network to the input signal x */
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * secondD((x-ai)/bi)/(bi*bi);
        }
        return result;
    }
    public double antiDResponse(double x){
        /* return the anti derivative of the response of the network to the input signal x */
        double result = 0;
        for(int i = 0; i<n; i++){
            double ai = p[i];
            double bi = p[i+n];
            double wi = p[i+2*n];
            result += wi * bi * (antiD((x-ai)/bi));
        }
        return result;
    }
    public double integral(double a, double b){
        /* return the anti derivative of the response of the network to the input signal between two x-values */
        return antiDResponse(b) - antiDResponse(a);
    }
   public void train(vector x,vector y){
        /* train the network to interpolate the given table {x,y} */
        /*initialize parameter vector randomly*/
        var rand = new Random();
        for (int i = 0; i < p.size; i++){
            p[i] = rand.NextDouble() - 0.5;
        }
        Func<vector,double> c = ps => {
            double toMin = 0; 
            for(int j = 0; j < x.size; j++){
                double result = 0; 
                for(int i = 0; i < n; i++){
                    double a_i = ps[i];
                    double b_i = ps[n+i];
                    double w_i = ps[2*n+i];
                    result += w_i * f( (x[j]-a_i)/b_i );
                }
                toMin += Pow(result-y[j],2);
            }
            return toMin/n;
        };
        p = minimizer.qnewton(c,p,0.001).Item1;
    
   }
}