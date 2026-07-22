# Sistema de Asignación de Asientos - Atracción (Cola FIFO)

Simula la línea de espera de una atracción de un parque de diversiones con
capacidad para **30 asientos**, garantizando que cada persona suba en el
orden correcto de llegada mediante una **Cola (Queue)**.

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) instalado.
- Visual Studio Code con la extensión **C# Dev Kit** (o **C#** de OmniSharp).

## Cómo ejecutar en Visual Studio Code

1. Abre esta carpeta (`ColaAtraccion`) completa en VS Code:
   `File > Open Folder...`
2. Abre una terminal integrada: `Terminal > New Terminal`.
3. Verifica que el SDK esté instalado:
   ```
   dotnet --version
   ```
4. Restaura y ejecuta el programa:
   ```
   dotnet run
   ```
5. Se mostrará un menú interactivo en consola. Navega por las opciones
   escribiendo el número correspondiente y presionando ENTER.

## Estructura del proyecto

```
ColaAtraccion/
├── ColaAtraccion.csproj      # Archivo de proyecto .NET
├── Program.cs                 # Menú principal y lógica de interacción
├── Persona.cs                 # Clase que representa a un visitante
├── Asiento.cs                 # Clase que representa un asiento asignado
├── GestorColaAtraccion.cs     # Clase principal con la Cola (Queue) y su lógica
├── Excepciones.cs             # Excepciones personalizadas del sistema
└── README.md
```

## Funcionalidades

1. **Registrar persona en la cola** (Enqueue).
2. **Asignar siguiente asiento** – atiende a la primera persona en la fila (Dequeue).
3. **Reportería** – consultar personas en espera y asientos ya asignados.
4. **Búsqueda** de una persona dentro de la cola.
5. **Estado general** del sistema (cupos disponibles, ocupados, en espera).
6. **Análisis de tiempo de ejecución** – benchmark de Enqueue/Dequeue con
   distintos tamaños de datos (10, 100, 1000, 10000 elementos), usado para
   el análisis de complejidad O(1) por operación / O(n) total requerido
   en el informe.
7. **Carga de datos de prueba** para probar rápidamente el sistema sin
   digitar manualmente cada persona.

## Capturas de pantalla (Anexos del informe)

> 📌 **ESPACIO PARA CAPTURA 1** — Menú principal del sistema en ejecución.

> 📌 **ESPACIO PARA CAPTURA 2** — Registro de varias personas en la cola de espera.

> 📌 **ESPACIO PARA CAPTURA 3** — Reporte de personas en cola (opción 3).

> 📌 **ESPACIO PARA CAPTURA 4** — Asignación de asientos y reporte de asientos
> asignados (opciones 2 y 4).

> 📌 **ESPACIO PARA CAPTURA 5** — Resultado del análisis de tiempo de ejecución
> (opción 7).

> 📌 **ESPACIO PARA CAPTURA 6** — Intento de registrar una persona con el cupo
> agotado (mensaje de error controlado).

## Agente de IA utilizado

Este proyecto fue generado con la asistencia de **Claude (Anthropic)** como
agente de IA. Complete aquí el porcentaje real de código escrito con ayuda
del agente y cualquier modificación manual que usted haya realizado, tal
como lo exige la guía de prácticas.
