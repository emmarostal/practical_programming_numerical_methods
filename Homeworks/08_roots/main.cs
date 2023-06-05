using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){
        string border = new string('-',86);
        WriteLine(border);
        WriteLine("Part A");
        WriteLine(border);
        vector v = new vector(12,-10);
 
        WriteLine("We wish to find an extremum of the Rosenback's valley function:\n");
        WriteLine("f(x,y) = (1-x)^2+100(y-x^2)^2.\n");
        WriteLine("This can be done by using the Newton's method routine on the gradient of the function;\n");
        
        WriteLine("df(x,y)/dx = 2x-2+400x^3-400xy");
        WriteLine("df(x,y)/dy = 200y-2x^2.\n");
        WriteLine("Initial guess vector v = (x,y): ");
        v.print();
        WriteLine("");
        WriteLine("Resulting vector x_0: ");
        vector b=newton(Rosen,v);
        b.print();
        vector r = Rosen(b);
        WriteLine("");
        WriteLine("For which the gradient of the Rosenback's valley function is minimised:");
        r.print();
        WriteLine(border);
        WriteLine("Part B");
        WriteLine(border);
        //vector c = new vector(1);
        //c[0]=-1;
        Func<double, double> toMinimize = x => Me(x,0.001,8, 0.01, 0.01);
		double eval = newton(toMinimize,-1,0.01);
        WriteLine("With the shooting method, the binding energy of lowest bound S-electron in H is found");
        WriteLine($"to be {eval} Hartree. Expected value is -0.5 Hartree.");
        WriteLine("The wavefunction corresponding to this eigen-energy is plotted on Hydrogen.svg,");
        WriteLine("along with the analytical solution. They only differ at the very end.");

        genlist<double> rs = new genlist<double>();
		genlist<vector> ys = new genlist<vector>();
		Me(eval,0.001,8, 0.01, 0.01, rs, ys);
		string toWrite = "";
		for(int i = 0; i < rs.size; i++){
			toWrite += $"{rs[i]}\t{ys[i][0]}\t{rs[i]*Exp(-rs[i])}\n";
		}
		File.WriteAllText("wavefunctions.txt",toWrite);
        
        WriteLine("Convergence progress:");
        WriteLine("Varying rmax,");
        toWrite = "";
        for(int i = 0; i < 100; i++){
            double rmax = 6+0.04*(i);
            toMinimize = x => Me(x,0.01,rmax,1e-2, 1e-2);
            eval = newton(toMinimize,-1,1e-4);
            toWrite += $"{rmax}\t{Abs(eval+0.5)/0.5}\n";
        }
        File.WriteAllText("rmax.txt",toWrite);

        WriteLine("Varying absolute accuracy,");
        toWrite = "";
        for(int i = 0; i < 100; i++){
            double absacc = 1e-5*(100 *i+5);;
            toMinimize = x => Me(x,0.01,8, absacc, 1e-4);
            eval = newton(toMinimize,-1,1e-2);
            toWrite += $"{Log10(absacc)}\t{Log10(Abs(eval+0.5)/0.5)}\n";
        }
        File.WriteAllText("absacc.txt",toWrite);

        WriteLine("Varying relative accuracy,");
        toWrite = "";
        for(int i = 0; i < 100; i++){
            double epsacc = 1e-5*(100*i+5);
            toMinimize = x => Me(x,0.01,8, 1e-4, epsacc);
            eval = newton(toMinimize,-1,1e-2);
            toWrite += $"{Log10(epsacc)}\t{Log10(Abs(eval+0.5)/0.5)}\n";
        }
        File.WriteAllText("epsacc.txt",toWrite);

 	toWrite = "";
        WriteLine("Varying rmin,");
        for(int i = 0; i < 1000; i++){
            double rmin = 0.0003*(i+4);
            toMinimize = x => Me(x,rmin,8,1e-4,1e-4);
            eval = newton(toMinimize,-1,1e-4);
            toWrite += $"{rmin}\t{Abs(eval+0.5)/0.5}\n";
        }
        File.WriteAllText("rmin.txt",toWrite);
        WriteLine("Done.");
        WriteLine("The convergence is investigated by means of varying different parameters, one");
        WriteLine("at a time. The resulting plot can be seen in Convergence.svg.");
        WriteLine(border);

    }
    static double newton(Func<double,double> f, double x, double eps=1e-2){
        Func<vector,vector> fvec = z => new vector(f(z[0]));
        vector xvec = new vector(1);
        xvec[0] = x;
        return newton(fvec,xvec,eps)[0];
    }

    static vector newton(Func<vector,vector> f, vector x, double eps=1e-2){
        int n = x.size;
        double dx;
        vector fx;
        matrix jmatrix = new matrix(n,n);
        matrix R = new matrix(n,n);
        vector newx = new vector(n);   
        double magfx = f(x).norm();
        while (magfx > eps){
            dx = x.norm() * Pow(2,-26);
            if (dx == 0) dx = Pow(2,-26);
            fx = f(x);
            for(int i=0; i < n; i++){
                newx = x.copy();
                newx[i] += dx;
                jmatrix[i] = (f(newx) - fx)/dx;
            }
            QRGS.decomp(jmatrix,R);
            newx = QRGS.solve(jmatrix, R, -fx);

            double lambda = 1;
            while(((f(x+lambda*newx)).norm() > (1.0-lambda/2)*fx.norm()) && (lambda > 1.0/32)){
                lambda /= 2;
            }
            x += lambda*newx;
            magfx = f(x).norm();
        }
        return x;
    }

    public static vector f1(vector x){
        vector result = new vector(x.size);
        for(int i=0;i<x.size;i++){
            result[i]+=x[i]*x[i]-4;
        }
        return result;
    }
    public static vector f2(vector x){
        vector result = new vector(x.size);
        for(int i=0;i<x.size;i++){
            result[i]+=x[i]*x[i]-x[i];
        }
        return result;
    }

    public static vector Rosen(vector x){
        vector result = new vector(x.size);
        result[0]=2*x[0]-2+400*Pow(x[0],3)-400*x[0]*x[1]; //dRosen/dx
        result[1]=200*x[1]-2*x[0]*x[0]; //dRosen/dy
        return result;
    }

    public static double Me(double E, double rmin = 0.001, double rmax = 8, double absacc = 0.01, double epsacc = 0.01, genlist<double> rs = null, genlist<vector> ys = null){
                Func<double,vector,vector> schr  = (x,y) => {
                    vector dydr = new vector(2);
                    // diff eq. is f'' = - 2 *(E+1/r)f. If y_1 = f', y_0 = f this gives
                    // y_0' = y_1
                    // y_1' = -2*(E+1/r)*y_0
                    //double E_0 = E[0];
                    dydr[0] = y[1];
                    dydr[1] = -2*(E+1/x)*y[0];
                    return dydr;
                };

                //initial conditions; at r_min, y0 = f(rmin) = r_min-r_min^2, y1 = f' = 1-2*r_min
                vector init = new vector(2);
                init[0] = rmin - rmin*rmin;
                init[1] = 1-2*rmin;
                (genlist<double> xmax, genlist<vector> ymax) = odesolver.driver(schr, rmin, init, rmax, 0.01, absacc, epsacc, rs, ys);
                return ymax[0][0]; //final point in the solution (at rmax), this is what should be zero if we find correct E. 
        

    }

}