using System;

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
        this.HP = 40;
        this.dmg = 5;
        this.shape = '\u263A';
        this.color = ConsoleColor.Red;
        this.X = x;
        this.Y = y;
    }
}