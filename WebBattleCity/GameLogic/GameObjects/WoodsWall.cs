using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WoodsWall : WallsBase
{

    public WoodsWall(int x, int y) : base(x, y, 1, 'ແ')
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
}