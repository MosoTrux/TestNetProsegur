# Proyecto Web API REST con Arquitectura Limpia y Patrón Repository

Este proyecto es una API REST desarrollada utilizando la Arquitectura Limpia y el Patrón Repository. Está diseñado para proporcionar una estructura modular y mantenible para la gestión de datos a través de una interfaz de programación de aplicaciones (API) RESTful.

## Estructura del Proyecto

El proyecto sigue la Arquitectura Limpia, que se compone de las siguientes capas:

1. **Dominio (Core):** Contiene las entidades de negocio y las reglas de negocio.
2. **Aplicación (Application):** Implementa casos de uso y orquesta la lógica de la aplicación.
3. **Infraestructura:** Proporciona implementaciones concretas de las interfaces definidas en el dominio y contiene detalles de la infraestructura, como bases de datos y servicios externos.

Además, se ha implementado el Patrón Repository para la gestión de datos, utilizando Entity Framework como ORM (Mapeador Objeto-Relacional) para la interacción con la base de datos.

## Dependencias Principales

- [Entity Framework](https://docs.microsoft.com/en-us/ef/): Framework ORM para la interacción con bases de datos.
- [Microsoft.EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/): Proveedor de base de datos en memoria para Entity Framework, útil para pruebas.
- [Microsoft.Extensions.Caching.Memory](https://www.nuget.org/packages/Microsoft.Extensions.Caching.Memory/): Proveedor de almacenamiento en caché en memoria para mejorar el rendimiento de la aplicación.
- [JWT](https://jwt.io/): Utilizado para el inicio de sesión y la validación de credenciales mediante token.

## Características Adicionales

- **Inicio de Sesión con JWT:** La autenticación en la API se realiza mediante tokens JWT para garantizar la seguridad y la validez de las credenciales.

- **Carga de Datos Predeterminados:** El proyecto, al utilizar una base de datos en memoria, carga automáticamente datos de productos y elementos del menú (bebidas) al iniciar la aplicación. Esto facilita las pruebas y la demostración del funcionamiento del sistema.

## Configuración y Uso


