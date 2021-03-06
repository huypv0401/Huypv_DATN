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
    
    public partial class User
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Posts = new HashSet<Post>();
        }
    
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string nickName { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public Nullable<bool> sex { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public Nullable<System.DateTime> regDate { get; set; }
        public Nullable<bool> rememberMe { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> favorite_UserId { get; set; }
        public Nullable<int> addrId { get; set; }
        public Nullable<int> relationshipId { get; set; }
    
        public virtual Addr Addr { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Favorite_User Favorite_User { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Relationship Relationship { get; set; }
    }
}
