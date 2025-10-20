namespace Ordering.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; protected set; }
        public string? Createby { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
