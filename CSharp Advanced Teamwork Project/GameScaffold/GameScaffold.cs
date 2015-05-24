using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;




class Game
{
    //Declarations

    //random
    static Random random = new Random();

    //player
    static Player player = new Player();
    static bool hasAttacked = false;
    static Position attackedPosition = new Position(0, 0);
    static Position previousPosition = new Position(player.X, player.Y);
    static string currentDirection = "right";

    //terrain
    static List<Terrain> terrain = new List<Terrain>();
    static bool[,] terrainPositions = new bool[BoardWidth, BoardHeight];

    //enemies
    static List<Enemies> enemies = new List<Enemies>();
    static bool[,] enemyPositions = new bool[BoardWidth, BoardHeight];
    static bool[,] previousEnemyField = new bool[BoardWidth, BoardHeight];
    static List<bool> willMove = new List<bool>();
    static List<Position> previousEnemyPosition = new List<Position>();

    //GUI
    const int BoardWidth = 65;
    const int BoardHeight = 25;
    const int GuiWidth = 25;
    const int GameWidth = BoardWidth + GuiWidth;
    const int GameHeight = BoardHeight;
    static int score = 0;

    static void Main()
    {
        // Console settings and GUI Initialization
        Console.CursorVisible = false;
        Console.Title = "Dungeon Crawler"; // Add title
        Console.WindowWidth = GameWidth;
        Console.BufferWidth = GameWidth;
        Console.WindowHeight = GameHeight;
        Console.BufferHeight = GameHeight;
        PrintGUI();

        //Initialization
        InitializeObjects();

        //Initial Draw

        //Draw Terrain 
        foreach (var terrainObject in terrain)
        {
            Console.SetCursorPosition(terrainObject.X, terrainObject.Y);
            Console.ForegroundColor = terrainObject.color;
            Console.Write(terrainObject.shape);
        }

        //while(true) running part of the game

        while (true)
        {
            ReadUserKey();
            EnemyAi();
            CheckForCollision();
            DrawTheScreen();
            DrawHealth();
            ChangeScore();
            ClearStates();

            //Game Over
            if (player.HP <= 0)
            {
                Console.SetCursorPosition(BoardWidth / 2 - 6, BoardHeight / 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GAME OVER!!!");
                Console.SetCursorPosition(BoardWidth / 2 - 6, BoardHeight / 2 + 1);
                Console.WriteLine("Score: {0}", score);
                Console.SetCursorPosition(BoardWidth / 2 - 12, BoardHeight / 2 + 2);
                Thread.Sleep(2500);
                return;
            }

            //Victory
            if(enemies.Count == 0)
            {
                Console.SetCursorPosition(BoardWidth / 2 - 6, BoardHeight / 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VICTORY!!!");
                Console.SetCursorPosition(BoardWidth / 2 - 6, BoardHeight / 2 + 1);
                Console.WriteLine("Score: {0}", score);
                Console.SetCursorPosition(BoardWidth / 2 - 12, BoardHeight / 2 + 2);
                Thread.Sleep(2500);
                return;
            }

            //Thread Sleep (Game Speed)
            score = score + 1;
            Thread.Sleep(175);
        }

    }

    //Initialization
    static void InitializeObjects()
    {
        //Initialize Terrain
        int numberOfTerrainObjects = random.Next(300, 350);
        for (int i = 0; i < numberOfTerrainObjects; i++)
        {
            Terrain newTerrain = new Terrain(random.Next(0, BoardWidth), random.Next(0, BoardHeight), random.Next(0, 100));
            if ((newTerrain.X > 1 || newTerrain.Y > 1) && terrainPositions[newTerrain.X, newTerrain.Y] == false)
            {
                terrainPositions[newTerrain.X, newTerrain.Y] = true;
                terrain.Add(newTerrain);
            }
            else i = i - 1;

        }
        //Initialize Enemies
        int numberOfEnemies = random.Next(10, 20);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Enemies newEnemy = new Enemies(random.Next(0, BoardWidth), random.Next(0, BoardHeight));
            if (terrainPositions[newEnemy.X, newEnemy.Y] == false && enemyPositions[newEnemy.X, newEnemy.Y] == false && (newEnemy.X > 2 || newEnemy.Y > 2))
            {
                enemyPositions[newEnemy.X, newEnemy.Y] = true;
                previousEnemyPosition.Add(new Position(newEnemy.X, newEnemy.Y));
                willMove.Add(false);
                enemies.Add(newEnemy);
            }
            else i = i - 1;
        }
    }

    //GUI

    static void PrintGUI()
    {
        for (int row = 0; row < GameHeight; row++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(BoardWidth, row);
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
        Console.SetCursorPosition(BoardWidth + 1, 9);
        Console.WriteLine(new string('═', GuiWidth - 1));
        Console.SetCursorPosition(BoardWidth + (GuiWidth - 12) / 2, 11);
        Console.WriteLine("Player Stats:");
        Console.SetCursorPosition(BoardWidth + 4, 13);
        Console.WriteLine("Health:");
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine(player.HP);
        Console.SetCursorPosition(BoardWidth + 1, 18);
        Console.WriteLine(new string('═', GuiWidth - 1));
        Console.SetCursorPosition(BoardWidth + 4, 20);
        Console.WriteLine("Score:");
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine(score);
    }
    static void DrawHealth()
    {
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine("   ");
        Console.SetCursorPosition(BoardWidth + 15, 13);
        Console.WriteLine(player.HP);
    }
    static void ChangeScore()
    {
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine("            ");
        Console.SetCursorPosition(BoardWidth + 13, 20);
        Console.WriteLine(score);
    }

    //Read User Key
    static void ReadUserKey()
    {
        if(Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            //Declare movement
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                previousPosition = new Position(player.X, player.Y);
                player.X = player.X;
                player.Y = player.Y - 1;
                currentDirection = "up";
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                previousPosition = new Position(player.X, player.Y);
                player.X = player.X;
                player.Y = player.Y + 1;
                currentDirection = "down";
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                previousPosition = new Position(player.X, player.Y);
                player.X = player.X + 1;
                player.Y = player.Y;
                currentDirection = "right";
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                previousPosition = new Position(player.X, player.Y);
                player.X = player.X - 1;
                player.Y = player.Y;
                currentDirection = "left";
            }
            //Declare Attack
            else if (keyInfo.Key == ConsoleKey.Z)
            {
                switch (currentDirection)
                {
                    case "up":
                        attackedPosition.X = player.X;
                        attackedPosition.Y = player.Y - 1;
                        break;
                    case "down":
                        attackedPosition.X = player.X;
                        attackedPosition.Y = player.Y + 1;
                        break;
                    case "right":
                        attackedPosition.X = player.X + 1;
                        attackedPosition.Y = player.Y;
                        break;
                    case "left":
                        attackedPosition.X = player.X - 1;
                        attackedPosition.Y = player.Y;
                        break;
                }
                if (attackedPosition.X >= 0 && attackedPosition.X < BoardWidth &&
                    attackedPosition.Y >= 0 && attackedPosition.Y < BoardHeight &&
                    terrainPositions[attackedPosition.X, attackedPosition.Y] == false)
                {
                    hasAttacked = true;
                }

            }
        }
        
    }

    //Colision
    static void CheckForCollision()
    {

        //Check for enemy collision
        for (int i = 0; i < enemies.Count; i++)
        {
            if (willMove[i] == true)
            {
                if (enemies[i].X < 0 || enemies[i].X >= BoardWidth ||
                    enemies[i].Y < 0 || enemies[i].Y >= BoardHeight ||
                    terrainPositions[enemies[i].X, enemies[i].Y] ||
                    previousEnemyField[enemies[i].X, enemies[i].Y])
                {
                    enemies[i].X = previousEnemyPosition[i].X;
                    enemies[i].Y = previousEnemyPosition[i].Y;
                }
                else if (enemies[i].X == player.X && enemies[i].Y == player.Y)
                {
                    player.HP = player.HP - enemies[i].dmg;
                    enemies[i].X = previousEnemyPosition[i].X;
                    enemies[i].Y = previousEnemyPosition[i].Y;
                    Hurt();
                }
                else
                {
                    enemyPositions[previousEnemyPosition[i].X, previousEnemyPosition[i].Y] = false;
                    enemyPositions[enemies[i].X, enemies[i].Y] = true;
                }
            }
        }

        //Check for player collision with terrain or outside of bounds
        if (player.X < 0 || player.X >= BoardWidth ||
            player.Y < 0 || player.Y >= BoardHeight ||
            terrainPositions[player.X, player.Y])
        {
            player.X = previousPosition.X;
            player.Y = previousPosition.Y;
        }

        //Check for player collision with enemies
        if (enemyPositions[player.X, player.Y])
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].X == player.X && enemies[i].Y == player.Y)
                {
                    player.HP = player.HP - enemies[i].dmg;
                    Hurt();
                }
            }
            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(" ");
            player.X = previousPosition.X;
            player.Y = previousPosition.Y;
        }

        //Check for attacks
        for (int i = 0; i < enemies.Count; i++)
        {
            if (hasAttacked == true && attackedPosition.X == enemies[i].X && attackedPosition.Y == enemies[i].Y)
            {
                enemies[i].HP = enemies[i].HP - player.dmg;
                Hit();
                // Remove dead enemies
                if (enemies[i].HP <= 0)
                {
                    previousEnemyField[previousEnemyPosition[i].X, previousEnemyPosition[i].Y] = false;
                    enemyPositions[enemies[i].X, enemies[i].Y] = false;
                    enemies.RemoveAt(i);
                    willMove.RemoveAt(i);
                    previousEnemyPosition.RemoveAt(i);
                    score = score + 25;
                }
            }
        }
    }

    //Enemy AI
    static void EnemyAi()
    {
        for (int i = 0; i < willMove.Count; i++)
        {
            int num = random.Next(0, 100);
            if (num < 20)
            {
                willMove[i] = true;

                int direction = random.Next(0, 4);
                previousEnemyField[previousEnemyPosition[i].X, previousEnemyPosition[i].Y] = false;
                previousEnemyPosition[i].X = enemies[i].X;
                previousEnemyPosition[i].Y = enemies[i].Y;
                previousEnemyField[previousEnemyPosition[i].X, previousEnemyPosition[i].Y] = true;
                switch (direction)
                {
                    case 0:
                        // left
                        enemies[i].X -= 1;
                        break;
                    case 1:
                        // up
                        enemies[i].Y -= 1;
                        break;
                    case 2:
                        // right
                        enemies[i].X += 1;
                        break;
                    case 3:
                        // down
                        enemies[i].Y += 1;
                        break;
                }
                //subcheck for other enemies
                if (enemies[i].X > 0 && enemies[i].X < BoardWidth &&
                    enemies[i].Y > 0 && enemies[i].Y < BoardHeight &&
                    enemyPositions[enemies[i].X, enemies[i].Y] == true)
                {
                    enemies[i].X = previousEnemyPosition[i].X;
                    enemies[i].Y = previousEnemyPosition[i].Y;
                    willMove[i] = false;
                }


            }
        }
    }

    //Draw the screen
    static void DrawTheScreen()
    {
        //Clear attack
        if (hasAttacked == false &&
            attackedPosition.X >= 0 && attackedPosition.X < BoardWidth &&
            attackedPosition.Y >= 0 && attackedPosition.Y < BoardHeight &&
             terrainPositions[attackedPosition.X, attackedPosition.Y] == false)
        {
            Console.SetCursorPosition(attackedPosition.X, attackedPosition.Y);
            Console.Write(" ");
        }

        //Clear Player
        Console.SetCursorPosition(previousPosition.X, previousPosition.Y);
        Console.Write(" ");

        //Clear Enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            Console.SetCursorPosition(previousEnemyPosition[i].X, previousEnemyPosition[i].Y);
            Console.Write(" ");
        }

        //Draw Player
        Console.SetCursorPosition(player.X, player.Y);
        Console.ForegroundColor = player.color;
        Console.Write(player.shape);

        //Draw Enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            Console.SetCursorPosition(enemies[i].X, enemies[i].Y);
            Console.ForegroundColor = enemies[i].color;
            Console.Write(enemies[i].shape);
        }

        //Draw attack
        if (hasAttacked)
        {
            Console.SetCursorPosition(attackedPosition.X, attackedPosition.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\u2588');
        }
    }

    //Clear States
    static void ClearStates()
    {
        hasAttacked = false;
        for (int i = 0; i < willMove.Count; i++)
        {
            willMove[i] = false;
        }
    }

    //Sounds
    static void Hurt()
    {
        Console.Beep(175, 150);
    }
    static void Hit()
    {
        Console.Beep(1000, 150);

    }
}
