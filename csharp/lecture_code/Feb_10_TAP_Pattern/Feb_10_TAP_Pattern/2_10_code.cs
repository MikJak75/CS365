using System;
using System.Threading.Tasks;

class Test {
    public static void Main(string[] args) {
        int start = 10;
        int end = 30;
        bool[] areprimes = new bool [ end - start + 1]
        Parallel.For(start, end+1, i => 
        {
            if (is_prime(i)) {
                Console.WriteLine($"{i} is prime");
            }
            else {
                Console.WriteLine($"{i} is NOT prime");

            }
        });
    }

    public static bool is_prime(int v) {
        return ( (v%2) == 0);
    }
}