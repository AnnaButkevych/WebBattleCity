using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class EmptyPosition : GameObject
{
    public EmptyPosition(int x, int y) : base(x, y, 1)
    {
    }

    public override string GetIconName()
    {
        return "emptyPosition.jpg";
    }
}