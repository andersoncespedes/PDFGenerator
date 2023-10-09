# PDFGenerator
El proyecto "Generador de Reportes desde una Base de Datos en C#" es una aplicación o biblioteca que permite generar informes y reportes a partir de datos almacenados en una base de datos utilizando el lenguaje de programación C# y otras tecnologías relacionadas. El objetivo principal es proporcionar una herramienta flexible para la generación de informes personalizados a partir de fuentes de datos.
## Características Principales
- Conexión a una variedad de bases de datos, como SQL Server, MySQL, PostgreSQL, etc.
- Consultas SQL o métodos LINQ para extraer datos de la base de datos.
- Creación de plantillas de informes personalizadas.
- Generación de informes en diferentes formatos PDF
- Personalización de estilos y diseños de informes.
- Programación de la generación de informes de manera automatizada.
- Exportación de informes a través de una interfaz de usuario o mediante comandos programáticos.
## Paquetes Utilizados
- Itext7
- BouncyCastle
## Instrucciones
Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas.
### Empezamos
- Iniciamos un proyecto c# NetCore de cuatro capas
- Agregamos, al menos, una entidad 
- Hacemos una migracion para actualizar la base de datos
- Creamos los repositorios correspondientes
- Implementamos los controladores con un endpoint llamado PDF
### Itext7
Al terminar los pasos basicos al crear nuestro proyecto procedemos a instalar el paquete Itext7, con el cual podemos generar archivos PDF en el proyecto API.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Itext7.PNG)

Tambien tendremos instalar un paquete llamado BouncyCastle que nos resolvera algunos problemas y bugs que presenta el Itext7. de igual forma lo instalaremos en API.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/BouncyCastle.PNG)

### Services

Para una Administracion mas optima del codigo se recomienda crear un servicio generico, el cual sera utilizado para generar los reportes dinamicamente independientemente sus datos. para esto creamos en la carpeta "Service" una clase llamada PDFGenerator.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Servicio.PNG)

En esta clase crearemos varios metodos, el primero LLamado "Generator", este metodo sera el que contenga la carga de memoria en bytes del archivo.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Generador.PNG)

Tambien Crearemos el metodo "Header" Donde se hara la configuracion y personalizacion de la parte superior del documento.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Header.PNG)

Al mismo tiempo creamos uno llamado Body y Footer, que como su nombre indica se encargaran de la configuracion y personalizacion de la parte central e inferior del documento respectivamente

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Body1.PNG)

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Body2.PNG)

### Controlador

Declaramos el servicio en los controladores el servicio "PDFGenerator" y la instanciamos en el constructor para usarlo en toda la clase

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Controllers1.PNG)

Desde el Controlador que creamos para generar pdf hacemos un llamado hacia el repositorio de la entidad que deseamos hacerle un reporte. en este caso usamos el metodo de paginacion para limitar la cantidad de informacion que podamos reportar.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Controllers2.png)

Creamos un pdo para mapear la lista

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Dto.PNG)

Enviamos la lista mapeada hacia el servicio y esta debe retornarnos un valor "MemoryStream" que retornaremos por el endpoint para su descarga.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/Controllers3.png)

### Endpoint
 
Creamos un nuevo request para Verificar si funciona correctamente, al clickear "send" nos deberia permitir descargar un archivo.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/response.PNG)

Descargamos el archivo, lo abrimos y podremos observar que se genero correctamente.

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/res.PNG)

![Image Text](https://github.com/andersoncespedes/PDFGenerator/blob/main/Assets/pdf.PNG)

## Autores

- [@andersoncespedes](https://www.github.com/andersoncespedes)
- [@Alejomdi193](https://github.com/Alejomdi193)

## Video
[VideoExplicativo](https://www.youtube.com/watch?v=3gq8GJcQYZU)


