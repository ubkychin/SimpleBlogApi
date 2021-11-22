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

        [JsonIgnore]
        public int MaxTitleLength { get; set; } = 70;

        public Post() { }
        public Post(string id, string title, string content, DateTime time)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.Time = time;
        }

        /// <summary>
        /// Checks that the title is 70 characters or less.  Truncates to 70.
        /// </summary>
        public void CheckTitleLength() {
            if (this.Title.Length > this.MaxTitleLength)
                this.Title = this.Title.Substring(0, this.MaxTitleLength); 
        }
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