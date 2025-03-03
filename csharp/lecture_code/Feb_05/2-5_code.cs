using System;
using System.Collections.Generic;

public class Test {

    public static void Main(string[] args) {
        //Test.OutputHello();
        //Other o = new Other();
        //o.Do();

        //(o as Test).Do();

        List<string> l = new List<string>();
        l.Add("hello");
        l.Add("World");
        //l.ForEach((s) => )
        Console.WriteLine("hello bruh");
    }

    public virtual void Do() {
        Console.WriteLine("Did it!");
    }

}

public class Other : Test{
    public delegate void FunctSignature(int a, int b);
    public override void Do() {
        Console.WriteLine("Nope!");
    }

}