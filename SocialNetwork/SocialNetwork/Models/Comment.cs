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
    
    public partial class Comment
    {
        public int commentId { get; set; }
        public Nullable<int> postId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> imageId { get; set; }
        public string text { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
