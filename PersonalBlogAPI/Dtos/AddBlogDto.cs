namespace PersonalBlogAPI.Dtos
{
    public class AddBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Photo { get; set; }
    }
}
