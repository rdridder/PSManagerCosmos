namespace AspireSample.ProcessApi.Data.Interfaces
{
    public interface ITimestamped
    {
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
