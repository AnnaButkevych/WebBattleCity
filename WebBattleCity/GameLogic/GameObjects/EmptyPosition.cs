using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class EmptyPosition : GameObject
{
    public EmptyPosition(int x, int y) : base(x, y)
    {
    }

    public override string GetIconName()
    {
        return "emptyPosition.jpg";
    }
}