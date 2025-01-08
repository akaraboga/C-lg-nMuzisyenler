using CılgınMuzisyenler.Models;
using Microsoft.AspNetCore.Mvc;

namespace CılgınMuzisyenler.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MuzisyenController : ControllerBase
    {

        private static List<MuzisyenModel> models = new List<MuzisyenModel>()
        {
            new MuzisyenModel{Id=1,Ad="Mehmet Tombalacı",Meslek="Üflemeli Alet Çalar",Ozellik="Ağzıyla Nağme Yapar"},
            new MuzisyenModel{Id=2,Ad="Hande Armoni",Meslek="Armoni Ustası",Ozellik="Yaratıcılık"},
            new MuzisyenModel{Id=3,Ad="Cemil Akor",Meslek="Akorist",Ozellik="Akorları sık değiştirir"}
        };



        [HttpGet]
        public ActionResult<IEnumerable<MuzisyenModel>> Get()
        {
            return Ok(models);
        }



        [HttpGet("{id}")]//İd ye gore listelenecek
        public ActionResult<MuzisyenModel> Get(int id)
        {
            var MuzisyenId = models.FirstOrDefault(m => m.Id == id);
            if (MuzisyenId == null)//Eğer verimiz yoksa 404 donderiyoruz
            {
                return NotFound();
            }
            return Ok(MuzisyenId);//İd ye gore listelenmesini sağlıyoruz.
        }


        [HttpPost]
        public ActionResult<MuzisyenModel> Create([FromBody] MuzisyenModel yeniMuzisyen)
        {
            if (yeniMuzisyen == null)
            {
                return NotFound(); // Eğer gelen veri eksikse 404 döndür
            }

            yeniMuzisyen.Id = models.Max(m => m.Id) + 1; // ID'yi otomatik arttır
            models.Add(yeniMuzisyen); // Yeni müzisyen ekle
            return CreatedAtAction(nameof(Get), new { id = yeniMuzisyen.Id }, yeniMuzisyen); // 201 Created döndür ve müzisyen ile birlikte yeni ID'yi döndür
        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] MuzisyenModel updatedMuzisyen)
        {
            var muzisyen = models.FirstOrDefault(m => m.Id == id); // ID'ye göre bul
            if (muzisyen == null)
            {
                return NotFound(); // Eğer müzisyen bulunamazsa 404 döndür
            }

            // Veriyi güncelle
            muzisyen.Ad = updatedMuzisyen.Ad;
            muzisyen.Meslek = updatedMuzisyen.Meslek;
            muzisyen.Ozellik = updatedMuzisyen.Ozellik;

            return NoContent(); // Güncelleme başarılı, 204 döndür
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var SilinecekDeğer = models.FirstOrDefault(m => m.Id == id);
            if (SilinecekDeğer == null)
            {
                return NotFound();
            }
            models.Remove(SilinecekDeğer);
            return NoContent();
        }

    }
}
