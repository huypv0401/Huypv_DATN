//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image
    {
        public Image()
        {
            this.Comments = new HashSet<Comment>();
            this.Posts = new HashSet<Post>();
        }
    
        public int imageId { get; set; }
        public string link { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}