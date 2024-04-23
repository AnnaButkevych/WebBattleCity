using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WoodsWall : WallsBase
{

    public WoodsWall(int x, int y) : base(x, y, 1)
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
        return "woodsWall.jpg";
    }
}