using System;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{
    public static void Main(){
        string border = new string('-',55);
        WriteLine("Part A");
        WriteLine(border);
        WriteLine("An indicative plot of the linear spline and integrator");
        WriteLine("can be seen in linear.svg");
        WriteLine(border);
        double[] xs = new double[]{0,1,2,3,4,5,6,7,8,9,10};
        double[] ys = new double[xs.Length];
        double[] zs = new double[]{0.5, 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5, 9.9};
        
        double[] lys = new double[zs.Length];
        double[] liys = new double[zs.Length];
        string toWrite = "";
        for(int i = 0; i<zs.Length; i++){
            ys[i] = Sin(xs[i]);}


        for(int i = 0; i<zs.Length; i++){
            lys[i] = interpolation.linterp(xs,ys,zs[i]);
            liys[i] = interpolation.linterpInteg(xs,ys,zs[i]);
            toWrite += $"{xs[i]} {ys[i]} {zs[i]} {lys[i]} {liys[i]}\n";
        }
        File.WriteAllText("linterp.txt", toWrite);
        
        
        
        WriteLine("Part B, shifting to object oriented programming.");
        WriteLine(border);
        WriteLine("An indicative plot of the quadratic spline and can");
        WriteLine("be seen in quadratic.svg, where three different");
        WriteLine("functions are used. If the b and c values were to be ");
        WriteLine("manually calculated, we would get");
        WriteLine($"for y = 1:        b_i = 0     c_i = 0");
        WriteLine($"for y = x:        b_i = 1     c_i = 0");
        WriteLine($"for y = x^2:      b_i = 2x_i  c_i = 1");
        
        
        WriteLine("In qsine.svg quadratic interpolation is made for sin(x).");
        int l = 11;
        int s = 6;

        vector vxs = new vector(l); //initialization
        vector vxs4 = new vector(s);

        vector vys1 = new vector(l);
        vector vys2 = new vector(l);
        vector vys3 = new vector(l);
        vector vys4 = new vector(s);

        vector vzs = new vector(l);
        vector vzs4 = new vector(s);

        vector qys1 = new vector(l);
        vector qys2 = new vector(l);
        vector qys3 = new vector(l);
        vector qys4 = new vector(s);

        vector qiys1 = new vector(l);
        vector qiys2 = new vector(l);
        vector qiys3 = new vector(l);
        vector qiys4 = new vector(s);

        string qWrite = "";
        string qsineWrite = "";

        for(int i=0;i<=l-1;i++){// Fill in the vectors
            vxs[i]=i;
            vys1[i]=1;
            vys2[i]=i;
            vys3[i]=i*i;
            vzs[i]=zs[i];
        }
        for(int i=0;i<=s-1;i++){
            vys4[i]=Sin(i);
            vxs4[i]=i;
            vzs4[i]=zs[i]/1.2;
        }

        qspline spline1 = new qspline(vxs, vys1); //spline away!
        qspline spline2 = new qspline(vxs, vys2);
        qspline spline3 = new qspline(vxs, vys3);
        qspline spline4 = new qspline(vxs4, vys4);
        WriteLine("For the interpolations made in quadratic svg, this");
        WriteLine("routine calculates the following values:");
        WriteLine("x_i:");
        spline1.x.print();
        WriteLine(border+border);
        WriteLine($"for y = 1:");
        WriteLine($"b_i");
        spline1.b.print();
        WriteLine($"c_i");
        spline1.c.print();
        WriteLine(border+border);
        WriteLine($"for y = x:");
        WriteLine($"b_i");
        spline2.b.print();
        WriteLine($"c_i");
        spline2.c.print();
        WriteLine(border+border);
        WriteLine($"for y = x^2:");
        WriteLine($"b_i");
        spline3.b.print();
        WriteLine($"c_i");
        spline3.c.print();
        WriteLine(border+border);
        WriteLine("Which is in fine agreement with the manually calculated values.");

        
    

        for(int i = 0; i<vzs.size; i++){// evaluate
            qys1[i] = spline1.evaluate(vzs[i]);
            qys2[i] = spline2.evaluate(vzs[i]);
            qys3[i] = spline3.evaluate(vzs[i]);
            
            qiys1[i] = spline1.integral(vzs[i]);
            qiys2[i] = spline2.integral(vzs[i]);
            qiys3[i] = spline3.integral(vzs[i]);
            
            qWrite+=$"{vxs[i]} {vys1[i]} {vys2[i]} {vys3[i]} {vzs[i]} {qys1[i]} {qys2[i]} {qys3[i]} {qiys1[i]} {qiys2[i]} {qiys3[i]} \n";
        }
        for(int i = 0;i<=s-1;i++){
            qys4[i] = spline4.evaluate(vzs4[i]);
            qiys4[i] = spline4.integral(vzs4[i]);
            qsineWrite+=$"{vxs4[i]} {vys4[i]} {vzs4[i]} {qys4[i]} {qiys4[i]}\n";

        }
        File.WriteAllText("qinterp.txt", qWrite);
        File.WriteAllText("qsine.txt", qsineWrite);
    }





}