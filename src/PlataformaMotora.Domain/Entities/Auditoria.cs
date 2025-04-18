namespace PlataformaMotora.Domain.Entities
{
    public abstract class Auditoria
    {
        public DateTime CriadoEm { get; protected set; }
        public Guid? CriadoPor { get; protected set; }

        public DateTime? ModificadoEm { get; protected set; }
        public Guid? ModificadoPor { get; protected set; }

        public bool Excluido { get; protected set; }
        public DateTime? ExcluidoEm { get; protected set; }
        public Guid? ExcluidoPor { get; protected set; }

        protected Auditoria()
        {
            CriadoEm = DateTime.UtcNow;
        }

        public void MarcarComoExcluido(Guid usuarioId)
        {
            Excluido = true;
            ExcluidoEm = DateTime.UtcNow;
            ExcluidoPor = usuarioId;
        }

        public void MarcarComoModificado(Guid usuarioId)
        {
            ModificadoEm = DateTime.UtcNow;
            ModificadoPor = usuarioId;
        }

        public void MarcarComoCriado(Guid usuarioId)
        {
            CriadoPor = usuarioId;
        }
    }
}
