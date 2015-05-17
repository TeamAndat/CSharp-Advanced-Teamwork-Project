using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


class FallingRocks
{
    struct Element
    {
        public int X, Y;
        public char symbol;
        public ConsoleColor color;
        public Element(int x, int y, char symbol, ConsoleColor color)
        {
            this.X = x;
            this.Y = y;
            this.symbol = symbol;
            this.color = color;
        }
    }

    static void Position(int x, int y, char symbol, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }

    static void WriteString(int x, int y, string symbol, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }

    public static ConsoleColor RandomColor()
    {
        ConsoleColor color = ConsoleColor.White;
        Random generator = new Random();
        switch (generator.Next(1, 6))
        {
            case 1: color = ConsoleColor.Blue;
                break;
            case 2: color = ConsoleColor.Red;
                break;
            case 3: color = ConsoleColor.Green;
                break;
            case 4: color = ConsoleColor.Yellow;
                break;
            case 5: color = ConsoleColor.Cyan;
                break;
        }
        return color;

    }

    public static char RandomSymbol()
    {
        Random generator = new Random();
        char symbol = '!';
        switch (generator.Next(1, 10))
        {
            case 1: symbol = '*';
                break;
            case 2: symbol = '+';
                break;
            case 3: symbol = '&';
                break;
            case 4: symbol = '#';
                break;
            case 5: symbol = '@';
                break;
            case 6: symbol = '^';
                break;
            case 7: symbol = '%';
                break;
            case 8: symbol = '!';
                break;
            case 9: symbol = '.';
                break;
        }
        return symbol;
    }


    static void Main(string[] args)
    {
        //The list with positions of the dwarf body
        Element[] dwarfBody = new Element[3];

        //Random Generator
        Random randomGenerator = new Random();

        //Score counter
        int score = 0;

        //Sleep time
        double sleepTime = 150;

        //Available move directions
        Element[] moveDirections = new Element[]
        {
            new Element(1,0,' ',ConsoleColor.White), //Right
            new Element(-1,0,' ',ConsoleColor.White), //Left
        };

        //Current Direction
        int currentDirection = 0; //0 = Right; 1 = Left;

        //Console Settings
        Console.CursorVisible = false;
        Console.BufferHeight = Console.WindowHeight;

        //First Initialization
        List<Element> rocks = new List<Element>();
        for (int i = 0; i <= 2; i++)
        {
            if (i == 0)
            {
                dwarfBody[i] = new Element((Console.WindowWidth / 2) - 1 + i, Console.WindowHeight - 1, '(', ConsoleColor.White);
                Position(dwarfBody[i].X, dwarfBody[i].Y, dwarfBody[i].symbol, dwarfBody[i].color);
            }
            else if (i == 1)
            {
                dwarfBody[i] = new Element((Console.WindowWidth / 2) - 1 + i, Console.WindowHeight - 1, 'O', ConsoleColor.White);
                Position(dwarfBody[i].X, dwarfBody[i].Y, dwarfBody[i].symbol, dwarfBody[i].color);
            }
            else if (i == 2)
            {
                dwarfBody[i] = new Element((Console.WindowWidth / 2) - 1 + i, Console.WindowHeight - 1, ')', ConsoleColor.White);
                Position(dwarfBody[i].X, dwarfBody[i].Y, dwarfBody[i].symbol, dwarfBody[i].color);
            }
        }
        while (true)
        {
            //Read user Key
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.RightArrow)
                    currentDirection = 0;
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                    currentDirection = 1;
            }

            //Write new rocks
            int randomizeRocks = randomGenerator.Next(1, 5);
            Element[] rock = new Element[randomizeRocks];
            for (int i = 0; i < randomizeRocks; i++)
            {
                rock[i] = new Element(randomGenerator.Next(1, Console.WindowWidth - 2), 2, RandomSymbol(), RandomColor());
                rocks.Add(rock[i]);
            }

            //Writing the old rocks and collision detection
            List<Element> newRocksList = new List<Element>();
            for (int i = 0; i < rocks.Count; i++)
            {
                Element oldRocks = rocks[i];
                Element newRocks = new Element();
                newRocks.X = oldRocks.X;
                newRocks.Y = oldRocks.Y + 1;
                newRocks.symbol = oldRocks.symbol;
                newRocks.color = oldRocks.color;

                if (newRocks.Y == dwarfBody[0].Y && (newRocks.X == dwarfBody[0].X || newRocks.X == dwarfBody[1].X || newRocks.X == dwarfBody[2].X))
                {
                    WriteString(1, 2, "Game Over!", ConsoleColor.Red);
                    Console.WriteLine();
                    Console.ReadLine();
                    return;
                }
                if (newRocks.Y < Console.WindowHeight)
                {
                    newRocksList.Add(newRocks);
                }
            }

            //Moving the Dwarf
            if (dwarfBody.First().X > 1 && dwarfBody.Last().X < Console.WindowWidth - 2)
                for (int i = 0; i < dwarfBody.Length; i++)
                {
                    dwarfBody[i].X += moveDirections[currentDirection].X;
                }
            else if (dwarfBody.First().X == 1)
            {
                currentDirection = 0;
                for (int i = 0; i < dwarfBody.Length; i++)
                {
                    dwarfBody[i].X += moveDirections[currentDirection].X;
                }
            }
            else if (dwarfBody.Last().X == Console.WindowWidth - 2)
            {
                currentDirection = 1;
                for (int i = 0; i < dwarfBody.Length; i++)
                {
                    dwarfBody[i].X += moveDirections[currentDirection].X;
                }
            }

            //
            rocks = newRocksList;
            Console.Clear();
            foreach (var element in dwarfBody)
            {
                Position(element.X, element.Y, element.symbol, element.color);
            }
            foreach (var element in rocks)
            {
                Position(element.X, element.Y, element.symbol, element.color);
            }
            WriteString(1, 1, "Score:" + score, ConsoleColor.White);
            Thread.Sleep(150);
            score++;
        }
    }
}

