using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using WebBattleCity.GameLogic;
using WebBattleCity.GameLogic.GameLogicEnums;
using WebBattleCity.GameLogic.GameObjects;
using WebBattleCity.Models;

namespace WebBattleCity.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GameProcess _gameProcess;

    public HomeController(ILogger<HomeController> logger, GameProcess gameProcess)
    {
        _logger = logger;
        _gameProcess = gameProcess;
    }

    public IActionResult Index(int keyCode)
    {
        GameObject[,] gameObjects = null;

        switch (keyCode)
        {
            case 37: // Left arrow
                gameObjects = _gameProcess.Process(ControlsKeysEnum.LeftArrow);
                break;
            case 38: // Up arrow
                gameObjects = _gameProcess.Process(ControlsKeysEnum.UpArrow);
                break;
            case 39: // Right arrow
                gameObjects = _gameProcess.Process(ControlsKeysEnum.RightArrow);
                break;
            case 40: // Down arrow
                gameObjects = _gameProcess.Process(ControlsKeysEnum.DownArrow);
                break;
            case 32: // Spacebar
                gameObjects = _gameProcess.Process(ControlsKeysEnum.SpaceBar);
                break;
            default:
                gameObjects = _gameProcess.GetCurrentState();
               // gameObjects = _gameProcess.Process(ControlsKeysEnum.None);
                break;
        }

        GameBoardViewModel gameBoardViewModel = new GameBoardViewModel();
        int rows = gameObjects.GetLength(0);
        int cols = gameObjects.GetLength(1);

        gameBoardViewModel.Matrix = new string[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                gameBoardViewModel.Matrix[j, i] = gameObjects[i, j].GetIconName();
            }
        }

        return View(gameBoardViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

