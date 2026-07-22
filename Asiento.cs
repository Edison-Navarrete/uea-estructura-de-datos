using System;

namespace ColaAtraccion
{
    /// <summary>
    /// Representa un asiento ya asignado a una persona dentro de la atracción.
    /// </summary>
    public class Asiento
    {
        public int NumeroAsiento { get; private set; }
        public Persona Ocupante { get; private set; }
        public DateTime HoraAsignacion { get; private set; }

        public Asiento(int numeroAsiento, Persona ocupante)
        {
            NumeroAsiento = numeroAsiento;
            Ocupante = ocupante;
            HoraAsignacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Asiento {NumeroAsiento,-3} -> {Ocupante.Nombre,-25} | Asignado: {HoraAsignacion:HH:mm:ss}";
        }
    }
}
