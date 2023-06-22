using System;
using static System.Console;
using static System.Math; 
using System.Collections.Generic;

public static class rootfinder{

    //public static double newton(Func<Dictionary<string,object>,double> f, Dictionary<string,object> dict, double eps=1e-2){
            
            //Func<vector,vector> fvec = z => new vector(f(z[0]));
            //vector xvec = new vector(1);
            //xvec[0] = x;
            //return newton(fvec,xvec,eps)[0];
           
            
            //Func<Dictionary<string,object>,vector> fvec = z => new vector(f(dict,z[0]));
            //vector xvec = new vector(1);
            //xvec[0] = x;
            //return newton(fvec,xvec,eps)[0];
       //}

    public static vector newton(Func<Dictionary<string,object>,vector> f, Dictionary<string,object> dict, double eps=1e-2){
        vector x = (vector)dict["y_old"];
        int n = x.size;
        double dx;
        vector fx=f(dict);
        matrix jmatrix = new matrix(n,n);
        matrix R = new matrix(n,n);
        vector newx = new vector(n);   
        double magfx = fx.norm();
        while (magfx > eps){
            dx = x.norm() * Pow(2,-26);
            if (dx == 0) dx = Pow(2,-26);
            fx = f(dict);
            for(int i=0; i < n; i++){
                newx = x.copy();
                newx[i] += dx;
                dict["y_old"]=newx;
                jmatrix[i] = (f(dict) - fx)/dx;
            }
            QRGS.decomp(jmatrix,R);
            newx = QRGS.solve(jmatrix, R, -fx);

            double lambda = 1;
            dict["y_old"]=x+lambda*newx;
            while(((f(dict)).norm() > (1.0-lambda/2)*fx.norm()) && (lambda > 1.0/32)){
                lambda /= 2;
                dict["y_old"]=x+lambda*newx;
            }
            x += lambda*newx;
            dict["y_old"]=x;
            magfx = f(dict).norm();
        }
        return x;
    }
}