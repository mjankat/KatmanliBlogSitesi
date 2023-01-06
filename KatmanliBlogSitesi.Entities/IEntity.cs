namespace KatmanliBlogSitesi.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }

        DateTime CreateDate { get; set; }
    }
}
