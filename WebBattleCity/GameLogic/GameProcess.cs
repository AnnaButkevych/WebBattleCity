using System;
using WebBattleCity.GameLogic.GameLogicEnums;
using WebBattleCity.GameLogic.GameObjects;
namespace WebBattleCity.GameLogic;

public class GameProcess
{

    MyTank Leopard;
    List<EnemyTank> EnemyTanks;
    public BattleField BattleField;

    public GameProcess()
    {
        BattleField = new BattleField();
    }

    public GameObject[,] GetCurrentState()
    {
        return BattleField.State;
    }

    public GameObject[,] Process(ControlsKeysEnum input)
    {
        EnemyTanks = BattleField.enemyTanksProperty;
        Leopard = BattleField.MyTankProperty;
        Projectile myTankProjectile;

        List<Projectile> projectiles = new List<Projectile>();
        switch (input)
        {
            case ControlsKeysEnum.UpArrow:
                Leopard.TurnAndMove(Vector.Up, BattleField);
                break;
            case ControlsKeysEnum.DownArrow:
                Leopard.TurnAndMove(Vector.Down, BattleField);
                break;
            case ControlsKeysEnum.LeftArrow:
                Leopard.TurnAndMove(Vector.Left, BattleField);
                break;
            case ControlsKeysEnum.RightArrow:
                Leopard.TurnAndMove(Vector.Right, BattleField);
                break;
            case ControlsKeysEnum.SpaceBar:
                projectiles.Add(Leopard.Fire());
                break;
            case ControlsKeysEnum.None:
                break;
        }
        foreach (var enemyTank in EnemyTanks)
        {
            if (enemyTank.IsOnTheSameLine(Leopard) && !enemyTank.IsDestroyed)
            {
                Vector vectorToShoot = enemyTank.FindVectorToShoot(Leopard);
                projectiles.Add(enemyTank.FireToVector(vectorToShoot));
            }
            enemyTank.MoveEnemyTank(BattleField);
        }

        BattleField.UpdateField(projectiles);

        return BattleField.State;
    }

    public bool IsLoser()
    {
        if (Leopard != null)
        {
            return Leopard.IsDestroyed;
        }
        else
        {
            return false;
        }
    }

    public bool IsWinner()
    {
        if (EnemyTanks != null && EnemyTanks.Any())
        {
            return EnemyTanks.All(x => x.IsDestroyed);
        }
        else
        {
            return false;
        }
    }
}


