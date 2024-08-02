namespace ImageManagementApi.Models
{
public class Image
{
    public int Id { get; set; }
    public string User { get; set; }
    public DateTime DateCreated { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
}

}
