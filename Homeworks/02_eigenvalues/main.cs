using System;
using static System.Console;
using static System.Math; 

public static class main{

	public static void Main(string[] args){

        string type = null;

        foreach(var arg in args){
            var words = arg.Split(':');
            if(words[0] == "-type"){
            type = words[1];
        }
    }
        if(type == "rmax"){
            for(double x = 1.5 ; x >= 0.01 ; x -= 1.0/64){
                    WriteLine($"{x} {BuildHam(10,x).Item1[0,0]}");
                 }
        }
        if(type == "dr"){
            for(double x = 2 ; x <= 12 ; x += 1.0/64){
                    WriteLine($"{x} {BuildHam(x,0.1).Item1[0,0]}");
                }
        }

        if(type == "radial"){
            for(int x = 0 ; x <= 2 ; x += 1){
                double r = 0;
                double dr = 0.1;
                WriteLine($"{x+1}s-wave");
                matrix radialW = BuildHam(30,dr).Item2;
                for(int y = 0;y<=radialW.size1-1; y++){
                    WriteLine($"{r} {Pow(1/Sqrt(dr)*radialW[y,x],2)}");
                    r+=dr;
                }
                WriteLine("\n\n");    
                }
                WriteLine("Analytical");
                double r1 = 0;
                double dr1 = 0.1;
                matrix radialW1 = BuildHam(30,dr1).Item2;
                for(int y = 0;y<=radialW1.size1-1; y+=1){
                double analytical1 =1.25*Pow(r1/Sqrt(PI*dr1)*Exp(-(r1)),2);
                WriteLine($"{r1} {analytical1}");
                r1+=dr1;
        }WriteLine("\n\n");   
        double r2 = 0;
        double dr2 = 0.1;
        for(int y = 0;y<=radialW1.size1-1; y+=1){
                double analytical2 =1.25*Pow(r2/Sqrt(32*PI*dr2)*(2-r2)*Exp(-r2/2),2);
                WriteLine($"{r2} {analytical2}");
                r2+=dr2;
        }WriteLine("\n\n");   
        double r3 = 0;
        double dr3 = 0.1;
        for(int y = 0;y<=radialW1.size1-1; y+=1){
                double analytical3 =1.25*Pow(r3/(81*Sqrt(3*PI*dr3))*(27-18*r3+2*r3*r3)*Exp(-r3/3),2); 
                WriteLine($"{r3} {analytical3}");
                r3+=dr3;
        }
        }

        if(type == ""){
        var rnd = new Random();
        string border = new string('-',100);
        WriteLine(border);
        WriteLine("A Jacobi diagonalization with cyclic sweeps");
        WriteLine(border);
        
        int size = 8;
        matrix A = new matrix(size,size);
        matrix V = new matrix(size,size);
        for(int i = 0; i < size; i++){
			V[i,i] = 1;
			for(int j = i; j < size; j++){
                int value= rnd.Next(0,10);
                A[i,j]=value;
                A[j,i]=value;
            }
        

    }
    WriteLine($"Randomly generated {size} x {size} symmetric matrix A:");
    A.print();
    WriteLine(border);
    matrix A_i = A.copy();

    jacobi.cyclic(A,V);
	WriteLine("D is a diagonal matrix with the eigenvalues:");
	A.print();
    WriteLine(border);

    WriteLine("V is the orthogonal matrix of the eigenvectors:");
	V.print();
    WriteLine(border);

    WriteLine("VDV^{T} is equal to the initial matrix A");
    matrix VT = V.transpose();
    matrix VDVT = V*A*VT;
    VDVT.print();
    WriteLine(border);

    WriteLine("V^{T}AV is equal to the matrix D");
    matrix VTAV = VT*A_i*V;
    VTAV.print();
    WriteLine(border);

    WriteLine("V^{T}V is the identity matrix");
    matrix VTV = VT*V;
    VTV.print();
    WriteLine(border);


    WriteLine("VV^{T} is also the identity matrix");
    matrix VVT = V*VT;
    VVT.print();
    WriteLine(border);
    }   
}

public static Tuple<matrix,matrix> BuildHam(double rmax,double dr){
    int npoints = (int)(rmax/dr)-1;
    vector r = new vector(npoints);
    for(int i=0;i<npoints;i++){
        r[i]=dr*(i+1);
    }
    matrix H = new matrix(npoints,npoints);
    for(int i=0;i<npoints-1;i++){
        H[i,i]  =-2;
        H[i,i+1]= 1;
        H[i+1,i]= 1;
    }
    H[npoints-1,npoints-1]=-2;
    double scalar = -0.5/(dr*dr);
    matrix.scale(H,scalar);
    for(int i=0;i<npoints;i++){
        H[i,i]+=-1/r[i];
    }
    matrix W = new matrix(H.size1,H.size2);
    for(int i = 0; i < H.size1; i++){
			W[i,i] = 1;
    }
    jacobi.cyclic(H,W);
    return Tuple.Create(H,W);

}
}