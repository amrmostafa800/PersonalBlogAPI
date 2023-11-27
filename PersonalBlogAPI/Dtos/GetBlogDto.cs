namespace PersonalBlogAPI.Dtos
{
    public class GetBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoPath { get; set; }
    }
}
