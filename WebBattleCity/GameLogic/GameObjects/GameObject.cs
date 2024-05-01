using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class GameObject
{
    private int x;
    public int X
    {
        get { return x; }
        set
        {
            if (value >= 0 && value < 10)
            {
                x = value;
            }
        }
    }

    private int y;
    public int Y {
        get { return y; }
        set {
            if (value >= 0 && value < 10)
            {
                y = value;
            }
        } }
    public bool IsDestroyed;
    private int Hp;

    public GameObject(int x, int y, int hp)
    {
        X = x;
        Y = y;
        Hp = hp;
    }


    public bool TryDestroy(Projectile projectile)
     {
        if(projectile == null || projectile.Owner != this)
        {
            Hp--;
            if (Hp < 1)
            {
                IsDestroyed = true;
            }
        }
        
        return IsDestroyed;
    }

    public virtual string GetIconName()
    {
        return "";
    }
}
