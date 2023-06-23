using System;
using static System.Console;
using static System.Math;
class inputoutput{
    public static void Main(string[] args){
        exercise_1(args);
        exercise_2();
        exercise_3(args);
    }   

    static void exercise_1(string[] args){
        WriteLine("Task 1");
        string border = new string('-',45);
        WriteLine(border);
        foreach(var arg in args){
            var words = arg.Split(':');
            if(words[0]=="-numbers"){
                var numbers=words[1].Split(',');
                foreach(var number in numbers){
                    double x = double.Parse(number);
                    WriteLine($"{x} {Sin(x)} {Cos(x)}");
                }
            }
        }
        WriteLine("");
    }

    static void exercise_2(){
        WriteLine("Task 2");
        string border = new string('-',45);
        WriteLine(border);
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        string line;
        while ((line = ReadLine()) != null && line != "end") {
            var numbers = line.Split(split_delimiters, split_options);
            foreach(var number in numbers) {
                double x = double.Parse(number);
                WriteLine($"{x} {Sin(x)} {Cos(x)}");
            }
        }
        WriteLine("");
    }

    static void exercise_3(string[] args){
        string infile=null,outfile=null;
        foreach(var arg in args){
            var words=arg.Split(':');
            if(words[0]=="-input")infile=words[1];
            if(words[0]=="-output")outfile=words[1];
        }
        if( infile==null || outfile==null) {
            Error.WriteLine("wrong filename argument");
        }
        var instream = new System.IO.StreamReader(infile);
        var outstream = new System.IO.StreamWriter(outfile,append:true);
        outstream.WriteLine("Task 3");
        string border = new string('-',45);
        outstream.WriteLine(border);
        for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){
            double x=double.Parse(line);
            outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
        }
        instream.Close();
        outstream.Close();
    }
    
}