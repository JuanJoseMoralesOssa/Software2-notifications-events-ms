using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using QRCoder;
using ms_notificaciones.Models;


[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo_bienvenida")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreoBienvenida(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("WELCOME_SENGRID_TEMPLATE");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("WELCOME_SENGRID_TEMPLATE"));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }
    [Route("correo-recuperacion-clave")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreoRecuperacionClave(ModeloCorreo datos)
    {
        SendGridMessage msg = this.crearMensajeBase(datos);
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var client = new SendGridClient(apiKey);

        msg.SetTemplateId(Environment.GetEnvironmentVariable("WELCOME_SENDGRID_TEMPLATE_ID"));
        var response = await client.SendEmailAsync(msg);
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = "password recovery"
        });

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo sent successfully to: " + datos.correoDestino);

        }
        else
        {
            return BadRequest("Correo  sent failed: " + datos.correoDestino);
        }
    }


    [Route("enviar-correo-2fa")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreo2fa(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("TwoFA_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("TwoFA_SENDGRID_TEMPLATE_ID"));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }

    [Route("enviar-certificado")]
    [HttpPost]
    public async Task<ActionResult> EnviarCertificado(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("CERTIFICADO_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("CERTIFICADO_SENDGRID_TEMPLATE_ID"));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }

    [Route("update-evento")]
    [HttpPost]
    public async Task<ActionResult> EnviarUpdateEvento(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("UPDATEEVENT_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("UPDATEEVENT_SENDGRID_TEMPLATE_ID"));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo,
            subject = datos.asuntoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }

    [Route("enviar-QR")]
    [HttpPost]
    public async Task<ActionResult> EnviarQR(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("QR_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("QR_SENDGRID_TEMPLATE_ID"));
        Console.WriteLine(datos.nombreDestino);
        Console.WriteLine(datos.contenidoCorreo);
        // Adjuntar la imagen del código QR
        if (string.IsNullOrEmpty(datos.contenidoCorreo))
        {
            return BadRequest("El contenido del correo no puede estar vacío.");
        }
        var qrImage = GenerarQR(datos.contenidoCorreo); // Genera el QR desde una URL o texto
        msg.AddAttachment("codigoQR.png", Convert.ToBase64String(qrImage));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo,
            subject = datos.asuntoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }

    [Route("hash-validacion-usuario")]
    [HttpPost]
    public async Task<ActionResult> EnviarHashValidacionUsuario(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("HASH_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId("d-1b68b49ac95a458db871b01cb92e472b");
        msg.SetTemplateData(new
        {
            nombre = datos.nombreDestino,
            mensaje = datos.contenidoCorreo,
            asunto = datos.asuntoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }
    private SendGridMessage crearMensajeBase(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("La clave de API de SendGrid no se ha configurado correctamente.");
        }

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Environment.GetEnvironmentVariable("EMAIL_FROM"),Environment.GetEnvironmentVariable("NAME_FROM"));
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino, datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        return msg;
    }

    [Route("cambio-contrasena")]
    [HttpPost]
    public async Task<ActionResult> EnviarCambioContrasena(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
        var templateId = Environment.GetEnvironmentVariable("CAMBIOCONTRASENA_SENDGRID_TEMPLATE_ID");
        var client = new SendGridClient(apiKey);

        SendGridMessage msg = this.crearMensajeBase(datos);
        msg.SetTemplateId(Environment.GetEnvironmentVariable("CAMBIOCONTRASENA_SENDGRID_TEMPLATE_ID"));
        msg.SetTemplateData(new
        {
            name = datos.nombreDestino,
            message = datos.contenidoCorreo,
            subject = datos.asuntoCorreo
        });
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }
    // Método para generar el código QR
private byte[] GenerarQR(string contenido)
{
    using (var generator = new QRCodeGenerator())
    {
        var qrCodeData = generator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
        using (var qrCode = new PngByteQRCode(qrCodeData))
        {
            return qrCode.GetGraphic(20); // Retorna la imagen como un array de bytes
        }
    }
}
}