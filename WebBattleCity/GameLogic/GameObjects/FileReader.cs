using System;
namespace WebBattleCity.GameLogic.GameObjects;

public static class FileReader
{
    public static string ReadFile(string filename)
    {
        string filePath = "" + filename;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string content = reader.ReadToEnd();

            return content;
        }
    }
}