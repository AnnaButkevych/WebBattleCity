using System;
using WebBattleCity.GameLogic.GameLogicEnums;
namespace WebBattleCity.GameLogic.GameObjects;

public class Projectile : GameObject
{
    public Vector Vector;

    public Projectile(int x, int y, Vector vector) : base(x, y)
    {
        Vector = vector;
        Icon = '꠪';
    }

    public void Move()
    {
        switch (Vector)
        {
            case Vector.Right:
                X++;
                break;
            case Vector.Left:
                X--;
                break;
            case Vector.Up:
                Y++;
                break;
            case Vector.Down:
                Y--;
                break;
        }
    }

    public override string GetIconName()
    {
        return "projectile.jpg";
    }
}

