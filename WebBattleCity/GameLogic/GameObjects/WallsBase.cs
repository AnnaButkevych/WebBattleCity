using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WallsBase : GameObject
{
    public int NumberOfShootsToBreak;

    public WallsBase(int x, int y, int numberOfShootsToBreak) : base(x, y)
    {
        NumberOfShootsToBreak = numberOfShootsToBreak;
        IsDestroyed = false;
    }

    public override bool TryDestroy()
    {
        NumberOfShootsToBreak--;
        if (NumberOfShootsToBreak < 1)
        {
            IsDestroyed = true;
        }
        return IsDestroyed;
    }
}
