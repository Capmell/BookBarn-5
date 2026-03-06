namespace BookBarn.Services.Policies
{
    public class DefaultBookVisibilityPolicy : IBookVisibilityPolicy
    {
        public bool IsVisible(string? title, decimal price)
        {
            return title != null
                && title.Trim() != ""
                && price >= 0;
        }
    }
}