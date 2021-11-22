using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogApi.Models;
using SimpleBlogApi.Services;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ICosmosDbService _cosmosDbService;

    public PostsController(ICosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
    }

    // GET api/items
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM Posts ORDER BY Posts.time desc OFFSET 0 LIMIT 7"));
    }

    // GET api/items/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _cosmosDbService.GetAsync(id));
    }

    // POST api/items
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewPost newPost)
    {
        Post post = new Post();
        post.Id = Guid.NewGuid().ToString();
        post.Title = newPost.Title;
        post.Content = newPost.Content;
        post.Time = DateTime.Now;
        await _cosmosDbService.AddAsync(post);
        return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
    }

    // // PUT api/items/5
    // [HttpPut("{id}")]
    // public async Task<IActionResult> Edit([FromBody] Post post)
    // {
    //     await _cosmosDbService.UpdateAsync(post.Id, post);
    //     return NoContent();
    // }

    // // DELETE api/items/5
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(string id)
    // {
    //     await _cosmosDbService.DeleteAsync(id);
    //     return NoContent();
    // }
}