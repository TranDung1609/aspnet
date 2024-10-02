using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        public class SumRequest
        {
            public int A { get; set; }
            public int B { get; set; }
        }
        [HttpPost("calculate")]
        public IActionResult CalculateSum([FromBody] SumRequest request)
        {
            int sum = request.A + request.B;

            return Ok(new { result = sum });
        }



        public class NumbersRequest
        {
            public int[] Numbers { get; set; }
        }
        [HttpPost("tongdauchan")]
        public IActionResult TongDauChan([FromBody] NumbersRequest request)
        {
            int sum = request.Numbers.Where(n => IsFirstDigitEven(n)).Sum();

            return Ok(new { result = sum });
        }

        [HttpPost("tbnguyento")]
        public IActionResult TinhTrungBinhCongNguyenTo([FromBody] NumbersRequest request)
        {
            var primeNumbers = request.Numbers.Where(IsPrime).ToArray();

            if (primeNumbers.Length == 0)
            {
                return Ok(new { result = 0.0 });
            }

            double average = primeNumbers.Average();
            return Ok(new { result = average });
        }



        private bool IsFirstDigitEven(int number)
        {
            int absNumber = Math.Abs(number);

            while (absNumber >= 10)
            {
                absNumber /= 10;
            }

            return absNumber % 2 == 0;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

    }
}
