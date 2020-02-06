using FEM.FEMCalculationsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM
{
    public class FEMGrid
    {
        public Node[] nodes;
        public FiniteElement[] elements;
        public double[,] H; //global matrix
        public double[] P; // global veector
        public IData data;

        public FEMGrid()
        {
            //TODO: think about parallelisation, using operation bus to store and sort calculated elements before resolving to mesh.
        }

        public FEMGrid(/*HeatTransferCalculationsData*/IData _data)
        {
            this.data = _data;

            this.H = new double[data.noOfNodes, data.noOfNodes];
            this.P = new double[data.noOfNodes];

            nodes = new Node[data.noOfNodes];
            for (int i = 0; i < data.noOfNodes; i++)
            {
                BoundryCondition conditionType = BoundryCondition.NoBoundryCondition;
                if (i == 0)
                {
                    conditionType = BoundryCondition.LeftBoundryCondition;
                }
                if (i == data.noOfNodes - 1)
                {
                    conditionType = BoundryCondition.RightBoundryCondition;
                }

                nodes[i] = new Node(i, i, conditionType);
            }

            this.elements = new FiniteElement[data.noOfElements];
            for (int i = 0; i < data.noOfElements; i++)
            {
                elements[i] = new FiniteElement(nodes[i], nodes[i + 1]);
            }
        }

        public void createLocalMatrix()
        {
            for (int i = 0; i < data.noOfElements; i++)
            {
                elements[i].C = ((data.crossSection * data.thermalConductivity) / data.singleSubelementLength);
                elements[i].createFiniteElementLocalMatrix(data.convectionCoefficient, data.crossSection);
            }
        }

        public void createLocalVector()
        {
            for (int i = 0; i < data.noOfElements; i++)
            {
                elements[i].createFiniteElementVector(data.heatFlux, data.crossSection, 
                                                      data.convectionCoefficient, data.ambientTemperature);
            }
        }

        public void createGlobalMatrix()
        {
            int size = data.noOfNodes;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    H[row, column] = 0;
                }
            }

            for (int i = 0; i < data.noOfElements; i++)
            {
                H[i, i] += elements[i].ElementH[0, 0];
                H[i, i + 1] += elements[i].ElementH[0, 1];
                H[i + 1, i] += elements[i].ElementH[1, 0];
                H[i + 1, i + 1] += elements[i].ElementH[1, 1];
            }
        }

        public void createGlobalVector()
        {
            for (int i = 0; i < data.noOfNodes; i++)
            {
                P[i] = 0;
            }

            for (int i = 0; i < data.noOfElements; i++)
            {
                P[i] += elements[i].ElementP[0];
                P[i + 1] += elements[i].ElementP[1];
            }
        }

        public double[,] GetGlobalMatrix()
        {
            createGlobalMatrix();
            return this.H;
        }

        public double [] GetGlobalVector()
        {
            createGlobalVector();
            return this.P;
        }
    }
}
