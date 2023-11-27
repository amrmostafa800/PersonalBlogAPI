namespace PersonalBlogAPI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;
    }
}
