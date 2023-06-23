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
        WriteLine("The implicit Heun's stepper is implemented, and selected plots and gifs from Homework");
        WriteLine("05 ode are replicated:\n");
		WriteLine("Solving u'' = -u with initial conditions u_0 = 0, u_0' = 1, should return sin(x).");
		WriteLine("The result can be seen in Sine.svg.\n");
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
		WriteLine("The result can be seen in Dampened.svg.\n");
		points = driver(dampened_pendulum,0,ya,10,1e-1,1e-1,1e-1,xlistInit, ylistInit);
		xs = points.Item1;
		ys = points.Item2;
		toWrite = "";
		for(int i = 0; i < xs.size; i++){
			toWrite += $"{xs[i]}\t{ys[i][0]}\t{ys[i][1]}\n";
		}
		File.WriteAllText("dampened.txt", toWrite);

        WriteLine("Solving three body problem with initial conditions from wikipedia");
		ya = new vector(new double[]{
            0.4662036850,
			0.4323657300,
			-0.93240737,
			-0.86473146,
			0.4662036850,
			0.4323657300,
			-0.97000436,
			0.24308753,
			0,
			0,
			0.97000436,
			-0.24308753});

		xlistInit=new genlist<double>();
		ylistInit=new genlist<vector>();
		points = driver(threebody,0,ya,6.3259,1e-4,1e-4,1e-4,xlistInit, ylistInit);
		xs = points.Item1;
		ys = points.Item2;
		toWrite = $"{xs[0]}\t{ys[0][6]}\t{ys[0][7]}\t{ys[0][8]}\t{ys[0][9]}\t{ys[0][10]}\t{ys[0][11]}\n";
		double currentTime = 0;
		double dt = 0.02;
		for(int i = 0; i < xs.size; i++){
			if(xs[i] > currentTime+dt){
				toWrite += $"{xs[i]}\t{ys[i][6]}\t{ys[i][7]}\t{ys[i][8]}\t{ys[i][9]}\t{ys[i][10]}\t{ys[i][11]}\n";
				currentTime = xs[i];
			}
		}
		File.WriteAllText("threebody.txt", toWrite);
		WriteLine("The result can be seen in Threebody.gif\n");
        WriteLine(border);
	

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
            y_pred_guess[i] = y[i] + h * k1[i]; //Euler as first guess
            y_next[i] = y[i];
        }

        Dictionary<string,object> mydictionary = new Dictionary<string, object>();
        mydictionary.Add("y_old",y_pred_guess);
        mydictionary.Add("x",x);
        mydictionary.Add("h",h);
        mydictionary.Add("f",f);

        y_pred = rootfinder.newton(minimizer,mydictionary); //improve y_pred vector by rootfinding
    
        vector k2 = f(x + h, y_pred);
        vector er = new vector(y.size);

        for (int i = 0; i < n; i++){
                y_next[i] = y[i] + h * (k2+k1)[i]/2;
                er[i]= (y_next[i] - y_pred_guess[i]);
            }
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
		//assume x is time, y1 is theta and y2 is omega. Assume damping constants.
		return new vector(ys[1],-0.25*ys[1]-5*Sin(ys[0]));
	}
    static vector threebody(double x, vector ys){
		double vx_1 = ys[0];
		double vy_1 = ys[1]; 
		double vx_2 = ys[2];
		double vy_2 = ys[3];
		double vx_3 = ys[4];
		double vy_3 = ys[5];
		double xx_1 = ys[6];
		double xy_1 = ys[7];
		double xx_2 = ys[8];
		double xy_2 = ys[9];
		double xx_3 = ys[10];
		double xy_3 = ys[11];
		double d12 = Sqrt( (xx_1-xx_2)*(xx_1-xx_2)+(xy_1-xy_2)*(xy_1-xy_2));
		double d13 = Sqrt( (xx_1-xx_3)*(xx_1-xx_3)+(xy_1-xy_3)*(xy_1-xy_3));
		double d23 = Sqrt( (xx_3-xx_2)*(xx_3-xx_2)+(xy_3-xy_2)*(xy_3-xy_2));
		
        return new vector( new double[] {
			(xx_2 - xx_1)/Pow(d12,3) +(xx_3 - xx_1)/Pow(d13,3),
			(xy_2 - xy_1)/Pow(d12,3) +(xy_3 - xy_1)/Pow(d13,3),
			(xx_1 - xx_2)/Pow(d12,3) +(xx_3 - xx_2)/Pow(d23,3),
			(xy_1 - xy_2)/Pow(d12,3) +(xy_3 - xy_2)/Pow(d23,3),
			(xx_1 - xx_3)/Pow(d13,3) +(xx_2 - xx_3)/Pow(d23,3),
			(xy_1 - xy_3)/Pow(d13,3) +(xy_2 - xy_3)/Pow(d23,3),
			vx_1,
			vy_1,
			vx_2,
			vy_2,
			vx_3,
			vy_3});
    }
}