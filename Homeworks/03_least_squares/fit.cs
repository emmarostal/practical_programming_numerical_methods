using System;
using static System.Console;
using static System.Math; 

public static class fit{

	public static (vector, matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
		//create matrix A_ij:
		matrix A = new matrix(x.size, fs.Length);
		vector b = new vector(y.size);
		for(int i = 0; i < x.size; i++){
			b[i] = y[i]/dy[i];
			for(int j = 0; j < fs.Length; j++){
				A[i,j] = fs[j](x[i])/dy[i];
			}
		}
		//solve for parameter vector and covariance matrix s
		matrix R = new matrix(fs.Length, fs.Length);
		matrix A2 = A.transpose() * A;
		matrix R2 = new matrix(A2.size1, A2.size1);
		QRGS.decomp(A,R);
		QRGS.decomp(A2,R2);
		var s = QRGS.invert(A2,R2);
		return (QRGS.solve(A,R,b),s);
	}

}