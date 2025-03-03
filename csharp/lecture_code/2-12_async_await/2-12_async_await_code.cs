using System;
using System.Threading.Tasks;

class Test{
    public static void Main(string[] args){
        List<Task<int>> tasks = new List<Task<int>>();
        for (int i = 0; i < 10; i+=1){
            tasks.Add(DoStuff());
        }
        Console.WriteLine("Tasks created");
        Task.WaitAll(tasks);
        Console.WriteLine("Tasks done");
        tasks.ForEach( (t) => {
            Console.WriteLine(t.Result);
        });

        /*
        int[] values = new int[] {10, 20, 30, 40};
        Task<int> t = DoStuff(values);
        Task<int> t2 = DoStuff(new int[] {5, 6, 7, 8});
        t.Wait();
        t2.Wait();
        Console.WriteLine(t.Result);
        Console.WriteLine(t2.Result);
        */
       
    }

    public static async Task<int> DoStuff(){
        return await Task.Run( () => {
            int j;
            for (j = 0; j < 10_000_000; j++){

            }
            return j;
        });
    
    }

    /*
    public static async Task<int> DoStuff(int[] values){
        return await Task.Run(() => {
            int i = 0;
            foreach (var v in values){
                i += v;
            }
            return i;
        });
    }
    */
}