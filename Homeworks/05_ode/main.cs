using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{

	static vector harmonic(double x, vector ys){
		//differential equation u'' = -u can be rewritten as 
		//y2 = u', y1 = u => y1' = y2, y2' = -y1
		return new vector(ys[1],-ys[0]);
	}

	static vector dampened_pendulum(double x, vector ys){
		//assume x is time, y1 is theta and y2 is omega. Assume same damping constants as in python example.
		return new vector(ys[1],-0.25*ys[1]-5*Sin(ys[0]));
	}
	static vector lotkavolterra(double t, vector ys){
        // Lotka-Volterra equation y1 is x and y2 is y.
        double a = 1.5;
        double b = 1.0;
        double c = 3.0;
        double d = 1.0;
        double x = ys[0];
        double y = ys[1];
        double dxdt = a*x - b*x*y;
        double dydt = -c*y + d*x*y;
        return new vector(dxdt, dydt);
    }

	public static void Main(){
		var xlistInit=new genlist<double>();
		var ylistInit=new genlist<vector>();
		vector ya = new vector(0,1);
		string border = new string('-',83);
		WriteLine(border);
		WriteLine("Part A");
		WriteLine("Solving u'' = -u with initial conditions u_0 = 0, u_0' = 1, should return sin(x).");
		WriteLine("The result can be seen in Sine.svg.");
		var points = odesolver.driver(harmonic,0,ya,4*PI,1e-3,1e-3,1e-3,xlistInit, ylistInit);
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
		points = odesolver.driver(dampened_pendulum,0,ya,10,1e-3,1e-3,1e-3,xlistInit, ylistInit);
		xs = points.Item1;
		ys = points.Item2;
		toWrite = "";
		for(int i = 0; i < xs.size; i++){
			toWrite += $"{xs[i]}\t{ys[i][0]}\t{ys[i][1]}\n";
		}
		File.WriteAllText("dampened.txt", toWrite);
		WriteLine(border);
		WriteLine("Part B");
		WriteLine("Driver has been improved with an option to give the endpoints only.");
		WriteLine("The result can be seen in Dampened_end.svg.");
		
		toWrite = "";
		for(int i = 1; i < 10; i++){
			points = odesolver.driver(dampened_pendulum,0,ya,i,1e-3,1e-3,1e-3);
			double x = points.Item1[0];
			double y1 = points.Item2[0][0];
			double y2 = points.Item2[0][1];
			toWrite += $"{x}\t{y1}\t{y2}\n";
		}
		File.WriteAllText("dampened_endpoints.txt", toWrite);

		WriteLine("The Lotka-Volterra system example is recreated and can be seen in LV.svg");
		WriteLine(border);

		toWrite = "";

		var xlist = new genlist<double>();
        var ylist = new genlist<vector>();
        vector lv_ya = new vector(10.0,5.0); // Initial state
        var vt_points = odesolver.driver(lotkavolterra, 0, lv_ya, 15, xlist:xlist, ylist:ylist); // Solve diff eq
		var lv_xs = vt_points.Item1;
		var lv_ys =vt_points.Item2;
        // Save data

		for(int i = 0; i < lv_xs.size; i++){
			toWrite += $"{lv_xs[i]}\t{lv_ys[i][0]}\t{lv_ys[i][1]}\n";
		}
		File.WriteAllText("lv.txt", toWrite);
        
        
    }



}


