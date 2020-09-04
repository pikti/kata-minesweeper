using System;
using System.Collections.Generic;
using Xunit;
using MineSweeper.Services;

namespace MineSweeper.UnitTests.Services
{
    public class MineSweeperService_GetFieldsShould
    {
        private MineSweeperService _mineSweeperService;

        public MineSweeperService_GetFieldsShould()
        {
            _mineSweeperService = new MineSweeperService();
        }

        [Fact]
        public void GetFields_InputIsNull_ReturnArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _mineSweeperService.GetFields(null));
        }

        [Fact]
        public void GetFields_InputIsNotAValidField_ReturnArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _mineSweeperService.GetFields(new List<string[]>
                {
                    new string[]{"3 4\r\n,,,"}
                }));
        }

        [Fact]
        public void GetFields_InputIsAValidField_ReturnValidSolution()
        {
            var result = _mineSweeperService.GetFields(new List<string[]>
                {
                    new string[]{
                        "4 4",
                        "*...",
                        "....",
                        ".*..",
                        "...."
                    }
                });
            Assert.True(result == "Field #1:\r\n*100\r\n2210\r\n1*10\r\n1110\r\n"
                , $"result:\r\n{result}\r\nExpected:\r\nField #1:\r\n*100\r\n2210\r\n1*10\r\n1110");
        }
    }
}
