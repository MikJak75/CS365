
using System.Collections;
using System.IO;

public partial class DataReaderEnum : IEnumerator{
    private double[] _dataref;
    private int _pos;
    public DataReaderEnum(double[] data){
        _dataref = data;
        Reset();
    }
    public void Reset(){
        _pos = -1;
    }
    public object Current{
        get => _dataref[_pos];
    }
    public bool MoveNext(){
        _pos += 1;
        return (_pos < _dataref.Length);
    }
}