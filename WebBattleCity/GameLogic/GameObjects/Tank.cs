using System;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public abstract class Tank : GameObject
{
    public Vector CurrentVector = Vector.Up;

    public Tank(int x, int y, int hp) : base(x, y, hp)
    {
        IsDestroyed = false;
    }

    public Projectile? Fire()
    {
        if (!IsDestroyed)
        {
            switch (CurrentVector)
            {
                case Vector.Left:
                    return new Projectile(X - 1, Y, CurrentVector, this);
                case Vector.Right:
                    return new Projectile(X + 1, Y, CurrentVector, this);
                case Vector.Up:
                    return new Projectile(X, Y - 1, CurrentVector, this);
                case Vector.Down:
                    return new Projectile(X, Y + 1, CurrentVector, this);
                default: throw new Exception();
            }
        }
        return null;
    }

    public Projectile? FireToVector(Vector newVector)
    {
        CurrentVector = newVector;
        return Fire();
    }

    public void TurnAndMove(Vector newVector, BattleField battleField)
    {
        CurrentVector = newVector;
        if (newVector == Vector.Up)
        {
            Y--;
        }

        if (newVector == Vector.Down)
        {
            Y++;
        }

        if (newVector == Vector.Right)
        {
            X++;
        }

        if (newVector == Vector.Left)
        {
            X--;
        }
    }
}
