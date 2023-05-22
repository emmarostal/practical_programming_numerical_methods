public static class QRGS{
    public static void decomp(matrix A, matrix R){
    int m = A.size2;
    for(int i = 0; i<m; i ++){
            R[i,i]=A[i].norm();
            A[i]/=R[i,i]; //normalization
            for (int j=i+1; j<m; j ++){
                R[i,j]=A[i].dot(A[j]); 
                A[j]-=A[i]*R[i,j];     
            }//R is now upper triangular
        }
   }

   public static vector solve(matrix Q, matrix R, vector b){
   b = Q.transpose()*b;
   for(int i = R.size1-1; i >= 0; i--){ //Starting at the bottom
        double sum = 0;
        for(int j = i+1;j<R.size2;j++){
            sum += R[i,j]*b[j];
    }
    b[i]=(b[i]-sum)/R[i,i];
   }

   return b;
   }

   public static double det(matrix R){
   double product = 1;
   for(int i=0; i<R.size1;i++){
    product*=R[i,i]; //product of diagonal elements
   }  
   return product;
   }

   public static matrix invert(matrix Q, matrix R){
   matrix inverted = new matrix(Q.size2,Q.size1);
   for(int i=0;i<Q.size2;i++){
    vector ivec = new vector(Q.size1);
    ivec[i]=1;
    inverted[i]=solve(Q,R,ivec);
   }
   return inverted;
   }
}