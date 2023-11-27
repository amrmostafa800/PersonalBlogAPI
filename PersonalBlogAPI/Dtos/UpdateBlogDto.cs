namespace PersonalBlogAPI.Dtos
{
    public class UpdateBlogDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
