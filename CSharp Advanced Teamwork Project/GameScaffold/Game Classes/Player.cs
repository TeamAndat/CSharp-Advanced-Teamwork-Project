using System;

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
        this.HP = 50;
        this.dmg = 10;
        this.shape = '\u263B';
        this.color = ConsoleColor.Cyan;
        this.X = 0;
        this.Y = 0;
    }
}