using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM
{
    public class Node
    {
        public int id { get; set; }
        public double x { get; set; }
        public BoundryCondition boundryType;

        public Node(int _id, double _x, BoundryCondition _boundryCondition)
        {
            this.id = _id;
            this.x = _x;
            this.boundryType = _boundryCondition;
        }
    }
    public enum BoundryCondition
    {
        LeftBoundryCondition = 0,
        NoBoundryCondition = 1,
        RightBoundryCondition = 2
    }
}
