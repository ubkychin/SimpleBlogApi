using System;
using Newtonsoft.Json;

namespace SimpleBlogApi.Models
{
    public class Post
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }

    public class Comment {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("post")]
        public string PostId { get; set; }
    }

    public class NewPost {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}