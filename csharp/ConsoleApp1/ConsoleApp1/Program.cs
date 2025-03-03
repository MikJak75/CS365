using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class Program{

    public static void Main(string[] args){
        string? s; 


        try {
            s = Console.ReadLine();
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return;    
        }

        if (s == null){
            Console.WriteLine("Error reading line");
            return;
        }

        List<string> operator_list = new List<string> {"+", "-", "*", "/", "%"};


        s = s.Trim();
        string[] fields = s.Split();

        string op = fields[0];
        string left = fields[1];
        string right = fields[2];

        bool in_op_list = operator_list.Contains(op);

        if (in_op_list == false){
            Console.WriteLine($"Invalid operator {op}.");
            return;
        }

        float left_int = int.Parse(left);
        float right_int = int.Parse(right);
        float result;

        if (op == "+")
        {
            result = left_int + right_int;
        }
        else if (op == "-")
        {
            result = left_int - right_int;
        } 
        else if (op == "*")
        {
            result = left_int * right_int;
        }
        else if (op == "/")
        {
            //Console.WriteLine(left_int);
            //Console.WriteLine(right_int);
            //Console.WriteLine(left_int/right_int);
            result = left_int / right_int;
        }
        else if (op == "%")
        {
            result = left_int % right_int;
        }
        else
        {
            result = -12345;
        }

        Console.WriteLine(result);
        return;
        //foreach (string b in fields) {
            //Console.WriteLine(b);
        //}

        //string? s2 = Console.ReadLine();
        //s2 = s2.Trim();

        //if (s2 == null){
            //Console.WriteLine("bruh null");
        //}
    }

}