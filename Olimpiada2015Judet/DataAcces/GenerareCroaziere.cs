using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Olimpiada2015Judet.Models;
using System.Windows.Forms;

namespace Olimpiada2015Judet.DataAcces
{
    public class GenerareCroaziere
    {
        public static void Generare(string connectionString)
        {
            SqlDataAcces.ClearDB(SqlDataAcces.ConnectionString, "Croaziere");
            List<List<DistantaModel>> distante = SqlDataAcces.GetDistante();
            int index = 0;
            int numberOfIterations = 0;
            for (int i = 1; i < 13; i++)
            {
                for (int j = i + 1; j < 13; j++)
                {
                    for (int k = j + 1; k < 13; k++)
                    {
                        int distance = distante[0][i].Distanta + distante[i][j].Distanta + distante[j][k].Distanta + distante[k][i].Distanta;
                        if (distance >= 800 && distance <= 1100)
                        {
                            index++;
                            List<int> listaPorturi = new int[] { i, j, k, i }.ToList();
                            int pret = distance * 2;
                            SqlDataAcces.SalveazaCroaziera(index, 3, listaPorturi, pret, distance);
                        }

                        numberOfIterations++;
                    }
                }
            }
            for (int i = 1; i < 13; i++)
            {
                for (int j = i + 1; j < 13; j++)
                {
                    for (int k = j + 1; k < 13; k++)
                    {
                        for (int n = k + 1; n < 13; n++)
                        {
                            for (int m = n + 1; m < 13; m++)
                            {
                                int distance = distante[0][i].Distanta + distante[i][j].Distanta + distante[j][k].Distanta + distante[k][n].Distanta + distante[n][m].Distanta + distante[m][i].Distanta;
                                if (distance >= 800 && distance <= 1600)
                                {
                                    index++;
                                    List<int> listaPorturi = new int[] { i, j, k, n, m, i }.ToList();
                                    int pret = distance * 2;
                                    SqlDataAcces.SalveazaCroaziera(index, 5, listaPorturi, pret, distance);
                                }
                                numberOfIterations++;
                            }
                        }

                    }
                }
            }
            for (int i = 1; i < 13; i++)
            {
                for (int j = i + 1; j < 13; j++)
                {
                    for (int k = j + 1; k < 13; k++)
                    {
                        for (int n = k + 1; n < 13; n++)
                        {
                            for (int m = n + 1; m < 13; m++)
                            {
                                for (int x = m + 1; x < 13; x++)
                                {
                                    for (int y = x + 1; y < 13; y++)
                                    {
                                        for (int z = y + 1; z < 13; z++)
                                        {
                                            int distance = distante[0][i].Distanta + distante[i][j].Distanta + distante[j][k].Distanta + distante[k][n].Distanta + distante[n][m].Distanta +
                                            distante[m][x].Distanta + distante[x][y].Distanta + distante[y][z].Distanta + distante[y][0].Distanta;
                                            if (distance >= 800 && distance <= 10000)
                                            {
                                                index++;
                                                List<int> listaPorturi = new int[] { i, j, k, n, m, x, y, i }.ToList();
                                                int pret = distance * 2;
                                                SqlDataAcces.SalveazaCroaziera(index, 8, listaPorturi, pret, distance);
                                            }
                                            numberOfIterations++;
                                        }

                                    }
                                }

                            }
                        }

                    }
                }
            }

            MessageBox.Show("Generate toate croazierele in " + numberOfIterations.ToString() + " de iteratii.");
        }
    }
}
