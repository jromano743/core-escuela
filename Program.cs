using System;
using System.Collections.Generic;
using CoreEscuela;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CorEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            
            Printer.WriteTitle("Bienvenidos a la escuela");
            Printer.Beep();
            ImprimirCursosEscuela(engine.Escuela);

            Printer.DrawLine(20);
            Printer.WriteTitle("Lista de Escuela");
            Printer.DrawLine(20);
            var listaObj = engine.GetObjetosEscuela();
        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            if(escuela?.Cursos == null) return;
            Printer.WriteTitle("Cursos de la escuela");
            foreach(var curso in escuela.Cursos){
                System.Console.WriteLine($"Curso: {curso.Nombre} || ID: {curso.UniqueID}");
            }
        }
    }
}
