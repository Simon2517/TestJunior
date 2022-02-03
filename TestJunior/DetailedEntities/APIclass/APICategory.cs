namespace TestJunior.DetailedEntities
{
    public class APICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class APICategoryWithNumOfProds
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfProds { get; set; }
    }
}
