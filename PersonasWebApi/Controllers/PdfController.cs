using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasWebApi.Data;


namespace PersonasWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PersonasContext _context;

    public PdfController(PersonasContext context) { 
            _context = context;
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GenerarPdf(int id)
        {
            var persona = await _context.Persona.FirstOrDefaultAsync(e => e.Id == id);

            if (persona == null) 
            { 
                return NotFound("No se encontro información para mostrar");
            }

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document,memoryStream);

                document.Open();


                BaseColor colorrojo = new BaseColor(255,0,0);
                Font fontrojo = FontFactory.GetFont(FontFactory.TIMES_ROMAN,16,Font.BOLD, colorrojo);
                document.Add(new Paragraph($"Informe de registro nro: {persona.Id}",fontrojo));
                document.Add(new Paragraph("\n\n"));
                document.Add(new Paragraph($"Nombre: {persona.Nombre}"));
                document.Add(new Paragraph($"Apellido: {persona.Apellido}"));
                document.Add(new Paragraph($"Tipo de Identificacion: {persona.TipoIdentificacion}"));
                document.Add(new Paragraph($"Identificacion: {persona.Identificacion}"));
                document.Add(new Paragraph($"Correo: {persona.Correo}"));
                document.Add(new Paragraph($"Celular: {persona.Celular}"));
                document.Add(new Paragraph($"Estado Civil: {persona.Estado}"));
                document.Add(new Paragraph($"Fecha de nacimiento: {persona.FechaNacimiento.ToString("dd/MM/yyy")}"));


                document.Close();   

                var pdfBytes = memoryStream.ToArray();

                return File(pdfBytes,"application/pdf",$"{persona.Nombre}_{persona.Apellido}.pdf");

            }
            
        }
    }
}
