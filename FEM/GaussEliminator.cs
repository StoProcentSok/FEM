using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM
{
    public class GaussEliminator
    {
        
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


                for (int j = i; j < size + 1; j++)
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
                    for (int k = i; k < size + 1; k++)
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

            for (int i = 0; i < n+1; i++)
            {
                A[i, n+1] = paramss[i];
            }


            // Print input
            this.generateAndPrintMatrixRepresentation(A);

            double[] x = new double[n];
            x = this.gautianElimination(A); //matrix [n, n+1] extedned by params.
            

            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = x[i];
            }

            return result;
        }

        #region print helper
        //TODO: Move to layer above, for availability to all matrices.
        public void generateAndPrintMatrixRepresentation(double[,] equationsMatrix)
        {
            //Generating 'canvas'
            int rows = equationsMatrix.GetLength(0);
            int columns = equationsMatrix.GetLength(1);
            int longestNumeral = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    longestNumeral = getNumeralLength(equationsMatrix[i, j]) > longestNumeral
                                   ? getNumeralLength(equationsMatrix[i, j]) : longestNumeral; 
                }
            }

            int getNumeralLength(double numeral)
            {
                return numeral.ToString().Length;
            }

            var resultCanvas = new string[rows,columns];

            StringBuilder canvasCell = new StringBuilder("");
            canvasCell.Append(' ', longestNumeral);

            var canvasCellToBeFilled = canvasCell.ToString();

            //creating canvas array
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    resultCanvas[i, j] = canvasCellToBeFilled;
                    string number = (equationsMatrix[i, j]).ToString();
                }
            }

            //filling canvas 
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    //TODO: investigate precission for specific cases
                    string number = (equationsMatrix[i, j]).ToString();
                    var numberLength = getNumeralLength(equationsMatrix[i, j]);
                    var trailingWhiteCharacters = longestNumeral - numberLength;
                    //values with minus sign in cell to the right were offset too much.
                    if (i + 1 != rows && j + 1 != columns && equationsMatrix[i,j+1] < 0)
                    {
                        trailingWhiteCharacters = trailingWhiteCharacters == 0 ? trailingWhiteCharacters : trailingWhiteCharacters - 1;  
                    }
                    //TODO: formatting of representation:
                    StringBuilder builder = new StringBuilder("");
                    string comma = (j == columns - 1) ? "" : ",";

                    builder.Append(number + comma)
                           .Append(' ', trailingWhiteCharacters);

                    var valueToPrint = builder.ToString();

                    resultCanvas[i, j] = valueToPrint;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < columns; j++)
                {

                    Console.Write(resultCanvas[i, j]);
                }
                Console.WriteLine("|");
            }
        }

        #endregion
    }
}
