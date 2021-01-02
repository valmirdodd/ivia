using System;

#nullable disable

namespace apiAgendamento.Models
{
    public partial class Agendamento
    {
        public int IdAgendamento { get; set; }
        public int IdVaga { get; set; }
        public DateTime DhInicial { get; set; }
        public DateTime DhFinal { get; set; }
        public string Obs { get; set; }
        public DateTime DhCriacao { get; set; }
        public DateTime? DhAtualizacao { get; set; }
        public int IdSituacaoAgendamento { get; set; }
        public int IdVeiculo { get; set; }

        public virtual SituacaoAgendamento IdSituacaoAgendamentoNavigation { get; set; }
        public virtual Veiculo IdVeiculoNavigation { get; set; }
    }
}
