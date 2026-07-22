using System;

namespace ColaAtraccion
{
    /// <summary>
    /// Representa a una persona que ingresa a la línea de espera de la atracción.
    /// </summary>
    public class Persona
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Cedula { get; private set; }
        public DateTime HoraLlegada { get; private set; }

        public Persona(int id, string nombre, string cedula)
        {
            Id = id;
            Nombre = nombre;
            Cedula = cedula;
            HoraLlegada = DateTime.Now;
        }

        public override string ToString()
        {
            return $"#{Id,-3} | {Nombre,-25} | Cédula: {Cedula,-12} | Llegada: {HoraLlegada:HH:mm:ss}";
        }
    }
}
