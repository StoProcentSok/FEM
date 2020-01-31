using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM
{
    public class FiniteElement
    {
        public double C;
        public Node[] Nodes;
        public double[,] ElementH;
        public double[] ElementP;

        public FiniteElement()
        {
            Nodes = new Node[2];
            ElementH = new double[2, 2];
            ElementP = new double[2];
        }

        public FiniteElement(Node n1, Node n2)
        {
            Nodes = new Node[2];
            Nodes[0] = n1;
            Nodes[1] = n2;
            ElementH = new double[2, 2];
            ElementP = new double[2];
        }

        public void createFiniteElementLocalMatrix(double convectionCoefficient, double crossSection)
        {
            this.ElementH[0,0] = C;
            this.ElementH[0,1] = -C;
            this.ElementH[1,1] = C;
            this.ElementH[1,0] = -C;

            if (this.Nodes[0].boundryType == BoundryCondition.RightBoundryCondition)
            {
                ElementH[0, 0] += convectionCoefficient * crossSection;
            }
            if (this.Nodes[1].boundryType == BoundryCondition.RightBoundryCondition)
            {
                ElementH[1, 1] += convectionCoefficient * crossSection;
            }
        }

        public void createFiniteElementVector(double heatFlux, double crossSection, double convectionCoefficient, double ambientTemperature)
        {
            ElementP[0] = 0;
            ElementP[1] = 0;

            if (this.Nodes[0].boundryType == BoundryCondition.LeftBoundryCondition)
            {
                ElementP[0] = ((-heatFlux) * crossSection);
            }
            if (this.Nodes[1].boundryType == BoundryCondition.RightBoundryCondition)
            {
                ElementP[1] = convectionCoefficient * crossSection * ambientTemperature;
            }
        }
    }
}
