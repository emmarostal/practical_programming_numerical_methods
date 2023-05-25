using System;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{

	public static void Main(){
        var rnd = new Random();
        string border = new string('-',70);
		WriteLine(border);
        WriteLine("Decomp test:");
        // Create a random n x m (m<=n) matrix
        // A minimum 2 x 2 matrix

        matrix A = new matrix(8,4);
        int n = A.size1;
        int m = A.size2;
        for(int j=0;j<m;j++){
            for(int i=0;i<n;i++){
                A[i,j]+=rnd.Next(0,10);
            }
        }
        matrix R = new matrix(A.size2,A.size2);
        WriteLine($"n = {n}, m = {m}");
        WriteLine(border);
        WriteLine("The A matrix is:");
        A.print();
        WriteLine(border);
        QRGS.decomp(A,R);
		WriteLine("After QR decomposition, the Q matrix is:");
		A.print();
        WriteLine(border);
		WriteLine("After QR decomposition, R is upper triangular:");
        R.print();
        WriteLine(border);
        WriteLine("Q^TQ should return identity:");
        (A.transpose()*A).print();
        WriteLine(border);
        WriteLine("QR is equal to A:");
        matrix QR = A*R;
        QR.print();
        WriteLine(border);


        WriteLine("Solve test:");
        matrix B = new matrix(5,5);
        int n2 = B.size1;
        int m2 = B.size2;
        for(int j=0;j<m2;j++){
            for(int i=0;i<n2;i++){
                B[i,j]+=rnd.Next(1,10);
            }
        }
        WriteLine($"n = {n2}, m = {m2}");
        WriteLine(border);
        vector b = new vector(n2);
        for(int i=0;i<n2;i++){
            b[i]+=rnd.Next(0,10);
        }
        WriteLine("The random square A matrix is:");
        B.print();
        WriteLine(border);
        WriteLine("The random b vector is:");
        b.print();
        WriteLine(border);
        matrix R2 = new matrix(n2,n2);
        QRGS.decomp(B,R2);
        WriteLine("After QR decomposition, the Q matrix is:");
		B.print();
        WriteLine(border);
        WriteLine("After QR decomposition, R is upper triangular:");
        R2.print();
        WriteLine(border);
        WriteLine("The solution x to the equation QRx = b is found to be:");
        vector x = QRGS.solve(B,R2,b);
        x.print();
        WriteLine(border);
        WriteLine("The found x solves the equation Ax=b and Ax returns the b vector:");
        vector BRx = B*R2*x;
        (BRx).print();
        WriteLine(border);

        WriteLine("Det test:");
        WriteLine("The determinant of a simple 2x2 matrix can be found by taking the product");
        WriteLine("of the diagonals and subtracting the product of the off-diagonals. Using");
        WriteLine("this approach, the determinant of the matrix  A printed below is found.");
        WriteLine("Using the QR factorisation and the det function from this project, the");
        WriteLine("determinant of the R-matrix is found:");
        matrix C = new matrix(2,2);
        int n3 = C.size1;
        int m3 = C.size2;
        for(int j=0;j<m3;j++){
            for(int i=0;i<n3;i++){
                C[i,j]+=rnd.Next(1,10);
            }
        }
        C.print();
        double a11 = C[1,1];
        double a01 = C[0,1];
        double a10 = C[1,0];
        double a00 = C[0,0];
        double d0 = (a00*a11)-(a10*a01);
        matrix R3 = new matrix(C.size2,C.size2);
        QRGS.decomp(C,R3);
        double d = QRGS.det(R3);
        WriteLine($"det(A) = {d0} ");
        WriteLine($"det(R) = {d}");
        WriteLine(border);

        WriteLine("Invert test:");
        matrix D = new matrix(6,6);
        int n4 = D.size1;
        int m4 = D.size2;
        for(int j=0;j<m4;j++){
            for(int i=0;i<n4;i++){
                D[i,j]+=rnd.Next(1,10);
            }
        }
        WriteLine($"n = {n4}, m = {m4}");
        WriteLine(border);
        WriteLine("The random square A matrix is:");
        D.print();
        WriteLine(border);
        matrix R4 = new matrix(D.size2,D.size2);
        QRGS.decomp(D,R4);
		WriteLine("After QR decomposition, the Q matrix is:");
		D.print();
        WriteLine(border);
		WriteLine("After QR decomposition, R is upper triangular:");
        R4.print();
        WriteLine(border);
        WriteLine("The inverse matrix B is found to be:");
        matrix invD = QRGS.invert(D,R4);
        invD.print();
        WriteLine(border);
        WriteLine("AB is found to be the identity matrix:");
        matrix I = D*R4*invD;
        I.print();
        WriteLine(border);






        }

        
    }
