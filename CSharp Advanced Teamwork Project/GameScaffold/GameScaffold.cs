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
        //hide cursor
        //set absolute height and width
        //set GUI height and width
        //set playing board height and width
        //draw player stats (health, damage etc.)
        //draw score

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

        //Drawing the screen
        //clear the previous screen first
        //draw objects on the matrix
        //print the matrix

        //Clearing all states

        //GameOver


        //Thread Sleep (Game Speed)
    }
    //Initialization
    static void InitializeObjects()
    {
    }

    //GUI
    static void RefreshGUI()
    {
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
