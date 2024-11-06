using Domain.Framework;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth;

public class ApplicationUser : IdentityUser
{
    // Additional properties here
    public ICollection<UserRating> Ratings { get; set; }
    public ICollection<Bookmark> Bookmarks { get; set; }
    public ICollection<SearchHistory> SearchHistories { get; set; }
    public ICollection<Note> Notes { get; set; }
}