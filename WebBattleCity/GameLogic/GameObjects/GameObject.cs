using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class GameObject
{
    public int X;
    public int Y;
    public char Icon;
    public bool IsDestroyed;

    public GameObject(int x, int y)
    {
        X = x;
        Y = y;
    }


    public virtual bool TryDestroy()
    {
        IsDestroyed = true;
        Icon = '྾';
        return IsDestroyed;
    }

    public virtual string GetIconName()
    {
        return "";
    }
}
