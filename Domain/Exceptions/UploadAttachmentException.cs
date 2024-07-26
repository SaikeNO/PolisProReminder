namespace PolisProReminder.Domain.Exceptions;

public class UploadAttachmentException(string filename) : Exception($"Failed to upload a {filename}")
{
}
