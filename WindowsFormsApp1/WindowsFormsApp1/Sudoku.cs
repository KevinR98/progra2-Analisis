using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Sudoku
    {

        int[,,] matriz;
        int[,,] solucion;
        int[,,] figuras;
        int[] sumas;
        int tamanno;

        Stack<int> instructionsStack = new Stack<int>();


        public Sudoku(int tamanno, int dificultad)
        {
            this.tamanno = tamanno;
            generate(dificultad);

        }

        public int[,,] getSudoku()
        {
            return matriz;
        }
        public int[,,] getSolucion()
        {
            return solucion;
        }
        public int[,,] getFiguras()
        {
            return figuras;
        }
        public int[] getSumas()
        {
            return sumas;
        }


        private void debug(int[,,] matriz)
        {

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
        }

        private void generate(int dificultad)
        {

            solucion = new int[2,tamanno, tamanno];


            Random random = new Random();
            int numero = random.Next(0, tamanno);
            int[] posiblesNumeros = new int[tamanno];
            int[] primeraFila = new int[tamanno];

            int numberIndex = 0;

            while (numberIndex < tamanno)
            {

                if (posiblesNumeros[numero] == 0)
                {
                    
                    posiblesNumeros[numero] = 1;
                    solucion[0,numberIndex, 0] = numero+1;
                    solucion[1, numberIndex, 0] = 1;
                    ++numberIndex;
                    numero = random.Next(0, tamanno);
                }
                else
                {
                    ++numero;
                    if(numero == tamanno)
                    {
                        numero = 0;
                    }
                }
            }


            Backtracking metodo = new Backtracking(solucion, figuras, sumas, tamanno);
            metodo.generateSudoku();

            Console.WriteLine("Solucion de sudoku.");
            debug(solucion);


            generarFiguras();

            this.matriz = new int[2, tamanno, tamanno];
            for (int x = 0; x < tamanno; ++x)
            {
                for (int y = 0; y < tamanno; ++y)
                {
                    matriz[1, x, y] = 1;
                    matriz[0, x, y] = solucion[0,x, y];
                }

            }

            Random a = new Random();
            int ax = 0;
            int ay = 0;
            int c = 0;
            while (c != dificultad)
            {
                ax = a.Next(tamanno);
                ay = a.Next(tamanno);

                matriz[0, ax, ay] = matriz[1, ax, ay] = 0;
                ++c;
            }
            Console.WriteLine("Sudoku generado.");
            debug(matriz);

        }

        private void generarFiguras()
        {
            int cantidad = 48;
            int largo = 3;

            // 9x9 = 27,3
            // 10x10 = 50,2
            // 11x11 = -
            // 12x12 = 48,3
            // 13x13 = -
            // 14x14 = 50,2
            // 15x15 = 45,5
            // 16x16 = 16,8
            // 17x17 = -
            // 18x18 = 54,6
            // 19x19 = -

            figuras = new int[cantidad, largo, 2];
            sumas = new int[cantidad];

            int matrizX = 0;
            int matrizY = 0;
            int suma = 0;

            for (int x = 0; x < cantidad; ++x)
            {
                for (int y = 0; y < largo; ++y)
                {
                    suma = solucion[0,matrizX, matrizY] + suma;
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

        }

    }
}
