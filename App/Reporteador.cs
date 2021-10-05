using System.Collections.Generic;
using CoreEscuela.Entidades;
using System;
using System.Linq;

namespace CoreEscuela.App
{
    public class Reporteador
    {   
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if(dicObjEsc == null) throw new ArgumentNullException(nameof(dicObjEsc));
            _diccionario = dicObjEsc;
        }

        public IEnumerable<Evaluacion> GetListaDeEvaluaciones()
        {
            if(_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }
        }

        public IEnumerable<string> GetListaDeAsignaturas()
        {
            return GetListaDeAsignaturas(out var dummy);
        }
        public IEnumerable<string> GetListaDeAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaDeEvaluaciones();
            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre ).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetListaDeEvaluacionesPorAsignatura()
        {
            var diccionario = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsig = GetListaDeAsignaturas(out var listaEvaluaciones);

            foreach(var asign in listaAsig)
            {
                var evalsAsign = from ev in listaEvaluaciones where ev.Asignatura.Nombre == asign select ev;
                diccionario.Add(asign, evalsAsign);
            }

            return diccionario;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnosPorAsignatura()
        {
            var respuesta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicEval = GetListaDeEvaluacionesPorAsignatura();

            foreach(var asignConEval in dicEval)
            {
                var promediosAlumnos = from eval in asignConEval.Value
                            group eval 
                            by new 
                            {
                                eval.Alumno.UniqueID,
                                eval.Alumno.Nombre
                            }
                            into grupoEvaluacionesAlumno
                            select new AlumnoPromedio
                            {
                                AlumnoId = grupoEvaluacionesAlumno.Key.UniqueID,//UniqueID del alumno
                                Promedio = grupoEvaluacionesAlumno.Average(ev => ev.Nota),
                                AlumnoNombre = grupoEvaluacionesAlumno.Key.Nombre
                            };

                respuesta.Add(asignConEval.Key, promediosAlumnos);
            }

            return respuesta;
        }
    }
}