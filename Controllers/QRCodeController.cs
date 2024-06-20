using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace QRApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QRCodeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetQrCode(string? payload)
        {
            if(string.IsNullOrEmpty(payload))
            {
                return BadRequest();
            }

            using var generator = new QRCodeGenerator();

            using var data = generator.CreateQrCode(
                payload,
                QRCodeGenerator.ECCLevel.Q
            );

            using var qrcode = new PngByteQRCode(data);

            return File(qrcode.GetGraphic(20), "image/png");
        }
    }
}
