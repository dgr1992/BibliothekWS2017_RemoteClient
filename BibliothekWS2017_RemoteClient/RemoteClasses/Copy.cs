namespace BibliothekWS2017_RemoteClient.RemoteClasses{
    public class Copy{
    private string copyNumber { get; set; } = "";
    private string id { get; set; } = "";
    private string mediaType { get; set; } = "";
    private string mediumId { get; set; } = "";
    private Rental rental { get; set; } = null;
    private Category category { get; set; } = null;
    private string copyStatus { get; set; } = "";
    }
}