using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class BrickWall : WallsBase
{

    public BrickWall(int x, int y) : base(x, y, 2)
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
        return "brickWall.jpg";
    }
}