using System;
using static System.Console;
using static System.Math;
using static cmath;
public static class main{
    public static void Main(){
        string Good = "The found value is close enough to the manually calculated value.";
        string Bad = "The found value is not close enough to the manually calculated value.";
        complex I = new complex(0,1);
        complex MinusOne = new complex(-1,0);
        complex SqrtMinusOne = sqrt(MinusOne);
        complex SqrtI = sqrt(I);
        complex EtoI = cmath.exp(I);
        complex EtoPiI = cmath.exp(PI*I);
        complex ItoI = I.pow(I);   
        complex LnI = cmath.log(I); 
        complex SinIPi = cmath.sin(I*PI);

        WriteLine($"Sqrt(-1)\nshould be i\nis found to be {SqrtMinusOne}");
        if (SqrtMinusOne.approx(I)) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"Sqrt(i)\nshould be (1+i)/sqrt(2), which is approximately 0.707(1+i)\nis found to be {SqrtI}");
        if (SqrtI.approx(new complex(1/sqrt(2),1/sqrt(2)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"e^i\nshould be cos(1) + i sin(1), which is approximately 0.54+0.84i\nis found to be {EtoI}");
        if (EtoI.approx(new complex(Cos(1),Sin(1)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"e^(pi i)\nshould be -1\nis found to be {EtoPiI}");
        if (EtoPiI.approx(-1)) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"i^i\nshould be e^(-pi/2), which is approximately 0.208\nis found to be {ItoI}");
        if (ItoI.approx(exp(-PI/2))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"ln(i)\nshould be i pi / 2, which is approximately 1.57i\nis found to be {LnI}");
        if (LnI.approx(new complex(0,PI/2))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"sin(i pi)\nshould be i*sinh(pi), which is approximately 11.54i\nis found to be {SinIPi}");
        if (SinIPi.approx(new complex(0,Sinh(PI)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");
    }


}