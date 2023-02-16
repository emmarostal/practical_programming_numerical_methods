using System;
using static System.Console;
using static vec;


public static class main{
    public static void print(this double x,string s){ /* x.print("x="); */
	Write(s);WriteLine(x);
	}
    public static void print(this double x){ /* x.print() */
	x.print("");
	}
    public static void Main(){
        vec v = new vec(2,3,4);
        vec u = new vec(1,2,3);
        u.print("u  = ");
        v.print("v  = ");
		(u+v).print("u + v = ");
		(2*u).print("2 * u = ");
        vec w=u*2;
		w.print("u * 2 = ");
        vec w2=u+6*v-w;
		w2.print("w2 = ");
        (-u).print("-u=");
        vec m = new vec();
        m.print("m = ");
        WriteLine($"u dot v  = {dot(u,v)}");
		WriteLine($"u.dot(v) = {u.dot(v)}");
        vec k=cross(u,v);
        k.print("u X v = ");
        vec l=u.cross(v);
        l.print("u.cross(v) = ");
        WriteLine($"norm(v)  = {norm(v)}");
        WriteLine($"norm(u)  = {norm(u)}");
        WriteLine($"norm(w2)  = {norm(w2)}");
        WriteLine($"u approx v  = {approx(u,v)}");
        vec a = new vec(1+2e-9,2-1e-9,3);
        vec b = new vec(1,2,3);
        a.print("a = ");
        b.print("b = ");
        WriteLine($"a approx b  = {approx(a,b)}");



    }

}