namespace BookBarn.Services.Policies
{
    public interface IBookVisibilityPolicy
    {
        bool IsVisible(string? title, decimal price);
    }
}