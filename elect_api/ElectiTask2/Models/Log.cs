namespace ElectiTask.Models
{
    // Define the Log class
    public class Log
    {
        // Properties that represent different attributes of a log entry
        public Guid? Id { get; set; }
        // A property to hold the unique identifier of the log entry (nullable Guid)

        public string? MachineName { get; set; }
        // A property to hold the machine name where the log entry was generated (nullable string)

        public string? Logged { get; set; }
        // A property to hold the timestamp when the log entry was generated (nullable string)

        public string? Level { get; set; }
        // A property to hold the log level (e.g., Info, Warning, Error) of the log entry (nullable string)

        public string? Message { get; set; }
        // A property to hold the main message content of the log entry (nullable string)

        public string? Logger { get; set; }
        public string? Callsite { get; set; }
        // A property to hold the callsite (source location) where the log entry was generated (nullable string)

        public string? Exception { get; set; }
        // A property to hold information about an exception associated with the log entry (nullable string)
    }
}
