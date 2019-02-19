using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BrainFuck
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            brainFuckInterpeter bs = new brainFuckInterpeter();
            bs.start();
        }

    }

    internal class brainFuckInterpeter
    {
        private int programPosition = 0;
        private int memoryPosition = 0;
        private List<byte> memory = new List<byte>();
        
        private string text;

        public void start()
        {
            memory.Add(0);
            text = File.ReadAllText("kod.txt");
            while (text.Length > programPosition) {
                char letter = text[programPosition];
                switch (letter) {
                    case '>':
                        gotoRight();
                        break;
                    case '<':
                        gotoLeft();
                        break;
                    case '+':
                        add();
                        break;
                    case '-':
                        sub();
                        break;
                    case '.':
                        print();
                        break;
                    case ',':
                        input();
                        break;
                    case '[':
                        loop();
                        break;
                    case ']':
                        loopAgain();
                        break;
                }
                programPosition++;
            }

        }

        private void gotoRight()
        {
            memoryPosition++;
            if (memoryPosition >= memory.Count) {
                memory.Add(0);
            }
        }

        private void gotoLeft()
        {
            memoryPosition--;
            if (memoryPosition < 0) {
                Console.WriteLine("Vi är utanför listan....");
            }
        }

        private void add()
        {
            memory[memoryPosition]++;
        }

        private void sub()
        {
            memory[memoryPosition]--;
        }

        private void print()
        {
            Console.Write(Convert.ToChar(memory[memoryPosition]));
        }

        private void input()
        {
            memory[memoryPosition] = (byte)Convert.ToChar(Console.ReadLine());
        }

        private void loop()
        {
            
            if (memory[memoryPosition] == 0) {
                string tempText = text.Substring(programPosition);
                int OpenBracketNr = 0;
                //programPosition++;
                foreach (char letter in tempText) {
                    switch (letter) {
                        case '[':
                            OpenBracketNr++;
                            break;
                        case ']':
                            OpenBracketNr--;
                            if (OpenBracketNr == 0) {
                                return;
                            }
                            break;
                    }
                    programPosition++;
                }
            }
        }

        private void loopAgain()
        {
            if (memory[memoryPosition] != 0) {
                string tempText = text.Substring(0,programPosition);
                IEnumerable<char> t = tempText.Reverse();

                int OpenBracketNr = 0;
                programPosition--;
                foreach (char letter in t) {
                    switch (letter) {
                        case '[':
                            OpenBracketNr--;
                            if (OpenBracketNr == -1) {
                                return;
                            }
                            break;
                        case ']':
                            OpenBracketNr++;

                            break;
                    }
                    programPosition--;
                }
            }
        }


    }
}
