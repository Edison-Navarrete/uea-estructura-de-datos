using System;

namespace ColaAtraccion
{
    /// <summary>
    /// Se lanza cuando se intenta registrar una persona y ya no hay
    /// asientos disponibles (cola llena / cupo agotado).
    /// </summary>
    public class CupoAgotadoException : Exception
    {
        public CupoAgotadoException(string mensaje) : base(mensaje) { }
    }

    /// <summary>
    /// Se lanza cuando se intenta asignar un asiento y la cola de espera
    /// está vacía (no hay nadie a quien atender).
    /// </summary>
    public class ColaVaciaException : Exception
    {
        public ColaVaciaException(string mensaje) : base(mensaje) { }
    }
}
