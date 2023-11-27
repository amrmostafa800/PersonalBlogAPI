using Microsoft.AspNetCore.Mvc;
using PersonalBlogAPI.Dtos;
using PersonalBlogAPI.Helpers;
using PersonalBlogAPI.Models;
using PersonalBlogAPI.Repositories;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace PersonalBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository _blogRepository;

        public BlogController(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddBlogDto blogDto)
        {
            if (blogDto == null) 
            {
                return BadRequest();
            }

            var photoPath = PhotoHelper.SaveNewPhoto(blogDto.Photo); //SavePhoto With Random Name
            if (photoPath is null)
            {
                return BadRequest("This File Is Not Photo");
            }

            //Save To DB
            Blog blog = new()
            {
                Content = blogDto.Content,
                Title = blogDto.Title,
                PhotoPath = photoPath
            };
            var result = await _blogRepository.Add(blog);
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateBlogDto blogDto)
        {
            string photoPath = null!;

            if (blogDto == null)
            {
                return BadRequest();
            }

            if (blogDto.Photo != null)
            {
                photoPath = PhotoHelper.SaveNewPhoto(blogDto.Photo); //SavePhoto With Random Name
                if (photoPath is null) 
                {
                    return BadRequest("This File Is Not Photo");
                }
            }

            var blog = await _blogRepository.GetById(blogDto.Id);

            if (blog is null)
            {
                return BadRequest("This Id Not Exist");
            }

            if (blogDto.Title != null)
            {
                blog.Title = blogDto.Title;
            }

            if (blogDto.Content != null)
            {
                blog.Content = blogDto.Content;
            }

            if (blogDto.Photo != null)
            {
                PhotoHelper.DeletePhoto(blog.PhotoPath); //Delete Old photo
                blog.PhotoPath = photoPath;
            }

            var result = await _blogRepository.Update(blog);
            return Ok(result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(DeleteBlogDto blogDto)
        {
            if (blogDto == null)
            {
                return BadRequest();
            }

            var result = await _blogRepository.Delete(blogDto.Id);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Blog> blogsFromDb = _blogRepository.GetAll();

            List<GetBlogDto> blogs = blogsFromDb.Select(blog => new GetBlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                PhotoPath = $"Photos/{new FileInfo(blog.PhotoPath).Name}", // Get File Name From Path using FileInfo Class
            }).ToList();

            return Ok(blogs);
        }
    }
}
