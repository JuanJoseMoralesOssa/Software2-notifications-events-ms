# Software2-notifications-events-ms

## Descripción

Microservicio encargado de gestionar y centralizar el envío de notificaciones por correo electrónico relacionadas con una plataforma de eventos. Utiliza plantillas de SendGrid para enviar mensajes personalizados a participantes y usuarios.

## Problema que resuelve

Evita que los diferentes módulos del sistema implementen individualmente la lógica de envío de correos. Centraliza las notificaciones transaccionales y facilita la comunicación automática con los usuarios.

## Funcionalidades

- Envío de correos de bienvenida.
- Recuperación y cambio de contraseña.
- Autenticación de dos factores mediante correo electrónico.
- Envío de certificados de participación.
- Notificaciones sobre actualizaciones de eventos.
- Envío de códigos QR adjuntos en formato PNG.
- Envío de hashes para validación de usuarios.
- Uso de plantillas dinámicas de SendGrid.
- Configuración mediante variables de entorno.
- Documentación de la API con Swagger/OpenAPI.

## Tecnologías utilizadas

- .NET 8.
- ASP.NET Core Web API.
- C#.
- SendGrid para el envío de correos.
- QRCoder para generar códigos QR.
- Swagger/OpenAPI mediante Swashbuckle.
- DotNetEnv para cargar variables de entorno.

## Ejecución

Configura las variables de entorno necesarias:

```bash
SENGRID_API_KEY=tu_clave_de_sendgrid
EMAIL_FROM=correo_remitente@ejemplo.com
NAME_FROM=Nombre del remitente
WELCOME_SENGRID_TEMPLATE=identificador_de_plantilla
WELCOME_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
TwoFA_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
CERTIFICADO_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
UPDATEEVENT_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
QR_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
HASH_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
CAMBIOCONTRASENA_SENDGRID_TEMPLATE_ID=identificador_de_plantilla
