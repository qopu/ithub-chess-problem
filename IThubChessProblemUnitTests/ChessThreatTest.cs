using IThubChessProblem.Controllers;
using IThubChessProblem.Models;
using IThubChessProblem.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace IThubChessProblemUnitTests
{
    [TestFixture]
    public class ChessControllerTests
    {
        private ChessController _chessController;
        private Mock<ChessThreatCheckerService> _mockChessThreatCheckerService;

        [SetUp]
        public void Setup()
        {
            _mockChessThreatCheckerService = new Mock<ChessThreatCheckerService>();
            _chessController = new ChessController(_mockChessThreatCheckerService.Object);
        }

        private ChessController.ChessResponse? DeserializeResponse(ActionResult<ChessController.ChessResponse> response)
        {
            var okResult = response.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            var jsonResponse = JsonConvert.SerializeObject(okResult?.Value);
            return JsonConvert.DeserializeObject<ChessController.ChessResponse>(jsonResponse);
        }

        [Test]
        public void Test1()
        {
            // Прямая угроза от ладьи без блокирующих фигур
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "D2",
                Bishop = "H3"
            };

            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);

            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от ладьи!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test2()
        {
            // Прямая угроза от ладьи с фигурой на нерелевантной позиции
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "D2",
                Bishop = "H1"
            };

            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);

            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от ладьи!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }

        [Test]
        public void Test3()
        {
            // Угроза от слона без блокирующих фигур
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "A8",
                Bishop = "G1"
            };

            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);

            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test4()
        {
            // Ладья блокирована слоном
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "D1",
                Bishop = "D3"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test5()
        {
            // Слон блокирован ладьей
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "F3",
                Bishop = "B1"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test6()
        {
            // Нет прямой линии между королем и угрожающими фигурами
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "E3",
                Bishop = "F2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test7()
        {
            // Прямая угроза от слона без блокирующих фигур
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "H8",
                Bishop = "A2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test8()
        {
            // Слон блокирован ладьей на диагонали
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "B3",
                Bishop = "A2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test9()
        {
            // Угроза от слона с фигурой на нерелевантной позиции
            var request = new ChessPositionsRequest
            {
                King = "D5",
                Rook = "H1",
                Bishop = "B3"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test10()
        {
            // Прямая угроза от слона без блокирующих фигур
            var request = new ChessPositionsRequest
            {
                King = "C4",
                Rook = "H7",
                Bishop = "F1"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }

        [Test]
        public void Test11()
        {
            // Прямая угроза от ладьи без блокирующих фигур
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "D2",
                Bishop = "H3"
            };

            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);

            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от ладьи!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test12()
        {
            // Прямая угроза от ладьи с фигурой на нерелевантной позиции
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "D2",
                Bishop = "H1"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от ладьи!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test13()
        {
            // Ладья блокирована слоном
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "D1",
                Bishop = "D3"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test14()
        {
            // Слон блокирован ладьей
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "F3",
                Bishop = "B1"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test15()
        {
            // Нет прямой линии между королем и угрожающими фигурами
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "E3",
                Bishop = "F2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test16()
        {
            // Прямая угроза от слона без блокирующих фигур
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "H8",
                Bishop = "A2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test17()
        {
            // Слон блокирован ладьей на диагонали
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "B3",
                Bishop = "A2"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Король в безопасности."));
            Assert.That(chessResponse?.Threat, Is.False);
        }
        
        [Test]
        public void Test18()
        {
            // Угроза от слона с фигурой на нерелевантной позиции
            var request = new ChessPositionsRequest()
            {
                King = "D5",
                Rook = "H1",
                Bishop = "B3"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }
        
        [Test]
        public void Test19()
        {
            // Прямая угроза от слона без блокирующих фигур
            var request = new ChessPositionsRequest()
            {
                King = "C4",
                Rook = "H7",
                Bishop = "F1"
            };
        
            var response = _chessController.CheckThreat(request);
            var chessResponse = DeserializeResponse(response);
        
            Assert.That(chessResponse?.Text, Is.EqualTo("Угроза королю от слона!"));
            Assert.That(chessResponse?.Threat, Is.True);
        }


    }
}
