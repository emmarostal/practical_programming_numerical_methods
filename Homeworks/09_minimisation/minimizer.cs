using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class minimizer{
    public static vector fit(
        Func<vector,double> f,
        vector guesses,
        List<double> xs,
        List<double> ys,
        List<double> yerrs,
        double acc
    ){
        Func<vector,double> toMin = par => {
            //assume first parameter to f is x-value. 
            double sum = 0;
            vector parvector = new vector(guesses.size+1);
            for(int i = 0; i < xs.Count; i++){
                parvector[0] = xs[i];
                for(int j = 0; j < guesses.size; j++){
                    parvector[j+1] = par[j];
                }
                sum += Pow( (f(parvector)-ys[i])/ yerrs[i] ,2);
            }
            return sum;
        };
        //D(m,Γ,A)=Σi[(F(Ei|m,Γ,A)-σi)/Δσi]2 .
        vector fitparams = qnewton(toMin,guesses,acc).Item1;
        return fitparams;
    }

public static Tuple<vector,int> qnewton(
	Func<vector,double> f, /* objective function */
	vector start, /* starting point */
	double acc = 1e-4,/* accuracy goal, on exit |gradient| should be < acc */
    int nsteps = 0
    ){ 
        int n = start.size;
        vector x = start.copy();
        vector grad = new vector(n);
        vector deltax = new vector(n);
        vector s = new vector(n);
        vector u = new vector(n);
        vector y = new vector(n);
        matrix deltaB = new matrix(n,n);
        matrix B = new matrix(n,n);
        B.set_identity();
        double lambda = 1.0;
        grad = gradient(f, x);

        while(grad.norm() > acc){
            nsteps++;
            deltax = -B*grad;
            lambda = 1.0;
            while(true){
                s = lambda*deltax;
                if (f(x+s) < f(x)){ // accept step and update B
                    x = x + s;
                    vector oldGrad = grad;
                    grad = gradient(f, x);
                    // Update B
                    y = grad - oldGrad;
                    u = s - B*y;
                    // deltaB * y = u  =>  deltaB * y * u = u * u 
                    deltaB = matrix.outer(u,u)/(u.dot(y));
                    B += deltaB;
                    break;
                }
                lambda = lambda/2;
                if (lambda < 1.0/Pow(2,16)){ // accept step and reset B
                    x = x + s;
                    grad = gradient(f, x);
                    B.set_identity();
                    break;
                }
            }
        }
    Tuple<vector,int> res = new Tuple<vector,int>(x,nsteps);
    return res;
    }
static vector gradient(Func<vector,double> f, vector x){
        int dim = x.size;
        vector grad = new vector(dim);
        vector newx = x.copy();
        for(int i = 0; i<dim; i++){
            double dx = Abs(x[i])*Pow(2,-26);
            newx[i] = x[i] + dx;
            grad[i] = (f(newx) - f(x))/dx;
        }
        return grad;
    }

    



}