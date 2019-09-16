using Api.Models;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface ICliente
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(int id);
        Cliente Post(Cliente item);
        bool Delete(int id);
        bool Put(Cliente cliente);
    }
}
