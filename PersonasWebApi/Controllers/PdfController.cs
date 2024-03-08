using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasWebApi.Data;
using static System.Net.Mime.MediaTypeNames;


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
                var document = new Document(PageSize.A4);
                PdfWriter.GetInstance(document,memoryStream);
                
                document.Open();
                string img = "Img/logo.png";

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(img);
                
                image.ScaleAbsolute(50f, 50f);
                              

                string html = $@"
                 <div style=""position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center;"">
                    <h1 style=""color:purple;"">Informe persona con codigo: {persona.Id}</h1>
                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <th scope=""row""><strong>Nombre:</strong></th>
                                    <td>{persona.Nombre}</td>
                                </tr>
                                <tr>
                                    <th scope=""row""><strong>Apellido:</strong></th>
                                    <td>{persona.Apellido}</td>
                                </tr>
                                <tr>
                                    <th scope=""row""><strong>Tipo Identificación:</strong></th>
                                    <td>{persona.TipoIdentificacion}</td>
                                </tr>

                                <tr>
                                    <th scope=""row""><strong>Numero de identificacion:</strong></th>
                                    <td>{persona.Identificacion}</td>
                                </tr>
                                <tr>
                                    <th scope=""row""><strong>Estado civil:</strong></th>
                                    <td>{persona.Estado}</td>
                                </tr>
                                <tr>
                                    <th scope=""row""><strong>Celular</strong></th>
                                    <td>{persona.Celular}</td>
                                </tr>

                                <tr>
                                    <th scope=""row""><strong>Fecha de Nacimiento:</strong></th>
                                    <td>{persona.FechaNacimiento.ToString("dd/MM/yyy")}</td>
                                </tr>
                                <tr>
                                    <th scope=""row""><strong>Correo:</strong></th>
                                    <td>{persona.Correo}</td>
                                </tr>

                            </tbody>
                         </table>
                    </div>

                ";


                float x = (PageSize.A4.Width - image.ScaledWidth) / 2;
                float y = PageSize.A4.Height - image.ScaledHeight;

                y -= 750;
                
                image.SetAbsolutePosition(x, y);
               
                var htmlWorker = new HTMLWorker(document);
                htmlWorker.Parse(new StringReader(html));
                document.Add(image);
                document.Close();   

                var pdfBytes = memoryStream.ToArray();

                return File(pdfBytes,"application/pdf",$"{persona.Nombre}_{persona.Apellido}.pdf");

            }
            
        }
    }
}
