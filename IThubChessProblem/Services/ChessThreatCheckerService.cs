namespace IThubChessProblem.Services
{
    public class ChessThreatCheckerService
    {
        private const int MinPosition = 1;
        private const int MaxPosition = 8;

        public string CheckThreat(int[] kingPos, int[] rookPos, int[] bishopPos)
        {
            if (!ArePositionsValid(kingPos, rookPos, bishopPos))
            {
                return "Invalid positions!";
            }

            var isRookThreat = IsRookThreat(kingPos, rookPos, bishopPos);
            var isBishopThreat = IsBishopThreat(kingPos, bishopPos, rookPos);

            if (isRookThreat) return "Угроза королю от ладьи!";
            if (isBishopThreat) return "Угроза королю от слона!";
            
            return "Король в безопасности.";
        }

        private bool ArePositionsValid(params int[][] positions)
        {
            return positions.All(pos => pos.All(p => p >= MinPosition && p <= MaxPosition));
        }

        private bool IsRookThreat(int[] kingPos, int[] rookPos, int[] blockerPos)
        {
            // Проверяем, находятся ли король и ладья на одной горизонтали
            var sameRow = kingPos[0] == rookPos[0];
            // Проверяем, находится ли блокирующая фигура на этой же горизонтали и между королем и ладьей
            if (sameRow && !(blockerPos[0] == kingPos[0] && IsBetween(kingPos[1], rookPos[1], blockerPos[1])))
            {
                return true;
            }

            // Проверяем, находятся ли король и ладья на одной вертикали
            var sameColumn = kingPos[1] == rookPos[1];
            // Проверяем, находится ли блокирующая фигура на этой же вертикали и между королем и ладьей
            if (sameColumn && !(blockerPos[1] == kingPos[1] && IsBetween(kingPos[0], rookPos[0], blockerPos[0])))
            {
                return true;
            }

            // Если не на одной линии либо есть блокирующая фигура между ними, то угрозы нет
            return false;
        }

        private bool IsBishopThreat(int[] kingPos, int[] bishopPos, int[] blockerPos)
        {
            if (!IsDiagonal(kingPos, bishopPos))
            {
                return false; // Угрозы от слона нет, так как они не находятся на одной диагонали
            }

            if (!IsDiagonal(blockerPos, bishopPos))
            { 
                return true; // Слон может угрожать королю, так как между ними нет блокирующей фигуры
            }

            var isBlockerInMiddleVertically = IsBetween(kingPos[0], bishopPos[0], blockerPos[0]);
            var isBlockerInMiddleHorizontally = IsBetween(kingPos[1], bishopPos[1], blockerPos[1]);

            if (isBlockerInMiddleVertically && isBlockerInMiddleHorizontally)
            {
                return false; // Блокирующая фигура защищает короля
            }

            return true; // Король под угрозой
        }

        private bool IsBetween(int kingCoord, int threatCoord, int blockerCoord)
        {
            return blockerCoord > Math.Min(kingCoord, threatCoord) && blockerCoord < Math.Max(kingCoord, threatCoord);
        }

        private bool IsDiagonal(int[] pos1, int[] pos2)
        {
            return Math.Abs(pos1[0] - pos2[0]) == Math.Abs(pos1[1] - pos2[1]);
        }
    }
}