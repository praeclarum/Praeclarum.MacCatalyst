using System;
using System.Threading.Tasks;

namespace MacCatSdk
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            await (new BuildApp ()).RunAsync ();
            return 0;
        }
    }
}
