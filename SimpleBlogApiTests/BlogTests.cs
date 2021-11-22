using System;
using Xunit;
using SimpleBlogApi.Models;


namespace SimpleBlogApiTests
{
    public class BlogTests
    {
        Post testPost;

        public BlogTests() {

        }

        [Theory]
        [InlineData("X4X8aJI1nD0S2z7lpPwaNjZB0nGPjrRW0IUabNNeeZ15tFMG4Y2axA9EsSjil4vpHBT1ju", "X4X8aJI1nD0S2z7lpPwaNjZB0nGPjrRW0IUabNNeeZ15tFMG4Y2axA9EsSjil4vpHBT1ju")]
        [InlineData("tksghm63r3QZjsDSCqaiEIAdW3tl2wzeT0fkyen5Wk2xCDhDzWMYU6ZO5bnHkcyRVbcodHS", "tksghm63r3QZjsDSCqaiEIAdW3tl2wzeT0fkyen5Wk2xCDhDzWMYU6ZO5bnHkcyRVbcodH")]        
        public void CheckLengthTest(string expected, string input)
        {
            testPost = new Post("123", input, "content", DateTime.Now);
            Assert.Equal(expected, testPost.Title);
            Assert.Equal(expected.Length, testPost.Title.Length);
        }
    }
}
