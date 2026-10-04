using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VuelosExpress
{
    public record Vuelo(string Destino, decimal Precio);

    public record Reserva(
        string Codigo,
        string Cliente,
        string Identificacion,
        string Ruta,
        decimal Precio,
        DateTime Fecha);

    public class Program
    {
        private static readonly Dictionary<string, List<Vuelo>> grafo =
            new(StringComparer.OrdinalIgnoreCase);

        private static readonly List<Reserva> reservas = new();

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            CargarVuelos();
            MostrarMenu();
        }

        private static void CargarVuelos()
        {
            Agregar("Quito", "Guayaquil", 78.50m);
            Agregar("Quito", "Cuenca", 65.00m);
            Agregar("Quito", "Manta", 72.00m);
            Agregar("Quito", "Esmeraldas", 60.00m);

            Agregar("Guayaquil", "Quito", 82.00m);
            Agregar("Guayaquil", "Cuenca", 55.00m);
            Agregar("Guayaquil", "Loja", 74.00m);
            Agregar("Guayaquil", "Manta", 48.00m);
            Agregar("Guayaquil", "Galapagos", 180.00m);

            Agregar("Cuenca", "Quito", 68.00m);
            Agregar("Cuenca", "Guayaquil", 58.00m);
            Agregar("Cuenca", "Loja", 45.00m);
            Agregar("Cuenca", "Manta", 70.00m);

            Agregar("Loja", "Quito", 98.00m);
            Agregar("Loja", "Cuenca", 47.00m);
            Agregar("Loja", "Guayaquil", 76.00m);

            Agregar("Manta", "Quito", 75.00m);
            Agregar("Manta", "Guayaquil", 50.00m);
            Agregar("Manta", "Esmeraldas", 62.00m);
            Agregar("Manta", "Galapagos", 165.00m);

            Agregar("Esmeraldas", "Quito", 63.00m);
            Agregar("Esmeraldas", "Guayaquil", 85.00m);

            Agregar("Galapagos", "Guayaquil", 185.00m);
            Agregar("Galapagos", "Quito", 210.00m);
        }

        private static void Agregar(
            string origen,
            string destino,
            decimal precio)
        {
            if (!grafo.ContainsKey(origen))
                grafo[origen] = new List<Vuelo>();

            if (!grafo.ContainsKey(destino))
                grafo[destino] = new List<Vuelo>();

            grafo[origen].Add(new Vuelo(destino, precio));
        }

        private static void MostrarMenu()
        {
            string opcion;

            do
            {
                Console.Clear();

                int aristas = grafo.Values.Sum(lista => lista.Count);

                Console.WriteLine("================================================");
                Console.WriteLine("                VUELOS EXPRESS");
                Console.WriteLine("       SISTEMA DE VUELOS MAS ECONOMICOS");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Consultar ciudades disponibles");
                Console.WriteLine("2. Mostrar reporte general de vuelos");
                Console.WriteLine("3. Visualizar la estructura del grafo");
                Console.WriteLine("4. Buscar la ruta mas barata");
                Console.WriteLine("5. Reservar un viaje para un cliente");
                Console.WriteLine("6. Consultar reservas realizadas");
                Console.WriteLine("0. Salir");
                Console.WriteLine("================================================");

                Console.WriteLine("INFORMACION TECNICA");
                Console.WriteLine("--------------------------------");
                Console.WriteLine(
                    $"Vertices: {grafo.Count} | Aristas: {aristas}");
                Console.WriteLine(
                    "Tipo: grafo dirigido y ponderado");
                Console.WriteLine(
                    "Representacion: lista de adyacencia");
                Console.WriteLine(
                    "Algoritmo: Dijkstra");
                Console.WriteLine(
                    "Complejidad: O((V + E) log V)");

                Console.Write("\nSeleccione una opcion: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        MostrarCiudades();
                        break;

                    case "2":
                        MostrarVuelos();
                        break;

                    case "3":
                        MostrarGrafo();
                        break;

                    case "4":
                        BuscarVuelo();
                        break;

                    case "5":
                        RegistrarReserva();
                        break;

                    case "6":
                        MostrarReservas();
                        break;

                    case "0":
                        Console.WriteLine(
                            "\nPrograma finalizado correctamente.");
                        continue;

                    default:
                        Console.WriteLine(
                            "\nLa opcion seleccionada no es valida.");
                        break;
                }

                Console.WriteLine(
                    "\nPresione una tecla para volver al menu...");
                Console.ReadKey();
            }
            while (opcion != "0");
        }

        private static void MostrarCiudades()
        {
            Console.Clear();

            Console.WriteLine("CIUDADES DISPONIBLES");
            Console.WriteLine("====================");

            int numero = 1;

            foreach (string ciudad in grafo.Keys.OrderBy(c => c))
            {
                Console.WriteLine($"{numero}. {ciudad}");
                numero++;
            }

            Console.WriteLine(
                $"\nTotal de ciudades: {grafo.Count}");
        }

        private static void MostrarVuelos()
        {
            Console.Clear();

            Console.WriteLine("REPORTE GENERAL DE VUELOS");
            Console.WriteLine(
                "------------------------------------------------");

            foreach (var origen in grafo.OrderBy(c => c.Key))
            {
                foreach (Vuelo vuelo in
                         origen.Value.OrderBy(v => v.Destino))
                {
                    Console.WriteLine(
                        $"{origen.Key,-15} -> " +
                        $"{vuelo.Destino,-15} " +
                        $"${vuelo.Precio:F2}");
                }
            }

            int total = grafo.Values.Sum(lista => lista.Count);

            Console.WriteLine(
                $"\nTotal de vuelos registrados: {total}");
        }

        private static void MostrarGrafo()
        {
            Console.Clear();

            Console.WriteLine("ESTRUCTURA DEL GRAFO");
            Console.WriteLine("====================");

            foreach (var origen in grafo.OrderBy(c => c.Key))
            {
                Console.WriteLine($"\n{origen.Key}");

                foreach (Vuelo vuelo in
                         origen.Value.OrderBy(v => v.Destino))
                {
                    Console.WriteLine(
                        $"  ---> {vuelo.Destino} " +
                        $"(${vuelo.Precio:F2})");
                }
            }

            Console.WriteLine(
                "\nCiudades: vertices");
            Console.WriteLine(
                "Vuelos: aristas");
            Console.WriteLine(
                "Precios: pesos");
        }

        private static void BuscarVuelo()
        {
            Console.Clear();

            Console.WriteLine("BUSQUEDA DE LA RUTA MAS BARATA");
            Console.WriteLine("==============================");

            if (!SolicitarCiudades(
                out string origen,
                out string destino))
            {
                return;
            }

            var resultado = Dijkstra(origen, destino);
            MostrarResultado(resultado);
        }

        private static bool SolicitarCiudades(
            out string origen,
            out string destino)
        {
            Console.WriteLine(
                "Ciudades: " +
                string.Join(", ", grafo.Keys.OrderBy(c => c)));

            Console.Write("\nCiudad de origen: ");
            string origenIngresado =
                Console.ReadLine()?.Trim() ?? "";

            Console.Write("Ciudad de destino: ");
            string destinoIngresado =
                Console.ReadLine()?.Trim() ?? "";

            origen = grafo.Keys.FirstOrDefault(
                ciudad => ciudad.Equals(
                    origenIngresado,
                    StringComparison.OrdinalIgnoreCase)) ?? "";

            destino = grafo.Keys.FirstOrDefault(
                ciudad => ciudad.Equals(
                    destinoIngresado,
                    StringComparison.OrdinalIgnoreCase)) ?? "";

            if (origen == "" || destino == "")
            {
                Console.WriteLine(
                    "\nUna de las ciudades no esta registrada.");
                return false;
            }

            if (origen.Equals(
                destino,
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    "\nEl origen y el destino deben ser diferentes.");
                return false;
            }

            return true;
        }

        private static (
            bool Encontrada,
            List<string> Ruta,
            decimal Precio,
            double Tiempo) Dijkstra(
                string origen,
                string destino)
        {
            Stopwatch reloj = Stopwatch.StartNew();

            var distancias = grafo.Keys.ToDictionary(
                ciudad => ciudad,
                ciudad => decimal.MaxValue,
                StringComparer.OrdinalIgnoreCase);

            var anteriores = grafo.Keys.ToDictionary(
                ciudad => ciudad,
                ciudad => (string?)null,
                StringComparer.OrdinalIgnoreCase);

            PriorityQueue<string, decimal> cola = new();

            distancias[origen] = 0;
            cola.Enqueue(origen, 0);

            while (cola.Count > 0)
            {
                cola.TryDequeue(
                    out string? actual,
                    out decimal costo);

                if (actual == null ||
                    costo > distancias[actual])
                {
                    continue;
                }

                if (actual.Equals(
                    destino,
                    StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                foreach (Vuelo vuelo in grafo[actual])
                {
                    decimal nuevoCosto =
                        distancias[actual] + vuelo.Precio;

                    if (nuevoCosto < distancias[vuelo.Destino])
                    {
                        distancias[vuelo.Destino] = nuevoCosto;
                        anteriores[vuelo.Destino] = actual;

                        cola.Enqueue(
                            vuelo.Destino,
                            nuevoCosto);
                    }
                }
            }

            reloj.Stop();

            if (distancias[destino] == decimal.MaxValue)
            {
                return (
                    false,
                    new List<string>(),
                    0,
                    reloj.Elapsed.TotalMilliseconds);
            }

            List<string> ruta = new();

            for (string? ciudad = destino;
                 ciudad != null;
                 ciudad = anteriores[ciudad])
            {
                ruta.Add(ciudad);
            }

            ruta.Reverse();

            return (
                true,
                ruta,
                distancias[destino],
                reloj.Elapsed.TotalMilliseconds);
        }

        private static void MostrarResultado(
            (
                bool Encontrada,
                List<string> Ruta,
                decimal Precio,
                double Tiempo
            ) resultado)
        {
            Console.WriteLine("\nRESULTADO DE LA BUSQUEDA");
            Console.WriteLine("========================");

            if (!resultado.Encontrada)
            {
                Console.WriteLine(
                    "No existe una ruta disponible.");

                Console.WriteLine(
                    $"Tiempo de ejecucion: " +
                    $"{resultado.Tiempo:F4} ms");

                return;
            }

            Console.WriteLine(
                "Ruta: " +
                string.Join(" -> ", resultado.Ruta));

            Console.WriteLine(
                "Numero de escalas: " +
                Math.Max(0, resultado.Ruta.Count - 2));

            Console.WriteLine(
                $"Precio total: ${resultado.Precio:F2}");

            Console.WriteLine(
                $"Tiempo de ejecucion: " +
                $"{resultado.Tiempo:F4} ms");
        }

        private static void RegistrarReserva()
        {
            Console.Clear();

            Console.WriteLine("RESERVAR VIAJE");
            Console.WriteLine("==============");

            Console.Write("Nombre del cliente: ");
            string cliente =
                Console.ReadLine()?.Trim() ?? "";

            Console.Write("Numero de identificacion: ");
            string identificacion =
                Console.ReadLine()?.Trim() ?? "";

            if (cliente == "" || identificacion == "")
            {
                Console.WriteLine(
                    "\nLos datos del cliente son obligatorios.");
                return;
            }

            if (!SolicitarCiudades(
                out string origen,
                out string destino))
            {
                return;
            }

            var resultado = Dijkstra(origen, destino);

            MostrarResultado(resultado);

            if (!resultado.Encontrada)
            {
                return;
            }

            Console.Write("\nConfirmar reserva (S/N): ");

            string respuesta =
                Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (respuesta != "S")
            {
                Console.WriteLine("\nReserva cancelada.");
                return;
            }

            string codigo =
                "VE-" + (reservas.Count + 1).ToString("D4");

            Reserva reserva = new(
                codigo,
                cliente,
                identificacion,
                string.Join(" -> ", resultado.Ruta),
                resultado.Precio,
                DateTime.Now);

            reservas.Add(reserva);

            Console.WriteLine(
                "\nRESERVA REGISTRADA CORRECTAMENTE");
            Console.WriteLine(
                "Codigo: " + codigo);
            Console.WriteLine(
                "Cliente: " + cliente);
            Console.WriteLine(
                "Ruta: " + reserva.Ruta);
            Console.WriteLine(
                $"Precio: ${reserva.Precio:F2}");
            Console.WriteLine(
                "Estado: Confirmada");
        }

        private static void MostrarReservas()
        {
            Console.Clear();

            Console.WriteLine("RESERVAS REALIZADAS");
            Console.WriteLine("===================");

            if (reservas.Count == 0)
            {
                Console.WriteLine(
                    "\nNo existen reservas registradas.");
                return;
            }

            foreach (Reserva reserva in reservas)
            {
                Console.WriteLine(
                    $"\nCodigo: {reserva.Codigo}");

                Console.WriteLine(
                    $"Cliente: {reserva.Cliente}");

                Console.WriteLine(
                    $"Identificacion: {reserva.Identificacion}");

                Console.WriteLine(
                    $"Ruta: {reserva.Ruta}");

                Console.WriteLine(
                    $"Precio: ${reserva.Precio:F2}");

                Console.WriteLine(
                    $"Fecha: {reserva.Fecha:dd/MM/yyyy HH:mm}");

                Console.WriteLine(
                    "Estado: Confirmada");

                Console.WriteLine(
                    "--------------------------------");
            }

            Console.WriteLine(
                $"\nTotal de reservas: {reservas.Count}");
        }
    }
}