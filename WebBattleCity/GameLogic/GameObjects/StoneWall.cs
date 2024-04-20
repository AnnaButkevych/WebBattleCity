using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class StoneWall : WallsBase
{

    public StoneWall(int x, int y) : base(x, y, 4, 'ߛ')
    {
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

    public override string GetIconName()
    {
        return "stoneWall.jpg";
    }
}
