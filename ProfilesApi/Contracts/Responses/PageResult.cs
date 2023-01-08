namespace ProfilesApi.Contracts;

public class PageResult<T>
{
    public int Count { get; set; }
    public List<T> Items { get; set; }

}