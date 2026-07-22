using System;
using System.Collections.Generic;
using System.Linq;

namespace ColaAtraccion
{
    /// <summary>
    /// Clase principal que administra la línea de espera de la atracción
    /// utilizando una Cola (Queue) para garantizar el comportamiento FIFO
    /// (First In, First Out): la primera persona en llegar es la primera
    /// en subir a la atracción / obtener asiento.
    /// </summary>
    public class GestorColaAtraccion
    {
        public const int CAPACIDAD_MAXIMA = 30;

        // Estructura de datos principal: Cola (FIFO)
        private readonly Queue<Persona> colaEspera;

        // Historial de asientos ya asignados (para reportería)
        private readonly List<Asiento> asientosAsignados;

        private int contadorId;
        private int siguienteNumeroAsiento;

        public GestorColaAtraccion()
        {
            colaEspera = new Queue<Persona>();
            asientosAsignados = new List<Asiento>();
            contadorId = 1;
            siguienteNumeroAsiento = 1;
        }

        public int PersonasEnCola => colaEspera.Count;
        public int AsientosOcupados => asientosAsignados.Count;
        public int AsientosDisponibles => CAPACIDAD_MAXIMA - AsientosOcupados - PersonasEnCola;

        /// <summary>
        /// Registra (encola) a una nueva persona en la línea de espera.
        /// </summary>
        public Persona RegistrarPersona(string nombre, string cedula)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (AsientosOcupados + PersonasEnCola >= CAPACIDAD_MAXIMA)
                throw new CupoAgotadoException(
                    $"No es posible registrar más personas: los {CAPACIDAD_MAXIMA} asientos ya están vendidos o en espera de asignación.");

            var persona = new Persona(contadorId++, nombre.Trim(), cedula.Trim());
            colaEspera.Enqueue(persona);
            return persona;
        }

        /// <summary>
        /// Atiende a la siguiente persona de la cola (FIFO) y le asigna
        /// el siguiente asiento disponible.
        /// </summary>
        public Asiento AsignarSiguienteAsiento()
        {
            if (colaEspera.Count == 0)
                throw new ColaVaciaException("No hay personas en espera para asignar un asiento.");

            Persona persona = colaEspera.Dequeue();
            var asiento = new Asiento(siguienteNumeroAsiento++, persona);
            asientosAsignados.Add(asiento);
            return asiento;
        }

        /// <summary>
        /// Reportería: devuelve la lista de personas que aún están en la
        /// cola de espera, respetando el orden FIFO, sin modificar la cola.
        /// </summary>
        public List<Persona> ConsultarColaEspera()
        {
            // ToArray/ToList sobre Queue<T> no altera la estructura original
            return colaEspera.ToList();
        }

        /// <summary>
        /// Reportería: devuelve el historial completo de asientos ya asignados.
        /// </summary>
        public List<Asiento> ConsultarAsientosAsignados()
        {
            return new List<Asiento>(asientosAsignados);
        }

        /// <summary>
        /// Busca una persona dentro de la cola de espera por nombre o cédula,
        /// sin alterar el orden ni el contenido de la cola.
        /// </summary>
        public Persona? BuscarEnCola(string criterio)
        {
            return colaEspera.FirstOrDefault(p =>
                p.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase) ||
                p.Cedula.Contains(criterio, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Devuelve un resumen del estado actual del sistema.
        /// </summary>
        public string ObtenerResumenEstado()
        {
            return
                $"Asientos totales      : {CAPACIDAD_MAXIMA}\n" +
                $"Asientos asignados    : {AsientosOcupados}\n" +
                $"Personas en espera    : {PersonasEnCola}\n" +
                $"Cupos aún disponibles : {AsientosDisponibles}";
        }
    }
}
