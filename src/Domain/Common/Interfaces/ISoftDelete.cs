namespace Domain.Common.Interfaces
{
    public interface ISoftDelete
    {
        public DateTime? DeletedAt {  get; set; }
        public Guid? DeletedBy {  get; set; }
        public bool Deleted { get; set; }
    }
}
