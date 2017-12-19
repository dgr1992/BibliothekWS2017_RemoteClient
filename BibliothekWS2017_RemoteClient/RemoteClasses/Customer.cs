namespace BibliothekWS2017_RemoteClient.RemoteClasses{
    public class Customer{
    private string id { get; set; } = "";
    private string firstName { get; set; } = "";
    private string lastName { get; set; } = "";
    private string customerId { get; set; } = "";
    private Address address { get; set; } = null;
    private string dateOfBirth { get; set; } = "";
    private string email { get; set; } = "";
    private string phoneNumber { get; set; } = "";
    private string paymentDate { get; set; } = "";
    }
}