using System.Collections;
using System.IO;
using System;
//using System.Threading.Tasks.Dataflow;

public partial class DataReader : IEnumerable {
//public class DataReader {
    //private double[] _data;
    private double[] _data;
    
    //public DataReaderEnum GetEnumerator(){
        //return new DataReaderEnum(_data);
    //}
    public IEnumerator GetEnumerator(){
        return new DataReaderEnum(_data);
    }

    public DataReader(string filename){
        BinaryReader br;

        if (filename == null){
            throw new FileNotFoundException();
        }
        if ( !(File.Exists(filename)) ) {
            throw new FileNotFoundException();
        }

        long num_bytes = new System.IO.FileInfo(filename).Length;
        int num_doubles = (int)(num_bytes/8);

        _data = new double[num_doubles];

        var stream = File.Open(filename, FileMode.Open);

        try {
            br = new BinaryReader(stream);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return;
        }

        for(int i = 0; i < num_doubles; i++){
            _data[i] = br.ReadDouble();
        }

        Count = num_doubles;
    }

    public int Count{
        get;
        set;
    }
    public double this[int index]{
        get {
            if (index >= Count){
                throw new IndexOutOfRangeException();
            }

            return _data[index];
        }
        set => _data[index] = value;
    }


}


//public class DataReaderEnum : IEnumerator{
    //private double[] _dataref;
    //private int _pos;
    //public DataReaderEnum(double[] data){
        //_dataref = data;
        //Reset();
    //}
    //public void Reset(){
        //_pos = -1;
    //}
    //public object Current{
        //get => _dataref[_pos];
    //}
    //public bool MoveNext(){
        //_pos += 1;
        //return (_pos < _dataref.Length);
    //}
//}
