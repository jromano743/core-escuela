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
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) => Printer.Beep(100, 1000, 1);
            var engine = new EscuelaEngine();
            engine.Inicializar();
            
            Printer.WriteTitle("Bienvenidos a la escuela");
            Printer.Beep();
            ImprimirCursosEscuela(engine.Escuela);
            
            Printer.WriteTitle("Funcion de de diccionario");
            var diccionario = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(diccionario, false, false, true, false);
            
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo..");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("El programa finalizó correctamente");

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
