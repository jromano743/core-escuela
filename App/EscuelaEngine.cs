using CoreEscuela.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {
            
        }

        public void Inicializar()
        {
            CargarCursos();

            CargarAsignaturas();

            CargarEvaluaciones();
        }

        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i+1}",
                                Nota = MathF.Round(5 * (float)rnd.NextDouble(), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase> > GetDiccionarioObjetos()
        {
            Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new [] {Escuela});
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var asignaturaTmp = new List<Asignatura>();
            var alumnoTmp = new List<Alumno>();
            var evaluacionTmp = new List<Evaluacion>();

            foreach(var curso in Escuela.Cursos)
            {
                asignaturaTmp.AddRange(curso.Asignaturas);
                alumnoTmp.AddRange(curso.Alumnos);

                foreach(var alumno in curso.Alumnos)
                {
                    evaluacionTmp.AddRange(alumno.Evaluaciones);
                }

            }
            diccionario.Add(LlaveDiccionario.Alumno, alumnoTmp.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Asignatura, asignaturaTmp.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Evaluacion, evaluacionTmp.Cast<ObjetoEscuelaBase>());

            return diccionario;
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
                    bool imprimirEvaluaciones = false,
                    bool imprimirCursos = false,
                    bool imprimirAsignaturas = false,
                    bool imprimirAlumnos = false)
        {
            foreach(var obj in dic)
            {
                Printer.WriteTitle(obj.Key.ToString());

                foreach(var value in obj.Value)
                {
                    switch (obj.Key)
                    {
                        case LlaveDiccionario.Evaluacion:
                            if(imprimirEvaluaciones) Console.WriteLine(value);
                        break;

                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + value);
                        break;

                        case LlaveDiccionario.Alumno:
                            if(imprimirAlumnos) Console.WriteLine("Alumno: " + value);
                        break;

                        case LlaveDiccionario.Curso:
                            var curTmp = value as Curso;
                            if(imprimirCursos && curTmp!= null) 
                            {
                                int count =  curTmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + value.Nombre + "Cantidad de alumnos: " + count);
                            }
                        break;

                        case LlaveDiccionario.Asignatura:
                            if(imprimirAsignaturas) Console.WriteLine(value);
                        break;

                        default:
                            Console.WriteLine(value);
                        break;
                    }
                }
            }
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            return GetObjetosEscuela(
            out conteoEvaluaciones,
            out conteoCursos,
            out conteoAsignaturas,
            out int dummy,
            traeEvaluaciones,
            traeAlumnos,
            traeAsignaturas,
            traeCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            return GetObjetosEscuela(
            out conteoEvaluaciones,
            out conteoCursos,
            out int dummy,
            out dummy,
            traeEvaluaciones,
            traeAlumnos,
            traeAsignaturas,
            traeCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            return GetObjetosEscuela(
            out conteoEvaluaciones,
            out int dummy,
            out dummy,
            out dummy,
            traeEvaluaciones,
            traeAlumnos,
            traeAsignaturas,
            traeCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            return GetObjetosEscuela(
            out int dummy,
            out dummy,
            out dummy,
            out dummy,
            traeEvaluaciones,
            traeAlumnos,
            traeAsignaturas,
            traeCursos);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            conteoEvaluaciones = conteoAsignaturas = conteoCursos = conteoAlumnos = 0;

            if(traeCursos){

                listaObj.AddRange(Escuela.Cursos);
                conteoCursos = Escuela.Cursos.Count;
                foreach(var curso in Escuela.Cursos)
                {
                    conteoAsignaturas += curso.Asignaturas.Count;
                    conteoAlumnos += curso.Alumnos.Count;

                    if(traeAsignaturas){
                        listaObj.AddRange(curso.Asignaturas);
                    }

                    if(traeAlumnos){
                        listaObj.AddRange(curso.Alumnos);
                    }

                    if(traeEvaluaciones){
                        foreach(var alumno in curso.Alumnos)
                        {
                            listaObj.AddRange(alumno.Evaluaciones);
                            conteoEvaluaciones += alumno.Evaluaciones.Count;
                        }
                    }
                }
            }
            return listaObj.AsReadOnly();
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>(){
                    new Asignatura{Nombre="Matematicas"},
                    new Asignatura{Nombre="Educacion Fisica"},
                    new Asignatura{Nombre="Castellano"},
                    new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidadAlumnos = 30)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };

            var listaAlumnos =  from n1 in nombre1
                                from n2 in nombre2
                                from a1 in nombre1
                                select new Alumno{Nombre=$"{n1} {n2} {a1}"};
            
            return listaAlumnos.OrderBy((al) => al.UniqueID).Take(cantidadAlumnos).ToList();
        }

        private void CargarCursos()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria, ciudad: "Bogota", pais: "Colombia");
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){Nombre="101", Jornada = TiposJornada.Mañana},
                        new Curso(){Nombre="201", Jornada = TiposJornada.Mañana},
                        new Curso(){Nombre="301", Jornada = TiposJornada.Mañana},
                        new Curso(){Nombre="401", Jornada = TiposJornada.Tarde},
                        new Curso(){Nombre="501", Jornada = TiposJornada.Tarde}
            };

            Random rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                int cantidad = rnd.Next(25,35);
                curso.Alumnos = GenerarAlumnosAlAzar(cantidad);
            }
        }
    }
}