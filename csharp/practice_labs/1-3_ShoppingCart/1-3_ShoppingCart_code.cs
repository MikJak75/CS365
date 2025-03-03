using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

class Cart {
    public static void Main(string[] nothing) {
        StreamReader sr;
        string? line;
        double price;
        List<Item> items_list = new List<Item>{};

        Cart cart = new Cart();
        //Console.Write("Enter shopping cart file: ");
        string? filename = Console.ReadLine();
        if (filename == null){
            return;
        }


        try {
            sr = new StreamReader(filename);
        }
        catch (Exception e){
            Console.WriteLine(e.Message);
            return;
        }

        while ( (line = sr.ReadLine()) != null){
            line = line.Trim();
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            price = double.Parse(words[0]);
            List<string> name_words = new List<string>{};

            for (int i = 1; i < words.Length; i++){
               name_words.Add(words[i]);
            }
            string name_string = String.Join(' ', name_words);

            cart.Add(price, name_string);

            //Console.WriteLine(name_string);
        }
        cart.Print();
    }
    private List<Item> _items;
    public Cart() {
        _items = new List<Item>{};
        return;
    }

    public void Add(double price, string name){
        bool present_already = false;
        int found_index = 0;

        for (int i = 0; i < _items.Count; i++){
            if (_items[i].name == name){
                present_already = true;
                found_index = i;
            }
        }

        if(present_already){
            _items[found_index].price += price;
        } 
        else {
            Item item = new Item(price, name);
            _items.Add(item);
        }
    }
    public void Print(){
        //_items.Sort();
        //foreach (Item i in _items){
            //string fstring = $"{i.name, -25} ${i.price, 7 :F2}";
            //Console.WriteLine(fstring);
        //}


        _items.Sort();
        double total = 0;
        foreach (Item i in _items){
            total += i.price;
            string fstring = $"{i.name, -25}  ${i.price, 7 :F2}";
            Console.WriteLine(fstring);
        }
        Console.WriteLine($"Total = ${total:F2}");
    }

    class Item : IComparable<Item> {
        public string name;
        public double price;
        public int CompareTo(Item? other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;

            double price1, price2;

            price1 = this.price;
            price2 = other.price;
            if( price1 > price2){
                return -1;
            }
            else if (price1 < price2){
                return 1;
            } else {
                //return 0;
                return String.Compare(this.name, other.name);
            }
        }

        public Item(double price, string name){
            this.price = price;
            this.name = name;
        }

    }
}






//using System;
//using System.IO;
//using System.Collections.Generic;

//class Cart {
    //public static void Main(string[] nothing) {
        //Cart cart = new Cart();
    //}
    //private List<Item> _items;
    //public Cart() {
        //// Write constructor here
    //}
    //class Item : IComparable<Item> {
        //// Write Item class here
    //}
//}