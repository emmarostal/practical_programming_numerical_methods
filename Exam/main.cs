using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){
        var xlistInit=new genlist<double>();
		var ylistInit=new genlist<vector>();
		vector ya = new vector(0,1);
		string border = new string('-',83);
		WriteLine(border);
		WriteLine("Part A");
		WriteLine("Solving u'' = -u with initial conditions u_0 = 0, u_0' = 1, should return sin(x).");
		WriteLine("The result can be seen in Sine.svg.");
		var points =driver(harmonic,0,ya,4*PI,1e-1,1e-1,1e-1,xlistInit, ylistInit);
		var xs = points.Item1;
		var ys = points.Item2;
		string toWrite = "";
		for(int i = 0; i < xs.size; i++){
			toWrite += $"{xs[i]}\t{ys[i][0]}\t{ys[i][1]}\n";
		}
		File.WriteAllText("harmonic_diff.txt", toWrite);
        
        xlistInit=new genlist<double>();
		ylistInit=new genlist<vector>();
		ya = new vector(PI-0.1,0);
		WriteLine("Solving dampened pendulum with same parameters as in scipy.integrate.odeint manual.");
		WriteLine("The result can be seen in Dampened.svg.");
		points = driver(dampened_pendulum,0,ya,10,1e-1,1e-1,1e-1,xlistInit, ylistInit);
		xs = points.Item1;
		ys = points.Item2;
		toWrite = "";
		for(int i = 0; i < xs.size; i++){
			toWrite += $"{xs[i]}\t{ys[i][0]}\t{ys[i][1]}\n";
		}
		File.WriteAllText("dampened.txt", toWrite);
    }

    static (vector,vector) HeunsImplicitStep(Func<double,vector,vector> f,double x, vector y, double h)
    {
        // Perform one step of Heun's implicit method
        int n = y.size;
        vector k1 = f(x, y);
        vector y_pred = new vector(n);
        vector y_pred_guess = new vector(n);
        vector y_next = new vector(n);

        for (int i = 0; i < n; i++)
        {
            y_pred_guess[i] = y[i] + h * k1[i]; //change y_pred here by root-finding
            y_next[i] = y[i];
        }

        Dictionary<string,object> mydictionary = new Dictionary<string, object>();
        mydictionary.Add("y_old",y_pred_guess);
        mydictionary.Add("x",x);
        mydictionary.Add("h",h);
        mydictionary.Add("f",f);
        y_pred_guess.print();
        WriteLine("Starting minimization...");

        WriteLine($"x = {x}, h = {h}");

        y_pred = rootfinder.newton(minimizer,mydictionary);
        WriteLine("Minimization finished");
        y_pred.print();
    
        //y_pred = y_pred_guess;
        double tolerance = 1e-3;
        double delta = 1.0;

        vector k2 = f(x + h, y_pred);
        vector er = new vector(y.size);
        for (int i = 0; i < n; i++){
                y_next[i] = y[i] + h * (k2+k1)[i]/2;
                er[i]= (y_next[i] - y_pred_guess[i]);
                //delta = y_next[i] - y_pred_guess[i];
                //y_pred[i] = y_next[i];
            }
        //while (Math.Abs(delta) > tolerance)
        //{
            
            
            
        //}
        return (y_next,er);
    }
    
public static (genlist<double>,genlist<vector>) driver(
        Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
        double a,                    /* the start-point a */
        vector ya,                   /* y(a) */
        double b,                    /* the end-point of the integration */
        double h=0.01,               /* initial step-size */
        double acc=0.01,             /* absolute accuracy goal */
        double eps=0.01,              /* relative accuracy goal */
        genlist<double> xlist=null, 
        genlist<vector> ylist=null
    ){
        if(a>b) throw new ArgumentException("driver: a>b");
        double x=a; vector y=ya.copy();
        if(!(xlist == null || ylist == null)){
            xlist.add(x);
            ylist.add(y);
            do{
                if(x>=b) return (xlist,ylist); /* job done */
                if(x+h>b) h=b-x;               /* last step should end at b */
                var (yh,erv) = HeunsImplicitStep(f,x,y,h);
                vector tol = new vector(yh.size);
                for(int i=0;i<yh.size;i++){
                    tol[i]=Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
                }
                bool ok=true;
                for(int i=0;i<yh.size;i++){
                    if(! (erv[i]<tol[i])) ok=false;
                }
                if(ok){ // accept step
                    x+=h; y=yh;
                    xlist.add(x);
                    ylist.add(y);
                }
                double factor = tol[0]/Abs(erv[0]);
                for(int i=1;i<y.size;i++){
                    factor=Min(factor,tol[i]/Abs(erv[i]));
                }
                h *= Min( Pow(factor,0.25)*0.95 ,2); // readjust stepsize
            }while(true);
        }
        else{
            do{
                if(x>=b){
                    xlist=new genlist<double>();
		            ylist=new genlist<vector>();
                    xlist.add(x);
                    ylist.add(y);
                    return (xlist,ylist);/* job done */
                }
                if(x+h>b) h=b-x;               /* last step should end at b */
                var (yh,erv) = HeunsImplicitStep(f,x,y,h);
                vector tol = new vector(yh.size);
                for(int i=0;i<yh.size;i++){
                    tol[i]=Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
                }
                bool ok=true;
                for(int i=0;i<yh.size;i++){
                    if(! (erv[i]<tol[i])) ok=false;
                }
                if(ok){ // accept step
                    x+=h; y=yh;
                }
                double factor = tol[0]/Abs(erv[0]);
                for(int i=1;i<y.size;i++){
                    factor=Min(factor,tol[i]/Abs(erv[i]));
                }
                h *= Min( Pow(factor,0.25)*0.95 ,2); // readjust stepsize
            }while(true);
        }
    }//driver

    public static vector minimizer(Dictionary<string,object> a){ //newton(func_to_minimize,guess_from_newton)
        
        double x = (double)a["x"];
        double h = (double)a["h"];
        vector y_old = (vector)a["y_old"];
        vector y = new vector(y_old.size);
        Func<double,vector,vector> f =(Func<double,vector,vector>) a["f"];
        vector result = new vector(y_old.size);
        for(int i=0;i<y_old.size;i++){
            result[i]=y_old[i]-y[i]+h/2*(f(x,y_old)[i]+f(x+h,y)[i]);
        }
        return result;
    }
    static vector harmonic(double x, vector ys){
		//differential equation u'' = -u can be rewritten as 
		//y2 = u', y1 = u => y1' = y2, y2' = -y1
		return new vector(ys[1],-ys[0]);
	}
    static vector dampened_pendulum(double x, vector ys){
		//assume x is time, y1 is theta and y2 is omega. Assume same damping constants as in python example.
		return new vector(ys[1],-0.25*ys[1]-5*Sin(ys[0]));
	}
}