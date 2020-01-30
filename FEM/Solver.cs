using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FEM
{
    public class Solver
    {
        static void Main(string[] args)
        {

            var reader = new DataReader();
            IData data = new HeatTransferCalculationsData();
            var path = Assembly.GetExecutingAssembly().Location;
            var resourcesPath = @"..\..\..\Resources\Data.txt";
            path =  Path.GetFullPath(Path.Combine(path, resourcesPath));
            reader.readData(ref data, path);


            Console.ReadLine();
        }
    }
}
