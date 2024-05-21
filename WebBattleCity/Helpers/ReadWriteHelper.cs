using WebBattleCity.GameLogic.GameObjects;

namespace WebBattleCity.Helpers;

public class ReadWriteHelper
{
    public static void WriteValuesToFile(IEnumerable<KeyValuePair<string, string>> keyValuePairs, string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        int count = 1;
        foreach (var pair in keyValuePairs)
        {
            if (count == 10)
            {
                writer.Write(pair.Value + ",\n");
                count = 0;
            }
            else
            {
                writer.Write(pair.Value + ", ");
            }
            count++;
        }
    }

    public static int[,] ReadLevelWriteValuesToFile(string levelName)
    {
        string fileState = FileReader.ReadFile($"GameLogic/{levelName}.txt");
        string[] lines = fileState.Split(new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.RemoveEmptyEntries);
        
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length-1;

        int[,] numbersArray = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].TrimEnd(',').Split(',');

            for (int j = 0; j < cols; j++)
            {
                numbersArray[i, j] = int.Parse(values[j].Trim());
            }
        }

        return numbersArray;
    }
}