using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Models;

namespace Api.Repository
{
    public class RCliente : ICliente
    {
        private Client conn = new Client();
        public bool Delete(int id)
        {
            var item = conn.Cliente.Find(id);
            if (item == null)
            {
                return false;
            }

            conn.Cliente.Remove(item);
            conn.SaveChanges();
            return true;
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (var db = new Client())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.Cliente.ToList();
            }
        }

        public Cliente GetById(int id)
        {
            using (var db = new Client())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return conn.Cliente.Find(id);
            }
        }

        public Cliente Post(Cliente item)
        {
            if (item == null)
            {
                return null;

            }

            conn.Cliente.Add(item);
            conn.SaveChanges();
            return item;
        }

        public bool Put(Cliente cliente)
        {
            var item = conn.Cliente.Find(cliente.Id);
            if (item == null)
            {
                return false;
            }

            item.Nombre = cliente.Nombre;
            item.Apellido = cliente.Apellido;
            item.Edad = cliente.Edad;
            conn.Entry(item).State = System.Data.Entity.EntityState.Modified;
            conn.SaveChanges();

            return true;
        }
    }
}