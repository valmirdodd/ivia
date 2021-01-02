using System;
using System.Collections.Generic;

#nullable disable

namespace apiAgendamento.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            Agendamentos = new HashSet<Agendamento>();
        }

        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public string DsVeiculo { get; set; }
        public int IdFornecedor { get; set; }

        public virtual Fornecedor IdFornecedorNavigation { get; set; }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}
