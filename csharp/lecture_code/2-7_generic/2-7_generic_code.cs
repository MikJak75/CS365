using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
class Test{
    public static void Main(){
        int i;
        var mc = new MyClass<int>(3);

    }
}

public class MyClass<T> {
    private T[] _data;
    public MyClass(int capacity){
        _data = new T[capacity]; 
    }
    public T this[int index] {
        get => _data[index];
    }
}