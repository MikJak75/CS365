//using System.Diagnostics;
//using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System;

public partial class MapReduce<T> {
    public delegate T ReduceFunc(T left, T right);

    public T Reduce(ReduceFunc func){
        if (Count == 0){
            throw new Exception("cant reduce on 0 size");
        } else if (Count == 1){
            return func(_data[0], _data[0]);
        } else {
            T accum = func(_data[0], _data[1]);
            for (int i = 2; i < Count; i++){
                T cur = _data[i];
                accum = func(accum, cur);
            }
            return accum;
        }
    }

    public async Task<T> ReduceAsync(ReduceFunc func){
        return await Task.Run( ()=> {
            if (Count == 0){
                throw new Exception("cant reduce on 0 size");
            } else if (Count == 1){
                return func(_data[0], _data[0]);
            } else {
                T accum = func(_data[0], _data[1]);
                for (int i = 2; i < Count; i++){
                    T cur = _data[i];
                    accum = func(accum, cur);
                }
                return accum;
            }
        });
    }

}
