using System;
using System.Linq;
using System.Text;

namespace CalculatorProgrammerMode
{
    public static class NumberConverter
    {
        public static string ToHex(string _number)
        {
            if (!string.IsNullOrWhiteSpace(_number) && _number.ToArray().All(_ch => char.IsDigit(_ch)))
            {
                if (int.TryParse(_number, out int numberInt))
                {
                    var hexNumStr = numberInt.ToString("X4");

                    return NumberConverter.GroupByElementsNumber(hexNumStr, 4, '0');
                }
            }

            return _number;
        }

        public static string ToBin(string _number)
        {
            if (!string.IsNullOrWhiteSpace(_number) && _number.ToArray().All(_ch => char.IsDigit(_ch)))
            {
                if (int.TryParse(_number, out int numberDouble))
                {
                    var binNumStr = Convert.ToString(numberDouble, 2);

                    return NumberConverter.GroupByElementsNumber(binNumStr, 4, '0');
                }
            }

            return _number;
        }

        private static string GroupByElementsNumber(string _input, int _elementsInGroup, char _filler = '0')
        {
            int countToFill = _elementsInGroup - (_input.Length % _elementsInGroup);
            var numGroupedStr = new StringBuilder();
            numGroupedStr.Append(_filler, countToFill);

            int i = countToFill;
            foreach (var binNum in _input)
            {
                if (i == _elementsInGroup)
                {
                    i = 0;
                    numGroupedStr.Append(' ');
                }

                numGroupedStr.Append(binNum);
                i++;
            }

            return numGroupedStr.ToString();
        }
    }
}
