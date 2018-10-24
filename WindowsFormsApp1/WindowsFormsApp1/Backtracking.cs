using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsFormsApp1
{
    class Backtracking
    {

        Stack <byte> instructionsStack = new Stack<byte>();
        int tamanno = 0;


        int[,,] matriz;
        int[,,] figuras;
        int[] sumas;


        int actualX;
        int actualY;

        int threadNumber = 0;

        public Backtracking(int[,,] matriz, int[,,]figuras, int[]sumas, int tamanno)
        {


            this.matriz = matriz;
            this.figuras = figuras;
            this.sumas = sumas;
            this.tamanno = tamanno;

            //debug(this.matriz);
            /*
            for (int y = 0; y < 27; ++y)
            {
                Console.WriteLine(sumas[y]);
                for (int x = 0; x < 3; ++x)
                {
                    Console.WriteLine(matrizEjemplo[figuras[y, x, 0], figuras[y, x, 1]]);
                }
                Console.Write("\n");
            }
            */
        }


    
        public void debug(int[,,] matrizB)
        {

            //normal
            for (int y = 0; y < this.tamanno; ++y)
            {
                for (int x = 0; x < this.tamanno; ++x)
                {
                    Console.Write(matrizB[0, x, y]);
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");

            //los 1 y 0
            /*
            for (int y = 0; y < 9; ++y)
            {
                for (int x = 0; x < 9; ++x)
                {
                    Console.Write(matrizB[1, x, y]);
                    Console.Write("\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");*/
        }



        private void applyInstruction()
        {
            matriz[0,actualX, actualY] = instructionsStack.Pop();
        }

        /*
         * Poda de comienzo, soluciones que no cambian y no fueron dadas.
         */
        private void strictSolutions()
        {
            //Console.WriteLine("poda");
            int numero = 0;
            for (int y = 0; y < tamanno; ++y)
            {
                for (int x = 0; x < tamanno; ++x)
                {
                    if (this.matriz[1, actualX, actualY] == 0)
                    {
                        actualX = x;
                        actualY = y;
                        addInstructions();

                        numero = instructionsStack.Pop();
                        if (instructionsStack.Pop() == 0)
                        {
                            this.matriz[0, actualX, actualY] = numero;
                            this.matriz[1, actualX, actualY] = 1;
                            //Console.WriteLine(actualX + "\t" + actualY);
                        }
                    }
                }
            }
            instructionsStack.Clear();
            //Console.WriteLine("fin poda");
        }


        private bool addInstructions()
        {
   
            //Revisa cuales numeros pueden ir por la fila y columna
            bool[] posibleNumbers = new bool[19] {true,true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }; // 19 numeros
            int actualNumber = 0;


            for (int y = 0; y < this.tamanno ; ++y){
                actualNumber = matriz[0,actualX,y];
                if(actualNumber != 0) {
                    posibleNumbers[actualNumber - 1] = false;
                }
            }

            for (int x = 0; x < this.tamanno; ++x)
            {
                actualNumber = matriz[0,x, actualY];
                if (actualNumber != 0)
                {
                    posibleNumbers[actualNumber - 1] = false;
                }
            }



            //Encuentra elemento en la figura
            int figureIndex = 0;
            int figureX = 0;
            while (this.figuras[figureIndex, figureX, 0] != actualX || this.figuras[figureIndex, figureX, 1] != actualY)
            {
                ++figureX;
                if (figureX == this.figuras.GetLength(1)) //cantidad de elementos por figura
                {
                    ++figureIndex;
                    figureX = 0;
                }
            }



            //calcula suma actual con posible numero
            int suma = 0;
            for (int figure = 0; figure < figuras.GetLength(1); ++figure)
            {
                suma = suma + this.matriz[0, this.figuras[figureIndex, figure, 0], this.figuras[figureIndex, figure, 1]];
            }

            for (int number = 0; number < posibleNumbers.Length; ++number)
            {
                if (posibleNumbers[number] && (number + 1 + suma) > sumas[figureIndex])
                {
                    posibleNumbers[number] = false;
                }
            }

    

            //Agrega los posibles numeros a la pila
            bool empty = true;
            int resultNumber = 0;
            instructionsStack.Push(0); //no quedan mas opciones

            for (int number = 0; number < this.tamanno; ++number)
            {
                if (posibleNumbers[number])
                {
                    empty = false;
                    resultNumber = number + 1;
                    instructionsStack.Push((byte)resultNumber);
                }
            }

            if (empty)
            {
                instructionsStack.Pop();
            }

            return empty;
        }

        private bool reverseCoordinates()
        {
            bool reversing = true;
            --actualX;
            if (actualX == -1)
            {
                --actualY;
                if(actualY == -1)
                {
                    reversing = false;
                    actualX = 0;
                    actualY = 0;
                }
                else
                {
                    actualX = this.tamanno - 1;
                }
            }
            return reversing;
        }

        private void continueCoordinates()
        {
            ++actualX;
            if (actualX == this.tamanno)
            {
                ++actualY;
                actualX = 0;
            }
        }




        /*
         * Algoritmo que recorre el arbol.
        */
        public void resolver()
        {

            //debug(this.matriz);
            strictSolutions();


            actualX = 0;
            actualY = 0;

            bool empty = false;
            bool reversing = false;

            //int cyclesDebug = 0;
            //int number = 1000000;

            while(actualY < this.tamanno && actualX < this.tamanno)
            {
                /*
                ++cyclesDebug;
                if(cyclesDebug == number)
                {
                    Console.WriteLine(cyclesDebug);
                    debug(this.matriz);
                    number = number + 1000000;
                    Console.WriteLine(this.matriz[0, actualX, actualY] + "\t" + actualX + "\t" + actualY + "\t" + this.matriz[1, actualX, actualY]);
                }*/

                //Console.WriteLine(this.matriz[0, actualX, actualY] + "\t" + actualX + "\t" + actualY + "\t" + this.matriz[1, actualX, actualY]);
                //Console.WriteLine(actualX + "\t" + actualY);
                //si esta en reversa no hace falta calcular las posibles opciones
                if (!reversing && this.matriz[1, actualX, actualY] == 0)
                {

                    empty = addInstructions();

                    if (empty)
                    {
                        reversing = reverseCoordinates();
                        
                    }
                    else
                    {
                        //Console.WriteLine("In normal");
                        applyInstruction();
                        continueCoordinates();
                    }
                }
                else if (reversing && this.matriz[1, actualX, actualY] == 0)
                {
                    //Console.WriteLine("In reverse");
                    applyInstruction();

                    if (this.matriz[0, actualX, actualY] == 0)
                    {
                        //Se mantiene en reversa
                        reversing = reverseCoordinates();
                    }
                    else
                    {
                        //Ya no hace falta seguir en reversa
                        reversing = false;
                        continueCoordinates();
                    }
                }
                else if (!reversing && this.matriz[1, actualX, actualY] == 1)
                {
                    continueCoordinates();
                }
                else if (reversing && this.matriz[1, actualX, actualY] == 1)
                {
                    reversing = reverseCoordinates();
                }

            }


            debug(this.matriz);
        }



        private bool soluciones()
        {

            //Revisa cuales numeros pueden ir por la fila y columna
            bool[] posibleNumbers = new bool[19] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true }; // 19 numeros
            int actualNumber = 0;


            for (int y = 0; y < this.tamanno; ++y)
            {
                actualNumber = matriz[0, actualX, y];
                if (actualNumber != 0)
                {
                    posibleNumbers[actualNumber - 1] = false;
                }
            }

            for (int x = 0; x < this.tamanno; ++x)
            {
                actualNumber = matriz[0, x, actualY];
                if (actualNumber != 0)
                {
                    posibleNumbers[actualNumber - 1] = false;
                }
            }

            //Agrega los posibles numeros a la pila
            bool empty = true;
            int resultNumber = 0;
            instructionsStack.Push(0); //no quedan mas opciones

            for (int number = 0; number < this.tamanno; ++number)
            {
                if (posibleNumbers[number])
                {
                    empty = false;
                    resultNumber = number + 1;
                    instructionsStack.Push((byte)resultNumber);
                }
            }

            if (empty)
            {
                instructionsStack.Pop();
            }

            return empty;
        }




        public int[,,] generateSudoku()
        {

            actualX = 0;
            actualY = 0;

            bool empty = false;
            bool reversing = false;

            //int cyclesDebug = 0;
            //int number = 1000000;

            while (actualY < this.tamanno && actualX < this.tamanno)
            {
                /*
                ++cyclesDebug;
                if(cyclesDebug == number)
                {
                    Console.WriteLine(cyclesDebug);
                    debug(this.matriz);
                    number = number + 1000000;
                    Console.WriteLine(this.matriz[0, actualX, actualY] + "\t" + actualX + "\t" + actualY + "\t" + this.matriz[1, actualX, actualY]);
                }
                */
                //Console.WriteLine(this.matriz[0, actualX, actualY] + "\t" + actualX + "\t" + actualY + "\t" + this.matriz[1, actualX, actualY]);
                //Console.WriteLine(actualX + "\t" + actualY);
                //si esta en reversa no hace falta calcular las posibles opciones
                if (!reversing && this.matriz[1, actualX, actualY] == 0)
                {

                    empty = soluciones();

                    if (empty)
                    {
                        reversing = reverseCoordinates();

                    }
                    else
                    {
                        //Console.WriteLine("In normal");
                        applyInstruction();
                        continueCoordinates();
                    }
                }
                else if (reversing && this.matriz[1, actualX, actualY] == 0)
                {
                    //Console.WriteLine("In reverse");
                    applyInstruction();

                    if (this.matriz[0, actualX, actualY] == 0)
                    {
                        //Se mantiene en reversa
                        reversing = reverseCoordinates();
                    }
                    else
                    {
                        //Ya no hace falta seguir en reversa
                        reversing = false;
                        continueCoordinates();
                    }
                }
                else if (!reversing && this.matriz[1, actualX, actualY] == 1)
                {
                    continueCoordinates();
                }
                else if (reversing && this.matriz[1, actualX, actualY] == 1)
                {
                    reversing = reverseCoordinates();
                }

            }
            return matriz;
        }



        private void runNewObject()
        {
            Backtracking variableBacktracking = new Backtracking(matriz, figuras, sumas, tamanno);
            variableBacktracking.resolver();
            Console.WriteLine("Fin de hilo: " + threadNumber + ".");
        }

        public void resolverHilos()
        {
            Console.WriteLine("Inicio hilos.");

            actualX = 0; //primer espacio X
            actualY = 0; //primer espacio Y
            while (this.matriz[1, actualX, actualY] != 0)
            {
                ++actualX;
                if(actualX == this.tamanno)
                {
                    actualX = 0;
                    actualY++;
                }
            }

            addInstructions();

            byte[] arregloPosibilidades = new byte[instructionsStack.Count];
            arregloPosibilidades = instructionsStack.ToArray();
            instructionsStack.Clear();
            int quantityPosibilites = arregloPosibilidades.Length-1;
            Thread[] threads = new Thread[quantityPosibilites];


            Console.WriteLine("Rama\t-\tNumero de hilo");
            for (int thread = 0; thread < quantityPosibilites; ++thread) // se toma en cuenta el 0 que pone la pila
            {
                this.matriz[0, actualX, actualY] = arregloPosibilidades[thread];
                this.matriz[1, actualX, actualY] = 1;

                Console.WriteLine(this.matriz[0, actualX, actualY] + "\t-\t" + threadNumber);
                threads[thread] = new Thread(runNewObject);
                threads[thread].Start();
                ++threadNumber;
            }
            Console.WriteLine("\n");

            bool finished = false;
            int index = 0;
            while (!finished)
            {
                if (!threads[index].IsAlive)
                {
                    for(int thread = 0; thread < quantityPosibilites; ++thread)
                    {
                        threads[thread].Abort();
                        finished = true;
                    }
                }
                ++index;
                if(index >= quantityPosibilites)
                {
                    index = 0;
                }
            }
        }
    }
}
