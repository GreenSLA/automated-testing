namespace RealWorldTests;

public class ArticleData
{
    public ArticleData(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public string? Tags { get; set; }
}
