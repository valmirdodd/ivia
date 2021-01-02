using System;
using System.Collections.Generic;

#nullable disable

namespace apiAgendamento.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            Veiculos = new HashSet<Veiculo>();
        }

        public int IdFornecedor { get; set; }
        public string NmFornecedor { get; set; }

        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
