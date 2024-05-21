using WebBattleCity.GameLogic.GameLogicEnums;
using WebBattleCity.GameLogic.GameObjects;
namespace WebBattleCity.GameLogic;

public class GameProcess
{
    private Base _base;
    List<MyTank?> MyTanks = new();
    List<EnemyTank> EnemyTanks;
    public BattleField BattleField;
    readonly Random _random = new();

    public GameProcess()
    {
        BattleField = new BattleField();
    }

    public GameObject?[,] GetCurrentState()
    {
        return BattleField.State;
    }

    public void AddUser(string tankId)
    {
        foreach (var tank in BattleField.MyTankProperty)
        {
            if (string.IsNullOrWhiteSpace(tank.Id))
            {
                tank.Id = tankId;
                return;
            }
        }
        
        if (!BattleField.MyTankProperty.Any(x => x.Id == tankId))
        {
            for (int i = 0; i < BattleField.Length; i++)
            {
                for (int j = 0; j < BattleField.Height; j++)
                {
                    if (BattleField.State[j, i] is EmptyPosition)
                    {
                        var myTank = new MyTank(j, i, tankId);
                        BattleField.State[j, i] = myTank;
                        BattleField.MyTankProperty.Add(myTank);
                        return;
                    }
                }
            }
        }
    }

    public GameObject?[,] Process(ControlsKeysEnum input, string userId)
    {
        EnemyTanks = BattleField.enemyTanksProperty;
        MyTanks = BattleField.MyTankProperty;
        _base = BattleField.MyBase;
        Projectile myTankProjectile;
        if (BattleField.MyTankProperty.Count == 1)
        {
            BattleField.MyTankProperty[0].Id = userId;
        }

        var currentTank = BattleField.MyTankProperty.FirstOrDefault(x => x.Id == userId);

        List<Projectile?> projectiles = new List<Projectile?>();
        switch (input)
        {
            case ControlsKeysEnum.UpArrow:
                currentTank.TurnAndMove(Vector.Up, BattleField);
                break;
            case ControlsKeysEnum.DownArrow:
                currentTank.TurnAndMove(Vector.Down, BattleField);
                break;
            case ControlsKeysEnum.LeftArrow:
                currentTank.TurnAndMove(Vector.Left, BattleField);
                break;
            case ControlsKeysEnum.RightArrow:
                currentTank.TurnAndMove(Vector.Right, BattleField);
                break;
            case ControlsKeysEnum.SpaceBar:
                projectiles.Add(currentTank.Fire());
                break;
            case ControlsKeysEnum.None:
                break;
        }

        foreach (var myTank in BattleField.MyTankProperty)
        {
            foreach (var enemyTank in EnemyTanks)
            {
                if (enemyTank.IsOnTheSameLine(myTank) && !enemyTank.IsDestroyed
                                                      && _random.Next(0, 3) == 1)
                {
                    Vector vectorToShoot = enemyTank.FindVectorToShoot(myTank);
                    projectiles.Add(enemyTank.FireToVector(vectorToShoot));
                }
                enemyTank.MoveEnemyTank(BattleField);
            }
        }
        
        BattleField.UpdateField(projectiles);

        return BattleField.State;
    }

    public bool IsLoser(string userId)
    {
        var currentTank = BattleField.MyTankProperty.FirstOrDefault(x => x.Id == userId);
        if (currentTank != null)
        {
            if (_base != null)
            {
                return currentTank.IsDestroyed || _base.IsDestroyed;
            }
            
            return currentTank.IsDestroyed;
        }
        
        return false;
    }

    public bool IsWinner()
    {
        if (EnemyTanks != null && EnemyTanks.Any())
        {
            return EnemyTanks.All(x => x.IsDestroyed);
        }
        
        return false;
    }
}
