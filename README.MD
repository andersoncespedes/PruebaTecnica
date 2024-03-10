# Viaje Por Colombia API &#x1F680;
Esta API permite buscar una ruta perfecta para un viaje desde un destino a otro, utilizando una API externa. Además, guarda la información del viaje en la base de datos para consultas futuras.
## Requisitos &#x1F4BB;
- .NET 7 SDK
- SQL Server Version 10.0.22631
## Dependencias &#x1F4E6;
- Microsoft.EntityFrameworkCore Version 7.0.16
- Microsoft.AspNetCore.Mvc.NewtonsoftJson Version 7.0.16
- Microsoft.EntityFrameworkCore.Design Version 7.0.16
- AutoMapper
- AspNetCoreRateLimit
## Configuración &#x1F527;
- Clonar el repositorio: git clone https://github.com/andersoncespedes/PruebaTecnica.git
- Abrir el proyecto en Visual Studio o mediante la línea de comandos
## Configuración de la Base de Datos &#x1F4BB; &#x1F527;
- Crear una base de datos en SQL Server.
- Actualizar la cadena de conexión en appsettings.json con los detalles de la base de datos creada
```
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NombreBaseDatos;User=Usuario;Password=Contraseña;"
}
```
- Ejecutar las migraciones de Entity Framework para crear las tablas necesarias en la base de datos
```
dotnet ef database update
```
## Ejecución &#x1F4DD;
- Compilar el proyecto
- Ejecutar el proyecto desde Visual Studio o mediante la línea de comandos
```
dotnet run
```
La API estará disponible en https://localhost:5038
## Uso

a continuacion se hara una lista de los endpoints, sus metodos y body:
### Obtener una lista de los diferentes vuelos obtenidos por api.

#### Metodo: GET
#### Endpoint
```
http://localhost:5038/api/flight/
```
#### Body
```
null
```
***
### Obtener una ruta para un viaje usando la api ( Importante )
#### Metodo: GET
#### Endpoint
```
http://localhost:5038/api/Journey/
```
#### Body
```
{
  "Origin":"Punto de partida del viaje",
  "Destination":"Ultimo destino del viaje"
}
```
## Pruebas Unitarias &#x1F4CB;
Las pruebas unitarias son fundamentales en nuestro proyecto para garantizar la calidad del código y la estabilidad de las funcionalidades. Utilizamos XUnit como framework de pruebas unitarias.

### Ejecución de las Pruebas
Para ejecutar las pruebas unitarias, sigue estos pasos:

- Abre la solución en Visual Studio.
- En el Explorador de Soluciones, haz clic derecho en el proyecto de pruebas y selecciona "Ejecutar pruebas".

También puedes ejecutar las pruebas desde la línea de comandos con el comando dotnet test.

- Organización de las Pruebas
Las pruebas unitarias están ubicadas en el proyecto "TestApi" dentro de la misma solución que el proyecto principal.

### Cobertura de Código
Actualmente, nuestras pruebas unitarias cubren aproximadamente el 80% del código. Puedes verificar la cobertura de código utilizando la herramienta integrada en Visual Studio o ejecutando un informe de cobertura de código con herramientas como ReportGenerator.
### Ejemplos
Aquí tienes un ejemplo simple de una prueba unitaria en C# utilizando NUnit:
```
// Prueba para verificar si la informacion responde correctamente
    public async void GetData()
    {
        //arrange 
        JourneyBodyDto journey = new();
        journey.Destination = "bta";
        journey.Origin = "bga";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.NotNull(result.Value);
        Assert.IsType<JourneyDto>(result.Value);
        Assert.IsAssignableFrom<JourneyDto>(result.Value);
    }
```
## Autor
- Anderson Cespedes

