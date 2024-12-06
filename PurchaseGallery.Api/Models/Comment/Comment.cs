using System;
using System.Collections.Generic;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.Comments;

public class Comment
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required Guid PurchaseRequestId { get; set; }
    public Guid CommenterId { get; set; }
    public required User Commenter { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int Likes { get; set; }
    public int Dislikes { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsEdited { get; set; }

    public Guid? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
