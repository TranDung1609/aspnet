using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        [HttpPost("calculate")]
        public IActionResult CalculateSum([FromBody] SumRequest request)
        {
            int sum = request.A + request.B;

            return Ok(new { result = sum });
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

        [HttpPost("trungbinhcong")]
        public IActionResult TinhTrungBinhCong([FromBody] NumbersRequest request)
        {
            if (request.Numbers == null || request.Numbers.Length == 0)
            {
                return BadRequest("Danh sách số không hợp lệ.");
            }

            var positiveNumbers = request.Numbers.Where(n => n > 0).ToArray();

            if (positiveNumbers.Length == 0)
            {
                return Ok(new { result = 0.0 });
            }

            double average = positiveNumbers.Average();

            return Ok(new { result = average });
        }

        [HttpPost("tbclonhon")]
        public IActionResult TinhTrungBinhCongLonHonX([FromBody] NumbersWithXRequest request)
        {
            if (request.Numbers == null || request.Numbers.Length == 0)
            {
                return BadRequest("Danh sách số không hợp lệ.");
            }

            var numbersGreaterThanX = request.Numbers.Where(n => n > request.X).ToArray();

            if (numbersGreaterThanX.Length == 0)
            {
                return Ok(new { result = 0.0 });
            }

            double average = numbersGreaterThanX.Average();

            return Ok(new { result = average });
        }

        [HttpPost("trungbinhnhan")]
        public IActionResult TinhTrungBinhNhan([FromBody] NumbersDoubleRequest request)
        {
            // Lấy các giá trị dương
            var positiveNumbers = request.Numbers.Where(n => n > 0).ToArray();

            // Nếu không có số dương nào, trả về 0
            if (positiveNumbers.Length == 0)
            {
                return Ok(new { result = 0.0 });
            }

            // Tính tích của các số dương
            double product = positiveNumbers.Aggregate(1.0, (acc, n) => acc * n);

            // Tính trung bình nhân
            double geometricMean = Math.Pow(product, 1.0 / positiveNumbers.Length);

            return Ok(new { result = geometricMean });
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

    public class SumRequest
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class NumbersRequest
    {
        public int[] Numbers { get; set; }
    }

    public class NumbersDoubleRequest
    {
        public double[] Numbers { get; set; }
    }

    public class NumbersWithXRequest
    {
        public double[] Numbers { get; set; }
        public double X { get; set; }
    }
}
