using System;
using static System.Console;
using static System.Math;
public class vec{
//Constructors
    public double x,y,z;
    public vec (){x=y=z=0;}
    public vec (double a,double b,double c){x=a ; y=b ; z=c;}
//Methods
    public void print(string s){Write(s);WriteLine($"{x} {y} {z}");}
    public void print(){this.print("");}
//Operators
    public static vec operator+(vec u,vec v){ /* u+v */
            return new vec(u.x+v.x,u.y+v.y,u.z+v.z);
            }
    public static vec operator*(vec u,double c){ /* u*c */
            return new vec(u.x*c,u.y*c,u.z*c);
            }
    public static vec operator*(double c,vec u){ /* c*u */
            return new vec(u.x*c,u.y*c,u.z*c);
            }
    public static vec operator-(vec u,vec v){ /* u-v */
            return new vec(u.x-v.x,u.y-v.y,u.z-v.z);
	    }
    public static vec operator-(vec v){ /* -v */
	    return new vec(-v.x,-v.y,-v.z);
	    }
    public double dot(vec other) /* to be called as u.dot(v) */
	    {return this.x*other.x+this.y*other.y+this.z*other.z;
            }
    public static double dot(vec v,vec w){ /* to be called as vec.dot(u,v) */
	    return v.x*w.x+v.y*w.y+v.z*w.z;
            }
    public vec cross(vec other){ /* to be called as u.cross(v) */
	    return new vec(
            this.y*other.z-this.z*other.y,
            this.z*other.x-this.x*other.z,
            this.x*other.y-this.y*other.x);
            }
    public static vec cross(vec v,vec w){ /* to be called as vec.cross(u,v) */
	    return new vec(
                v.y*w.z-v.z*w.y,
                v.z*w.x-v.x*w.z,
                v.x*w.y-v.y*w.x);
            }
    public static double norm(vec v){ /* to be called as vec.norm(v) */
	    return Math.Sqrt(v.x*v.x+v.y*v.y+v.z*v.z);
            }
    //approx method
    static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){
	if(Abs(a-b)<acc)return true;
	if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
	return false;
	}
    public bool approx(vec other){
	if(!approx(this.x,other.x))return false;
	if(!approx(this.y,other.y))return false;
	if(!approx(this.z,other.z))return false;
	return true;
	}
    public static bool approx(vec u, vec v) => u.approx(v);
    // Overwrite ToString
    public override string ToString(){ return $"{x} {y} {z}"; }
    


}