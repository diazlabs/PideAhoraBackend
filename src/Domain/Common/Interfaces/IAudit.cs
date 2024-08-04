
namespace Domain.Common.Interfaces
{
    internal interface IAudit
    {
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
