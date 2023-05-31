namespace Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int InsertedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool Deleted { get; set; }
    }
}