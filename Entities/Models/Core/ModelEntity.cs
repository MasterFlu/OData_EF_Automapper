namespace Example.Entities.Models.Core
{
    public abstract class ModelEntity : ModelEntityCore
    {
        public int? Oid { get; set; }

        public int? SeqNum { get; set; }

        public int? SortNum { get; set; }

        public string? Number { get; set; }

        public string? Name { get; set; }

        public string? Comments { get; set; }

        public ModelEntity() : base()
        {
            //
        }
    }
}
