using System;
using WebBattleCity.GameLogic.GameLogicEnums;
namespace WebBattleCity.GameLogic.GameObjects;

public class Projectile : GameObject
{
    public Vector Vector;
    public bool IsOut = false;
    public Tank Owner;

    public Projectile(int x, int y, Vector vector, Tank owner) : base(x, y, 1)
    {
        Owner = owner;
        Vector = vector;
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

