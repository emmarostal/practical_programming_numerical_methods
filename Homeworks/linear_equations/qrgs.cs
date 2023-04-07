using System;
using static System.Console;
using static System.Math; 

public static class QRGS{
   public static void decomp(matrix A, matrix R){
    for(int i = 0; i<A.size2; i ++){
            R[i,i]=A[i].norm();
            A[i]/=R[i,i]; //normalization
            for (int j=i+1; j<A.size2; j ++){
                R[i,j]=A[i].dot(A[j]); 
                A[j]-=A[i]*R[i,j];     
            }
            //R is now upper triangular
        }

   }



   public static vector solve(matrix Q, matrix R, vector b){

   }



   public static double det(matrix R){ 

   }
}