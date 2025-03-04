using System;
using System.Collections.Generic;

// Full class MapReduce
public partial class MapReduce<T> {
    private List<T> _data;
    public string name;

    public void Add(T entry){
        _data.Add(entry);
        Count = Count+1;
    }

    public MapReduce(){
        _data = new List<T>();
        Count = 0;
        name = "bruh";
    }

    public int Count{
        get;
        set;
    }
    //Indexer
    public T this[int index]{
        get => _data[index];
        set => _data[index] = value;
    }
}