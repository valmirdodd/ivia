using System;
using System.Collections.Generic;

#nullable disable

namespace apiAgendamento.Models
{
    public partial class SituacaoAgendamento
    {
        public SituacaoAgendamento()
        {
            Agendamentos = new HashSet<Agendamento>();
        }

        public int IdSituacaoAgendamento { get; set; }
        public string DsSituacao { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}
