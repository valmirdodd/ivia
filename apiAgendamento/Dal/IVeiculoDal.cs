using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiAgendamento.Diversos;
using apiAgendamento.Models;

namespace apiAgendamento.Dal
{
    public interface IVeiculoDal
    {
        /// <summary>
        /// Listar veículos
        /// </summary>
        /// <returns>List<Veiculo></returns>
        Task<List<Veiculo>> FindAll();
    }    
}