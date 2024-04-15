using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WallsBase : GameObject
{
    public int NumberOfShootsToBreak;

    public WallsBase(int x, int y, int numberOfShootsToBreak, char icon) : base(x, y)
    {
        NumberOfShootsToBreak = numberOfShootsToBreak;
        Icon = icon;
        IsDestroyed = false;
    }

    public override bool TryDestroy()
    {
        NumberOfShootsToBreak--;
        if (NumberOfShootsToBreak < 1)
        {
            IsDestroyed = true;
            Icon = ' ';
        }
        return IsDestroyed;
    }
}
