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
            IData data = new HeatTransferCalculationsData();
            var path = Assembly.GetExecutingAssembly().Location;
            var resourcesPath = @"..\..\..\Resources\Data.txt";
            path =  Path.GetFullPath(Path.Combine(path, resourcesPath));

            var reader = new DataReader();
            reader.ReadData(ref data, path);

            data.noOfNodes = data.noOfElements + 1;
            data.singleSubelementLength = data.totalLengthOfElement / data.noOfElements;


            Console.ReadLine();
        }


    }

    public class Node
    {
        int id;
        double x;
        BoundryCondition boundryType;

        public Node(int _id, double _x, BoundryCondition _boundryCondition)
        {
            this.id = _id;
            this.x = _x;
            this.boundryType = _boundryCondition;
        }
    }

    public class FiniteElement
    {

    }

    public enum BoundryCondition
    {
        LeftBoundryCondition = 0,
        NoBoundryCondition = 1,
        RightBoundryCondition = 2
    }
}
