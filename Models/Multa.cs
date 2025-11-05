namespace BibliotecaAPI.Models
{
    public enum StatusMulta { PENDENTE, PAGA }

    public class Multa
    {
        public int Id { get; set; }
        public int IdEmprestimo { get; set; }
        public decimal Valor { get; set; }
        public StatusMulta Status { get; set; } = StatusMulta.PENDENTE;
    }
}
