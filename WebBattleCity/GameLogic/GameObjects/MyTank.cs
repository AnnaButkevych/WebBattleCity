using System;
using System.Threading.Tasks;
namespace WebBattleCity.GameLogic.GameObjects;

public class MyTank : Tank
{
    private int NumberOfShoots;
    public MyTank(int x, int y) : base(x, y)
    {
        NumberOfShoots = 8;
        Icon = 'ꥃ';
    }

    public override bool TryDestroy()
    {
        NumberOfShoots--;
        if (NumberOfShoots == 0)
        {
            IsDestroyed = true;
        }
        return IsDestroyed;
    }
}
