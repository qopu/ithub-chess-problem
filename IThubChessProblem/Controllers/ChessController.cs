using IThubChessProblem.Models;
using IThubChessProblem.Services;
using Microsoft.AspNetCore.Mvc;

namespace IThubChessProblem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessController : ControllerBase
    {
        private readonly ChessThreatCheckerService _chessThreatCheckerService;

        public ChessController(ChessThreatCheckerService chessThreatCheckerService)
        {
            _chessThreatCheckerService = chessThreatCheckerService;
        }
        
        public class ChessResponse
        {
            public string? Text { get; set; }
            public bool Threat { get; set; }
        }

        /// <summary>
        /// Example:
        /// { 
        ///     "king": "C3",
        ///     "rook": "A1",
        ///     "bishop": "B2" 
        /// }
        /// </summary>
        [HttpPost]
        public ActionResult<ChessResponse> CheckThreat([FromBody] ChessPositionsRequest positions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid positions provided.");
            }

            try
            {
                var kingPosition = ChessPositionConverterService.ConvertChessPosition(positions.King);
                var rookPosition = ChessPositionConverterService.ConvertChessPosition(positions.Rook);
                var bishopPosition = ChessPositionConverterService.ConvertChessPosition(positions.Bishop);

                var result = _chessThreatCheckerService.CheckThreat(kingPosition, rookPosition, bishopPosition);
                
                var threat = result != "Король в безопасности.";

                return Ok(new ChessResponse { Text = result, Threat = threat });
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid argument: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }
        }
    }
}