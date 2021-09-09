using System;
namespace CoreEscuela.Entidades
{
    public class ObjetoEscuelaBase
    {
        public string UniqueID { get; private set; }
        public string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            UniqueID = Guid.NewGuid().ToString();
        }
    }
}