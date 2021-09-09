using CoreEscuela.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreEscuela
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
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i+1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        public List<ObjetoEscuelaBase> GetObjetosEscuela()
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            listaObj.AddRange(Escuela.Cursos);
            foreach(var curso in Escuela.Cursos)
            {
                listaObj.AddRange(curso.Asignaturas);
                listaObj.AddRange(curso.Alumnos);

                foreach(var alumno in curso.Alumnos)
                {
                    listaObj.AddRange(alumno.Evaluaciones);
                }
            }
            return listaObj;
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