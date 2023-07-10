namespace UP.Domain;

public class BaseUserEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNo { get; set; }
    public string? ContactAddress { get; set; }
    public string? NextOfKin { get; set; }
    public string? NextOfKinPhone { get; set; }
}