using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class StoneWall : WallsBase
{

    public StoneWall(int x, int y) : base(x, y, 3)
    {
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

    public override string GetIconName()
    {
        return "stoneWall.jpg";
    }
}
