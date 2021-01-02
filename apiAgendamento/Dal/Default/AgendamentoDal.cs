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
    public class AgendamentoDal : IAgendamentoDal
    {
        private ApiDbContext _contexto;

        public AgendamentoDal(ApiDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Agendamento>>
        FindAllByDate(DateTime dtAgendamento)
        {
            //Uma vez que o agendamento só é possível entre 08:00 e 18:00,
            //pode-se testar a existência em uma determinada data
            //tanto pelo campo DhInicial cuanto pelo campo DhFinal
            //Foi escolhido arbitrariamente o campo DtInicial
            DateTime dtIni = dtAgendamento.Date;
            DateTime dtFin = dtIni.AddDays(1).AddMinutes(-1);

            List<Agendamento> agendamentos =
                await _contexto
                    .Agendamentos
                    .Where(reg =>
                        reg.DhInicial >= dtIni && reg.DhInicial <= dtFin)
                    .AsNoTracking()
                    .ToListAsync();

            return agendamentos;
        }

        public async Task<RetAgendamento> Insert(Agendamento agendamento)
        {
            RetAgendamento retorno =
                new RetAgendamento()
                {
                    IdAgendamento = agendamento.IdAgendamento,
                    IdVaga = agendamento.IdVaga,
                    DhInicial = agendamento.DhInicial,
                    DhFinal = agendamento.DhFinal,
                    Obs = agendamento.Obs,
                    DhCriacao = agendamento.DhCriacao,
                    DhAtualizacao = agendamento.DhAtualizacao,
                    IdSituacaoAgendamento = agendamento.IdSituacaoAgendamento,
                    IdVeiculo = agendamento.IdVeiculo,
                    Mensagem = string.Empty
                };

            try
            {
                if (agendamento != null)
                {
                    //Separação das datas e horas para facilitar a aplicação das regras de negócio
                    DateTime dhInicial = agendamento.DhInicial.Date;
                    DateTime dhFinal = agendamento.DhFinal.Date;

                    DateTime hrInicial =
                        DateTime
                            .Parse(agendamento.DhInicial.ToString("HH:mm:ss"));
                    DateTime hrFinal =
                        DateTime
                            .Parse(agendamento.DhFinal.ToString("HH:mm:ss"));

                    if (dhInicial != dhFinal)
                    {
                        retorno.Mensagem =
                            "Datas inicial e final devem ser iguais";
                    }
                    else
                    {
                        if (hrFinal <= hrInicial)
                        {
                            retorno.Mensagem =
                                "Hora inicial deve ser anterior à hora final";
                        }
                        else
                        {
                            Console.WriteLine("hora do expediente");
                            if (
                                hrInicial.Hour < 8 ||
                                (hrInicial.Hour > 12 && hrInicial.Hour < 13) ||
                                hrInicial.Hour > 18 ||
                                hrFinal.Hour < 8 ||
                                (hrFinal.Hour > 12 && hrFinal.Hour < 13) ||
                                hrFinal.Hour > 18
                            )
                            {
                                retorno.Mensagem =
                                    "Agendamento fora do horário de atendimento";
                            }
                            else
                            {
                                //Verificar se há agendamento no período, levando em consideração o delay de 30 minutos entre agendamentos
                                int vagasPreenchidasNoHorario =
                                    this
                                        .VagasPreenchidasNoHorario(agendamento)
                                        .Result;

                                if (vagasPreenchidasNoHorario >= 3)
                                {
                                    retorno.Mensagem =
                                        "Não há vagas disponíveis para o agendamento solicitado";
                                }
                                else
                                {
                                    agendamento.IdVaga =
                                        vagasPreenchidasNoHorario + 1;
                                    _contexto.Update (agendamento);
                                    await _contexto.SaveChangesAsync();
                                    retorno.Mensagem =
                                        "Agendamento salvo com sucesso";
                                    retorno.IdAgendamento =
                                        agendamento.IdAgendamento;
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine (ex);
                throw ex;
            }

            return retorno;
        }

        // private async Task<List<Agendamento>>
        // FindAllByAgendamento(Agendamento agendamento)
        // {
        //     List<Agendamento> agendamentos =
        //         await _contexto
        //             .Agendamentos
        //             .Where(reg =>
        //                 reg.DhInicial >= agendamento.DhInicial &&
        //                 reg.DhInicial <= agendamento.DhFinal)
        //             .AsNoTracking()
        //             .ToListAsync();
        //     return agendamentos;
        // }

        private async Task<int>
        VagasPreenchidasNoHorario(Agendamento agendamento)
        {
            int idVeiculo = agendamento.IdVeiculo;

            Veiculo veiculo = await _contexto.Veiculos.FindAsync(idVeiculo);
            int idFornecedor = veiculo.IdFornecedor;

            //Como não foi explicitado uma regra expecífica para o número da vaga,
            //Estou considerando que o Id da vaga será gerado automaticamente pela api
            List<Agendamento> agendamentos =
                await _contexto
                    .Agendamentos
                    .Where(reg => //Intervalo de 30 minutos antes e depois dos agendamentos
                        reg.DhFinal > agendamento.DhInicial.AddHours(-0.5) &&
                        reg.DhInicial < agendamento.DhFinal.AddHours(0.5))
                    .Include(reg => reg.IdVeiculoNavigation)
                    .ThenInclude(v => v.IdFornecedorNavigation)
                    .Where(reg =>
                        reg.IdVeiculoNavigation.IdFornecedor == idFornecedor)
                    .ToListAsync();

            return agendamentos.Count();
        }
    }
}
