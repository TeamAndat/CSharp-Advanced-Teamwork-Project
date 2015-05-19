using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Game
{
    struct Player
    {

    }
    struct Enemies
    {

    }
    struct Terrain
    {

    }
    struct Position
    {

    }
    const int BoardWidth = 65;
    const int BoardHeight = 25;
    const int GuiWidth = 25;
    const int GameWidth = BoardWidth + GuiWidth;
    const int GameHeight = BoardHeight;
    static int health = 100;
    static int score = 0;
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
        //randomly create between 0 and X terrain objects (generate random char from a list for shape within the constuctor)
        //randomly set terrain position (check that it isn't within 1 square to the player and not on top of itself)
        //randomly generate between 0 and Y enemy objects (colors and shape will be created by the constructor of the class)
        //randomly set enemies starting positions ( check it isn't near the player and not on top of terrain and not on top of itself)

        //while(true) running part of the game

        //GUI Refresh
        //change GUI values each tick

        //Read User Key
        //Declare movement
        //Declare Attack

        //AI Enemy Movement
        //Enemy Movemen

        //CheckForCollision
        //Player Collision
        //Collision with terrain and out of bounds
        //Collision with enemies
        //Enemy Collision
        //COllision with terrain and out of bounds
        //Collision with other enemies and their previous positions

        //GameOver


        //Thread Sleep (Game Speed)
    }
    static void PrintGUI()
    {
        for (int row = 0; row < GameHeight; row++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(BoardWidth + 1, row);
            Console.Write("║");
        }
        Console.SetCursorPosition(BoardWidth + (GuiWidth - 7)/ 2, 1);
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
        Console.SetCursorPosition(BoardWidth + (GuiWidth - 12) /2, 11);
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
    static void InitializeObjects()
    {
    }

    static void ReadUserKey()
    {
    }

    //GUI
    static void RefreshGUI()
    {
    }

    //Colision
    static void CheckForCollision()
    {
    }
    static void CollisionEnemies()
    {
    }
    static void CollisionTerrain()
    {
    }

    //Game Over
    static void GameOver()
    {
    }
}
