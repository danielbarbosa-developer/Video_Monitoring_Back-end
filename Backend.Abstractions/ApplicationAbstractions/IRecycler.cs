namespace Backend.Abstractions.ApplicationAbstractions
{
    public interface IRecycler
    {
        public void RecycleAllVideos(long days);
        public string Status { get; set; }
    }
}