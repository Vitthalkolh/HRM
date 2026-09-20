public class EmailNotificationTemplate
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public string? ToEmail { get; set; }
    public string? CcEmail { get; set; }
    public string? BccEmail { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }

    public bool IsActive { get; set; }
}