# Patrones de Diseño y Arquitectura Limpia

Este repositorio contiene ejemplos de implementación de los patrones de diseño Mediator, Unit of Work, Repository y CQRS, siguiendo los principios de la arquitectura limpia.

## Mediator

### Descripción
El patrón Mediator se utiliza para reducir las dependencias directas entre los componentes de un sistema. En lugar de que los componentes se comuniquen directamente entre sí, utilizan un objeto mediador central para gestionar las interacciones.

### Ejemplo de Uso
En nuestro proyecto, el `Mediator` actúa como un intermediario entre los diferentes módulos del sistema, facilitando la comunicación de manera desacoplada.

## Unit of Work

### Descripción
El patrón Unit of Work se utiliza para gestionar transacciones y asegurar que un conjunto de operaciones se complete con éxito o se deshaga completamente si algo falla.

### Ejemplo de Uso
Hemos implementado una clase `UnitOfWork` que encapsula las operaciones de base de datos, permitiendo realizar cambios en la base de datos como una unidad coherente.

## Repository

### Descripción
El patrón Repository se utiliza para encapsular la lógica de acceso a datos y proporcionar una interfaz común para trabajar con datos persistentes.

### Ejemplo de Uso
Hemos creado un conjunto de `Repositories` para manejar la persistencia de entidades específicas, facilitando la separación de preocupaciones y mejorando la mantenibilidad del código.

## CQRS (Command Query Responsibility Segregation)

### Descripción
El patrón CQRS se basa en la idea de dividir las operaciones de lectura (queries) y escritura (commands) en modelos de dominio separados. Esto permite optimizar cada modelo para su tarea específica.

### Ejemplo de Uso
Hemos implementado el patrón CQRS en nuestro sistema, dividiendo las responsabilidades de lectura y escritura para mejorar el rendimiento y la escalabilidad.

## Arquitectura Limpia

### Descripción
La arquitectura limpia es un enfoque de desarrollo de software que promueve la separación de preocupaciones y la organización del código en capas independientes.

### Principios Principales
1. **Independencia de Frameworks:** Las capas internas no deben depender de detalles de implementación de capas externas o de frameworks externos.
2. **Independencia de Interfaz de Usuario:** La lógica de negocio no debe depender de la interfaz de usuario, permitiendo cambios en la IU sin afectar la lógica subyacente.
3. **Independencia de la Base de Datos:** La capa de negocio no debe depender directamente de la capa de acceso a datos, facilitando la flexibilidad en la elección de tecnologías de almacenamiento.

## Registro de Tiempo de Respuesta

### Descripción
Todos los tiempos de respuesta de las solicitudes se registran automáticamente en archivos de logs. Los archivos de logs están ubicados en la carpeta `/Log/` del proyecto.

### Ejemplo de Uso
Puedes revisar los archivos de logs en la carpeta `/Log/` para analizar el rendimiento de las solicitudes y detectar posibles problemas.


### Base de Datos SQL Server
1. Asegúrate de tener un servidor SQL Server disponible.
2. Configura la cadena de conexión en el archivo `appsettings.json` en la sección `ConnectionStrings`. Ejemplo:
   ```json
   {
     "ConnectionStrings": {
       "TestNet": "Server=tu_servidor_sql;Database=nombre_base_de_datos;User=usuario;Password=contraseña;"
     },
     // ...
   }
3. Ejecutar el script ("ScriptsSQL/[dbo].[Product].sql") para la creación de la Tabla productos:

## Ejecución del Proyecto

1. Clona el repositorio: `git clone https://github.com/MosoTrux/TestNet.git`
2. Navega al directorio del proyecto: `cd proyecto`
3. Ejecuta la aplicación: `npm start` o `yarn start`

¡Gracias por revisar nuestro proyecto! Si tienes alguna pregunta o sugerencia, no dudes en abrir un problema o ponerte en contacto con nosotros.
