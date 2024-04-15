using System;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public abstract class Tank : GameObject
{
    public Vector Vector = Vector.Up;

    public Tank(int x, int y) : base(x, y)
    {
        IsDestroyed = false;
        Icon = 'ᐃ';

    }

    public Projectile Fire()
    {
        if (!IsDestroyed)
        {
            switch (Vector)
            {
                case Vector.Left:
                    return new Projectile(X - 1, Y, Vector);
                case Vector.Right:
                    return new Projectile(X + 1, Y, Vector);
                case Vector.Up:
                    return new Projectile(X, Y - 1, Vector);
                case Vector.Down:
                    return new Projectile(X, Y + 1, Vector);
                default: throw new Exception();
            }
        }
        return null;
    }

    public Projectile FireToVector(Vector newVector)
    {
        Vector = newVector;
        return Fire();
    }

    //todo:check for not going out of the battleField
    public void TurnAndMove(Vector newVector, BattleField battleField)
    {
        Vector = newVector;
        if (newVector == Vector.Up)
        {
            Icon = 'ᐃ';
            Y--;
        }

        if (newVector == Vector.Down)
        {
            Icon = 'ᐁ';
            Y++;
        }

        if (newVector == Vector.Right)
        {
            Icon = 'ᐅ';
            X++;
        }

        if (newVector == Vector.Left)
        {
            Icon = 'ᐊ';
            X--;
        }
    }
}
