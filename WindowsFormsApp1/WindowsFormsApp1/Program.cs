using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]


        static void Main()
        {

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            /*
            int tamanno = 12;

            int[,] matrizEjemplo = new int[tamanno, tamanno];


            Random random = new Random();
            int numero = random.Next(1,tamanno+1);
            int siguiente = 2;
            for (int x = 0; x < tamanno; ++x)
            {
                for (int y = 0; y < tamanno; ++y)
                {
                    matrizEjemplo[x, y] = 0;
                    ++numero;
                    if(numero == tamanno+1)
                    {
                        numero = 1;
                    }
                }
                numero = siguiente;
                ++siguiente;
            }


            int[,,]figuras = new int[48, 3, 2];
            int[]sumas = new int[48];

            int matrizX = 0;
            int matrizY = 0;
            int suma = 0;

            for (int x = 0; x < 48; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    suma = matrizEjemplo[matrizX, matrizY] + suma;
                    figuras[x, y, 0] = matrizX;
                    figuras[x, y, 1] = matrizY;
                    ++matrizX;

                }

                sumas[x] = suma;
                suma = 0;

                if (matrizX == tamanno)
                {
                    ++matrizY;
                    matrizX = 0;
                }
            }


            int[,,] matriz = new int[2, tamanno, tamanno];

            for (int x = 0; x < tamanno; ++x)
            {
                for (int y = 0; y < tamanno; ++y)
                {
                    matriz[1, x, y] = 0;
                    matriz[0, x, y] = 0;
                }

            }
            
            Random a = new Random();
            int ax = 0;
            int ay = 0;
            int c = 0;
            while(c != 200)
            {
                ax = a.Next(tamanno);
                ay = a.Next(tamanno);

                matriz[0, ax, ay] = matriz[1, ax, ay] = 0;
                ++c;
            }
            


            

            //normal
            for (int y = 0; y < tamanno; ++y)
            {
                for (int x = 0; x < tamanno; ++x)
                {
                    Console.Write(matrizEjemplo[x, y]);
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");



            //normal
            for (int y = 0; y < tamanno; ++y)
            {
                for (int x = 0; x < tamanno; ++x)
                {
                    Console.Write(matriz[0, x, y]);
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");



            //normal
            for (int y = 0; y < tamanno; ++y)
            {
                for (int x = 0; x < tamanno; ++x)
                {
                    Console.Write(matriz[1, x, y]);
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");


            */
            Console.WriteLine("Creacion Sudoku.");

            int tamanno = 12;



            int dificultad = (tamanno * tamanno) / 2;
            Sudoku a = new Sudoku(tamanno,dificultad);


            Console.WriteLine("Backtracking.");
            int[,,] matriz = a.getSudoku();
            int[,,] figuras = a.getFiguras();
            int[] sumas = a.getSumas();


            Backtracking metodo = new Backtracking(matriz, figuras, sumas, tamanno);
            metodo.resolver();
            //metodo.resolverHilos();
        }
    }
}
