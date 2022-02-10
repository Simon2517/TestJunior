namespace TestJunior.Services.Pagination
{
    public abstract class Pagination
    {
        public int PageNumber { get; private set; } = 1;
        public int PageSize { get; private set; } = 10;
        public bool asc_desc { get; set; } = false;
    }
}
