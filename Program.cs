using System;
using System.Collections.Generic;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (object sender, EventArgs e) => Printer.Beep(100, 1000, 1);
            var engine = new EscuelaEngine();
            engine.Inicializar();
            
            Printer.WriteTitle("Bienvenidos a la escuela");
            var reporter = new Reporteador(engine.GetDiccionarioObjetos());
            var evaList = reporter.GetListaDeEvaluaciones();
            var listaAsignaturas = reporter.GetListaDeAsignaturas();
            var listaEvalAsign = reporter.GetListaDeEvaluacionesPorAsignatura();
            var promediosPorAsignatura = reporter.GetPromedioAlumnosPorAsignatura();

            Printer.WriteTitle("Captura de una Evaluacion por consola");
            var newEval = new Evaluacion();
            string nombre, notaString;
            int nota;

            WriteLine("Ingrese el nombre de la evaluacion");
            Printer.PresioneEnter();

            nombre = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacio");
                WriteLine("Saliendo del programa...");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluacion ha sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluacion.");
            Printer.PresioneEnter();

            notaString = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(notaString))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacio");
                WriteLine("Saliendo del programa...");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notaString);
                    if(newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluacion ha sido ingresado correctamente");
                }
                catch(ArgumentOutOfRangeException arge)
                {
                    WriteLine("El valor ingresado no es un numero valido");
                    WriteLine(arge.Message);
                }
                catch(Exception)
                {
                    WriteLine("El valor ingresado no es un numero");
                }
            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo..");
            //Printer.Beep(3000, 1000, 3);
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
