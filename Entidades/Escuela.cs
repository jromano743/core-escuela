using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Escuela : ObjetoEscuelaBase
    {
        public int AnioDeCreacion { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TiposEscuela TipoEscuela { get; set; }

        public List<Curso> Cursos { get; set; }

        public Escuela(string nombre, int anio) => (Nombre, AnioDeCreacion) = (nombre, anio);
        public Escuela(string nombre, int anio, TiposEscuela tipo, string pais = "", string ciudad = "")
        {
            (Nombre, AnioDeCreacion) = (nombre, anio);
            this.Pais = pais;
            this.TipoEscuela = tipo;
            this.Ciudad = ciudad;
        }

        public override string ToString()
        {
            //System.Enviroment.NewLine
            return $"Nombre: {Nombre},Tipo: {TipoEscuela}\nPais: {Pais}, Ciudad: {Ciudad}";
        }
    }
}
