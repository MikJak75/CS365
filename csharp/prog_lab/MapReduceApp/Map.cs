using System;
using System.Threading.Tasks;

//Parallel function to map
public partial class MapReduce<T> {
    public delegate T MapFunc(T x);
    public void Map(MapFunc func){
        Parallel.For(0, Count, i =>
        {
            _data[i] = func(_data[i]);
        });
    }
}