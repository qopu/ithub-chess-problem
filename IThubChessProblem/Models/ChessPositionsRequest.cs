using System.ComponentModel.DataAnnotations;

namespace IThubChessProblem.Models
{
    public class ChessPositionsRequest
    {
        [Required]
        public string? King { get; set; }

        [Required]
        public string? Rook { get; set; }

        [Required]
        public string? Bishop { get; set; }
    }
}