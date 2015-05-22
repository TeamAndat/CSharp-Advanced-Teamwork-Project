using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;



class Player
{
    public int HP { get; set; }
    public int dmg { get; set; }
    public char shape { get; set; }
    public ConsoleColor color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Player()
    {
        this.HP = 100;
        this.dmg = 10;
        this.shape = '\u263B';
        this.color = ConsoleColor.Cyan;
        this.X = 0;
        this.Y = 0;
    }
}
class Enemies
{
    public int HP { get; set; }
    public int dmg { get; set; }
    public char shape { get; set; }
    public ConsoleColor color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Enemies(int x, int y)
    {
        this.HP = 20;
        this.dmg = 5;
        this.shape = '\u263A';
        this.color = ConsoleColor.Red;
        this.X = x;
        this.Y = y;
    }
}
class Terrain
{
    public char shape { get; set; }
    public ConsoleColor color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Terrain(int x, int y)
    {
        char[] shapes = { '\u2588', '\u2580', '\u2593', '\u2592', '\u2591' };
        Random random = new Random();
        this.color = ConsoleColor.White;
        this.X = x;
        this.Y = y;
        this.shape = shapes[random.Next(0, 5)];
    }
}
class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
class Game
{
    const int BoardWidth = 65;
    const int BoardHeight = 25;
    const int GuiWidth = 25;
    const int GameWidth = BoardWidth + GuiWidth;
    const int GameHeight = BoardHeight;
    static int health = 100;
    static int score = 0;
    static Random random = new Random();
    static bool[,] boardPositions = new bool[GameHeight, GameWidth];
    static List<bool> willMove = new List<bool>();
    static List<Position> previousEnemyPosition = new List<Position>();
    //Static declaration so it's easily accesible
    //Declaration
    //declare player object
    //declare terrain (list of the struct type, fill them upon initialization)
    //declare enemy objects (list of the struct type, fill them upon initialization)
    //movement
    //attack
    //enemy movement

    static void Main()
    {
        // Console settings and GUI Initialization
        Console.CursorVisible = false;
        Console.Title = ""; // Add title
        Console.WindowWidth = GameWidth;
        Console.BufferWidth = GameWidth;
        Console.WindowHeight = GameHeight;
        Console.BufferHeight = GameHeight;
        PrintGUI();
        //Initialization
        //player can be initialized entirely with a constructor in the struct
        //Initialize Terrain
        int numberOfTerrainObjects = random.Next(20, 40);
        for (int i = 0; i < numberOfTerrainObjects; i++)
        {
            Terrain newTerrain = new Terrain(random.Next(0, BoardWidth), random.Next(0, BoardHeight));
            Console.SetCursorPosition(newTerrain.X, newTerrain.Y);
            if (newTerrain.X > 2 && newTerrain.Y > 2)
            {
                Console.ForegroundColor = newTerrain.color;
                Console.Write(newTerrain.shape);
                boardPositions[newTerrain.Y, newTerrain.X] = true;
            }
        }
        //Initialize Enemies
        int numberOfEnemies = random.Next(4, 12);
        int iterations = numberOfEnemies;
        for (int i = 0; i < iterations; i++)
        {
            Enemies newEnemy = new Enemies(random.Next(0, BoardWidth), random.Next(0, BoardHeight));
            Console.SetCursorPosition(newEnemy.X, newEnemy.Y);
            if (boardPositions[newEnemy.Y, newEnemy.X] == false && (newEnemy.X > 2 && newEnemy.Y > 2))
            {
                Console.ForegroundColor = newEnemy.color;
                Console.Write(newEnemy.shape);
                previousEnemyPosition.Add(new Position(newEnemy.X ,newEnemy.Y));
                willMove.Add(false);
            }
            else
            {
                iterations++;
            }
        }
        //while(true) running part of the game

        //GUI Refresh
        //change GUI values each tick

        //Read User Key
        //Declare movement
        //Declare Attack

        //AI Enemy Movement
        //Enemy Movement

        //CheckForCollision
        //Player Collision
        //Collision with terrain and out of bounds
        //Collision with enemies
        //Enemy Collision
        //COllision with terrain and out of bounds
        //Collision with other enemies and their previous positions

        //Drawing the screen
        //clear the previous screen first
        //draw objects on the matrix
        //print the matrix

        //Clearing all states

        //GameOver


        //Thread Sleep (Game Speed)
    }
    //<<<<<<< HEAD
    static void PrintGUI()
    {
        for (int row = 0; row < GameHeight; row++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(BoardWidth + 1, row);
            Console.Write("║");
        }
        Console.SetCursorPosition(BoardWidth + (GuiWidth - 7) / 2, 1);
        Console.WriteLine("Controls:");
        Console.SetCursorPosition(BoardWidth + GuiWidth / 2, 3);
        Console.WriteLine(" ↑");
        Console.SetCursorPosition(BoardWidth + 3, 4);
        Console.WriteLine(" Move:   ← →");
        Console.SetCursorPosition(BoardWidth + GuiWidth / 2, 5);
        Console.WriteLine(" ↓");
        Console.SetCursorPosition(BoardWidth + 2, 7);
        Console.WriteLine(" Attack:   Z");
        Console.SetCursorPosition(BoardWidth + 2, 9);
        Console.WriteLine(new string('═', GuiWidth - 2));
        Console.SetCursorPosition(BoardWidth + (GuiWidth - 12) / 2, 11);
        Console.WriteLine("Player Stats:");
        Console.SetCursorPosition(BoardWidth + 4, 13);
        Console.WriteLine("Health:");
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine(health);
        Console.SetCursorPosition(BoardWidth + 2, 18);
        Console.WriteLine(new string('═', GuiWidth - 2));
        Console.SetCursorPosition(BoardWidth + 4, 20);
        Console.WriteLine("Score:");
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine(score);
    }
    //Initialization
    static void InitializeObjects()
    {
    }

    //GUI
    static void LowerHealth(int lower)
    {
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine("   ");
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine(health - lower);
    }
    static void ChangeScore(int addScore)
    {
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine("            ");
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine(score + addScore);
    }

    //Read User Key
    static void ReadUserKey()
    {
    }

    //Colision
    static void CheckForCollision()
    {
    }

    //Enemy AI
    static void EnemyAi()
    {
    }

    //Draw the screen
    static void DrawTheScreen()
    {
    }

    //Clear States
    static void ClearStates()
    {
    }
    //Game Over
    static void GameOver()
    {
    }
}
