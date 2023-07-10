using UP.Domain.Common;

namespace UP.Domain.User;

public class Permission 
{
    public int Id { get; set; }
    public string ControllerName { get; set; }
    public string Name { get; set; }
}