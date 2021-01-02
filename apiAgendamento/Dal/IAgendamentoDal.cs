using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiAgendamento.Diversos;
using apiAgendamento.Models;

namespace apiAgendamento.Dal
{
    public interface IAgendamentoDal
    {
        /// <summary>
        /// Listar agendamentos na data informada
        /// </summary>
        /// <param name="dtAgendamento">dtAgendamento</param>
        /// <returns>List<Agendamento></returns>
        Task<List<Agendamento>> FindAllByDate(DateTime dtAgendamento);

        /// <summary>
        /// Inserir uma agendamento
        /// </summary>
        /// <param name="Agendamento">Agendamento</param>
        /// <returns><Agendamento></returns>
        Task<RetAgendamento> Insert(Agendamento agendamento);
    }    
}