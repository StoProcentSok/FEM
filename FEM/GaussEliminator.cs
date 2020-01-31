using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM
{
    public class GaussEliminator
    {
        public void printEquationsMatrix(double[,] equationsMatrix)
        {
            int size = equationsMatrix.GetLength(1); //length of A of AxA matrix
            for (int i = 0; i < size; i++)
            {
                Console.Write("|");
                for (int j = 0; j < size; j++)
                {
                    //TODO: find longest (chars wise) number in array and print every
                    //number in "cell' of such size for graphical interpretation integrity
                    Console.Write(equationsMatrix[i, j] + "  ");
                }
                Console.WriteLine("|");
            }
        }

        public double[] gautianElimination(double[,] matrix)
        {
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                double biggestValue = Math.Abs(matrix[i, i]);
                int biggestValueRow = i;
                for (int j = i + 1; j < size; j++)
                {
                    if (Math.Abs(matrix[j, i]) > biggestValue)
                    {
                        biggestValue = matrix[j, i];
                        biggestValueRow = j;
                    }
                }


                for (int j = i; j < size; j++)
                {
                    double temp = matrix[biggestValueRow, j];
                    matrix[biggestValueRow, j] = matrix[i, j];
                    //setting row with biggest value as current row
                    //elimination will start from biggest value and will
                    //reduce amtrix with each next iteration
                    matrix[i, j] = temp;
                }

                for (int j = i + 1; j < size; j++)
                {
                    double reducingCoefficient = -matrix[j, i] / matrix[i, i];
                    for (int k = i; k < size; k++)
                    {
                        if (i == k) {
                            matrix[j, k] = 0;
                        }
                        else
                        {
                            matrix[j, k] += reducingCoefficient * matrix[i, k];
                        }
                    }
                }
            }

            double[] result = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                result[i] = matrix[i, size] / matrix[i, i];
                for (int j = i - 1; j >= 0; j--)
                {
                    matrix[j, size] -= matrix[j, i] * result[i];
                }
            }

            return result;
        }

        public double[] calculate(double[,] matrix, double[] paramss, int params_c)
        {
            int n = params_c - 1;
            double[] line = new double[n + 1];

            
            double[,] A = new double[n + 1, n+2];

            // Read input data
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    A[i,j] = matrix[i,j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                A[i, n+1] = paramss[i];
            }


            // Print input
            this.printEquationsMatrix(A);

            double[] x = new double[n];
            x = this.gautianElimination(A); //matrix [n, n+1] extedned by params.
            

            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = x[i];
            }

            return result;
        }

    }
}
