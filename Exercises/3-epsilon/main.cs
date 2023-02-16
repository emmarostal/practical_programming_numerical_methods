using System;
using static System.Console;
using static System.Math;
class epsilon{
	static void Main(){
		Write("1. Maximum and minimun integers\n");
		int i=1; while(i+1>i) {i++;}
		Write("my max int = {0}\n",i);
		Write("MaxValue int = {0}\n",int.MaxValue);

		int j=1; while(j-1<i) {j--;}
		Write("my min int = {0}\n",j);
		Write("MinValue int = {0}\n\n",int.MinValue);


		Write("2. Machine epsilon\n");
		double x=1; while(1+x!=1){x/=2;} x*=2;
		Write("my double = {0}\n",x);
		Write("1.0 - my double = {0}\n",1.0-x);
		float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;
		Write("my float = {0}\n",y);
		Write("1.0 - my float = {0}\n\n",1.0-y);

		Write("3. Small sums\n");
		int n=(int)1e6;
		double epsilon=Pow(2,-52);
		double tiny=epsilon/2;
		Write("tiny = {0}\n",tiny);

		double sumA=0,sumB=0;

		sumA+=1; for(int k=0;k<n;k++){sumA+=tiny;}
		for(int k=0;k<n;k++){sumB+=tiny;} sumB+=1;
		WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");
		WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");



}
}
