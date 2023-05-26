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
    }





}