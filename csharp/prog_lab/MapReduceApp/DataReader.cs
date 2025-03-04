using System.Collections;
using System.IO;
using System;

public partial class DataReader : IEnumerable {
    private double[] _data;
    
    public IEnumerator GetEnumerator(){
        return new DataReaderEnum(_data);
    }

    //Construt by reading in data
    public DataReader(string filename){
        BinaryReader br;

        //If no filename entered
        if (filename == null){
            throw new FileNotFoundException();
        }
        //If filename doesn't exist
        if ( !(File.Exists(filename)) ) {
            throw new FileNotFoundException();
        }

        // Find number of bytes and thus number of doubles that will be read in
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

        // Read in num_doubles doubles
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
            // If index is out of bounds throw
            if (index >= Count){
                throw new IndexOutOfRangeException();
            }

            return _data[index];
        }
        set => _data[index] = value;
    }


}

