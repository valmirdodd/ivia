using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using apiAgendamento.Context;
using apiAgendamento.Diversos;
using apiAgendamento.Models;

namespace apiAgendamento.Dal.Default
{
    public class VeiculoDal : IVeiculoDal
    {
        private ApiDbContext _contexto;

        public VeiculoDal(ApiDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Veiculo>> FindAll()
        {
            List<Veiculo> veiculos = 
                await _contexto
                    .Veiculos
                    .OrderBy(reg => reg.DsVeiculo)
                    .AsNoTracking()
                    .ToListAsync();

            return veiculos;
        }
    }
}
