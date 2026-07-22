using System;
using System.Diagnostics;

namespace ColaAtraccion
{
    /// <summary>
    /// Sistema de asignación de 30 asientos en orden de llegada.
    /// Simula la línea de espera de una atracción de un parque de diversiones,
    /// garantizando que cada persona suba en el orden correcto (FIFO)
    /// mediante el uso de una Cola (Queue).
    /// </summary>
    internal class Program
    {
        private static readonly GestorColaAtraccion gestor = new GestorColaAtraccion();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            MostrarBienvenida();

            while (!salir)
            {
                MostrarMenu();
                string? opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarPersonaMenu();
                        break;
                    case "2":
                        AsignarAsientoMenu();
                        break;
                    case "3":
                        ReporteColaEspera();
                        break;
                    case "4":
                        ReporteAsientosAsignados();
                        break;
                    case "5":
                        BuscarPersonaMenu();
                        break;
                    case "6":
                        Console.WriteLine("===== ESTADO GENERAL DEL SISTEMA =====");
                        Console.WriteLine(gestor.ObtenerResumenEstado());
                        break;
                    case "7":
                        AnalisisTiempoEjecucion();
                        break;
                    case "8":
                        CargarDatosDePrueba();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("Cerrando el sistema. ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private static void MostrarBienvenida()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("   SISTEMA DE ASIGNACIÓN DE ASIENTOS - ATRACCIÓN (30 CUPOS)");
            Console.WriteLine("   Estructura de Datos: COLA (Queue - FIFO)");
            Console.WriteLine("==========================================================\n");
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("---------------------- MENÚ PRINCIPAL ----------------------");
            Console.WriteLine("1. Registrar persona en la cola de espera");
            Console.WriteLine("2. Asignar siguiente asiento (atender a la fila)");
            Console.WriteLine("3. Reportería: ver personas en cola de espera");
            Console.WriteLine("4. Reportería: ver asientos ya asignados");
            Console.WriteLine("5. Buscar persona dentro de la cola");
            Console.WriteLine("6. Ver estado general del sistema");
            Console.WriteLine("7. Analizar tiempo de ejecución (benchmark)");
            Console.WriteLine("8. Cargar datos de prueba (simulación automática)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("--------------------------------------------------------------");
            Console.Write("Seleccione una opción: ");
        }

        private static void RegistrarPersonaMenu()
        {
            Console.WriteLine("===== REGISTRO DE PERSONA EN LA COLA =====");
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine() ?? string.Empty;
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine() ?? string.Empty;

            try
            {
                var sw = Stopwatch.StartNew();
                Persona p = gestor.RegistrarPersona(nombre, cedula);
                sw.Stop();

                Console.WriteLine($"\n✔ Persona registrada correctamente: {p}");
                Console.WriteLine($"  (Operación Enqueue ejecutada en {sw.Elapsed.TotalMilliseconds:F4} ms)");
            }
            catch (CupoAgotadoException ex)
            {
                Console.WriteLine($"✘ Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"✘ Error: {ex.Message}");
            }
        }

        private static void AsignarAsientoMenu()
        {
            Console.WriteLine("===== ASIGNACIÓN DE SIGUIENTE ASIENTO =====");
            try
            {
                var sw = Stopwatch.StartNew();
                Asiento asiento = gestor.AsignarSiguienteAsiento();
                sw.Stop();

                Console.WriteLine($"✔ Asiento asignado: {asiento}");
                Console.WriteLine($"  (Operación Dequeue ejecutada en {sw.Elapsed.TotalMilliseconds:F4} ms)");
            }
            catch (ColaVaciaException ex)
            {
                Console.WriteLine($"✘ Error: {ex.Message}");
            }
        }

        private static void ReporteColaEspera()
        {
            Console.WriteLine("===== REPORTE: PERSONAS EN COLA DE ESPERA (orden FIFO) =====");
            var personas = gestor.ConsultarColaEspera();

            if (personas.Count == 0)
            {
                Console.WriteLine("No hay personas en espera actualmente.");
                return;
            }

            int posicion = 1;
            foreach (var p in personas)
            {
                Console.WriteLine($"{posicion,3}. {p}");
                posicion++;
            }
            Console.WriteLine($"\nTotal en espera: {personas.Count}");
        }

        private static void ReporteAsientosAsignados()
        {
            Console.WriteLine("===== REPORTE: ASIENTOS YA ASIGNADOS =====");
            var asientos = gestor.ConsultarAsientosAsignados();

            if (asientos.Count == 0)
            {
                Console.WriteLine("Aún no se ha asignado ningún asiento.");
                return;
            }

            foreach (var a in asientos)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine($"\nTotal asignados: {asientos.Count} / {GestorColaAtraccion.CAPACIDAD_MAXIMA}");
        }

        private static void BuscarPersonaMenu()
        {
            Console.WriteLine("===== BUSCAR PERSONA EN LA COLA =====");
            Console.Write("Ingrese nombre o cédula a buscar: ");
            string criterio = Console.ReadLine() ?? string.Empty;

            Persona? encontrada = gestor.BuscarEnCola(criterio);

            if (encontrada == null)
            {
                Console.WriteLine("✘ No se encontró ninguna persona en la cola con ese criterio.");
            }
            else
            {
                Console.WriteLine($"✔ Persona encontrada: {encontrada}");
            }
        }

        private static void CargarDatosDePrueba()
        {
            Console.WriteLine("===== CARGA DE DATOS DE PRUEBA =====");
            string[] nombres =
            {
                "Ana Torres", "Luis Vega", "María Pérez", "Carlos Ruiz", "Sofía León",
                "Jorge Salazar", "Diana Ríos", "Pedro Castillo", "Lucía Ortiz", "Andrés Paredes"
            };

            int agregados = 0;
            foreach (var nombre in nombres)
            {
                try
                {
                    string cedula = new Random().Next(1000000000, 1999999999).ToString();
                    gestor.RegistrarPersona(nombre, cedula);
                    agregados++;
                }
                catch (CupoAgotadoException)
                {
                    break;
                }
            }
            Console.WriteLine($"✔ Se registraron {agregados} personas de prueba en la cola de espera.");
        }

        /// <summary>
        /// Analiza el tiempo de ejecución de las operaciones fundamentales
        /// de la cola (Enqueue / Dequeue) sobre un conjunto simulado de
        /// N elementos, útil para el análisis de la estructura de datos
        /// requerido en el informe.
        /// </summary>
        private static void AnalisisTiempoEjecucion()
        {
            Console.WriteLine("===== ANÁLISIS DE TIEMPO DE EJECUCIÓN =====");
            Console.WriteLine("Se simula el llenado y vaciado de una cola independiente");
            Console.WriteLine("(no afecta la cola real del sistema) para medir el tiempo");
            Console.WriteLine("promedio de las operaciones Enqueue y Dequeue.\n");

            int[] tamanos = { 10, 100, 1000, 10000 };

            Console.WriteLine(string.Format("{0,-15}{1,-22}{2,-22}{3,-25}",
                "N elementos", "Enqueue total (ms)", "Dequeue total (ms)", "Prom. por operación (ms)"));
            Console.WriteLine(new string('-', 80));

            foreach (int n in tamanos)
            {
                var colaPrueba = new System.Collections.Generic.Queue<int>();

                var swEnqueue = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    colaPrueba.Enqueue(i);
                }
                swEnqueue.Stop();

                var swDequeue = Stopwatch.StartNew();
                for (int i = 0; i < n; i++)
                {
                    colaPrueba.Dequeue();
                }
                swDequeue.Stop();

                double totalMs = swEnqueue.Elapsed.TotalMilliseconds + swDequeue.Elapsed.TotalMilliseconds;
                double promedioPorOperacion = totalMs / (n * 2.0);

                Console.WriteLine(
                    $"{n,-15}{swEnqueue.Elapsed.TotalMilliseconds,-22:F4}{swDequeue.Elapsed.TotalMilliseconds,-22:F4}{promedioPorOperacion,-25:F6}");
            }

            Console.WriteLine("\nConclusión técnica: tanto Enqueue como Dequeue sobre una");
            Console.WriteLine("Queue<T> tienen complejidad O(1) (tiempo constante), por lo");
            Console.WriteLine("que el tiempo total crece linealmente con el número de");
            Console.WriteLine("elementos (O(n)), lo cual se evidencia en la tabla anterior.");
        }
    }
}
