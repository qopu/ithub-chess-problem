namespace IThubChessProblem.Services;

public static class ChessPositionConverterService
{
    public static int[] ConvertChessPosition(string? position)
    {
        if (string.IsNullOrEmpty(position) || position.Length != 2)
            throw new ArgumentException("Invalid chess position format.");

        var column = position[0];
        var row = position[1];

        var columnNumber = ConvertColumnToNumber(column);
        var rowNumber = int.Parse(row.ToString());

        return [columnNumber, rowNumber];
    }

    private static int ConvertColumnToNumber(char column)
    {
        return column switch
        {
            'A' => 1,
            'B' => 2,
            'C' => 3,
            'D' => 4,
            'E' => 5,
            'F' => 6,
            'G' => 7,
            'H' => 8,
            _ => throw new ArgumentException("Invalid column.")
        };
    }
}
