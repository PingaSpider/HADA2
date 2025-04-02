using System;
using System.Collections.Generic;

namespace library
{
    public class ENCategory
    {
        // Atributos privados
        private int _id;
        private string _name;

        // Propiedades públicas
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Constructores
        public ENCategory()
        {
            _id = 0;
            _name = "";
        }

        public ENCategory(int id, string name)
        {
            _id = id;
            _name = name;
        }

        // Métodos
        public bool Read()
        {
            CADCategory cad = new CADCategory();
            return cad.Read(this);
        }

        public List<ENCategory> ReadAll()
        {
            CADCategory cad = new CADCategory();
            return cad.ReadAll();
        }
    }
}