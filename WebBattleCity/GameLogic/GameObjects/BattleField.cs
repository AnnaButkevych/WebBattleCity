using System;
using System.IO.Pipelines;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class BattleField
{
    public int Length = 10;
    public int Height = 10;
    public GameObject[,] State;
    public int Indentation = 3;
    public MyTank MyTankProperty;
    public List<EnemyTank> enemyTanksProperty = new();


    public BattleField()
    {
        State = new GameObject[Length, Height];
        InitialisePositions();
    }

    public void UpdateField(List<Projectile> projectiles)
    {

        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (State[j, i] is Projectile)
                {
                    Projectile projectile = (Projectile)State[j, i];
                    if (projectile.X < Length - 1 && projectile.X > 0 + 1 && projectile.Y < Length - 1 && projectile.Y > 0 + 1)
                        switch (projectile.Vector)
                        {
                            case Vector.Left:
                                if (State[j - 1, i] is EmptyPosition)
                                {
                                    State[j - 1, i] = new Projectile(j - 1, i, projectile.Vector);
                                }
                                else
                                {
                                    State[j - 1, i].TryDestroy();
                                }
                                break;
                            case Vector.Right:
                                if (State[j + 1, i] is EmptyPosition)
                                {
                                    State[j + 1, i] = new Projectile(j + 1, i, projectile.Vector);
                                }
                                else
                                {
                                    State[j + 1, i].TryDestroy();
                                }
                                break;
                            case Vector.Up:
                                if (State[j, i - 1] is EmptyPosition)
                                {
                                    State[j, i - 1] = new Projectile(j, i - 1, projectile.Vector);
                                }
                                else
                                {
                                    State[j, i - 1].TryDestroy();
                                }
                                break;
                            case Vector.Down:
                                if (State[j, i + 1] is EmptyPosition)
                                {
                                    State[j, i + 1] = new Projectile(j, i + 1, projectile.Vector);
                                }
                                else
                                {
                                    State[j, i + 1].TryDestroy();
                                }
                                break;
                            default: throw new Exception();
                        }
                    State[j, i] = new EmptyPosition(j, i);
                }

                if (State[j, i] is MyTank)
                {
                    MyTank mytank = (MyTank)State[j, i];
                    if (State[mytank.X, mytank.Y] is EmptyPosition)
                    {
                        State[mytank.X, mytank.Y] = State[j, i];
                        if (j != mytank.X || i != mytank.Y)
                        {
                            State[j, i] = new EmptyPosition(j, i);
                        }
                    }
                    if (State[mytank.X, mytank.Y] is not EmptyPosition && !(State[mytank.X, mytank.Y] is MyTank))
                    {
                        switch ((State[j, i] as MyTank).CurrentVector)
                        {
                            case Vector.Left:
                                State[j, i].X++;
                                break;
                            case Vector.Right:
                                State[j, i].X--;
                                break;
                            case Vector.Up:
                                State[j, i].Y++;
                                break;
                            case Vector.Down:
                                State[j, i].Y--;
                                break;
                        }
                    }

                }

            }
        }

        if (projectiles.Count > 0)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.X <= Length && projectile.X >= 0 && projectile.Y <= Length && projectile.Y >= 0)
                {
                    if (State[projectile.X, projectile.Y] is EmptyPosition)
                    {
                        State[projectile.X, projectile.Y] = projectile;
                    }
                    else
                    {
                        if (State[projectile.X, projectile.Y].TryDestroy())
                        {
                            State[projectile.X, projectile.Y] = new EmptyPosition(projectile.X, projectile.Y, State[projectile.X, projectile.Y].Icon);
                        }

                    }
                }
            }
        }
    }

    private void InitialisePositions()
    {
        string fileState = FileReader.ReadFile("GameLogic/BattleFieldMatrix.txt");
        for (int i = 0; i < fileState.Length; i = i + Indentation)
        {
            int x = (i / Indentation) % 10;
            int y = i / (Length * Indentation);


            PositionsEnum obj = (PositionsEnum)Enum.Parse(typeof(PositionsEnum), fileState[i].ToString());
            switch (obj)
            {
                case PositionsEnum.BrickWall:
                    State[x, y] = new BrickWall(x, y);
                    break;
                case PositionsEnum.StoneWall:
                    State[x, y] = new StoneWall(x, y);
                    break;
                case PositionsEnum.WoodsWall:
                    State[x, y] = new WoodsWall(x, y);
                    break;
                case PositionsEnum.Empty:
                    State[x, y] = new EmptyPosition(x, y);
                    break;
                case PositionsEnum.EnemyTank:
                    State[x, y] = new EnemyTank(x, y);
                    enemyTanksProperty.Add((EnemyTank)State[x, y]);
                    break;
                case PositionsEnum.MyTank:
                    State[x, y] = new MyTank(x, y);
                    MyTankProperty = (MyTank)State[x, y];
                    break;
            }

        }
    }
}

