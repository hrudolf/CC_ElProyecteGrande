namespace backend.Model.Records;

public record Forecast()
{
    public DateTime StartOfWeek { get; set; }
    public DateTime EndOfWeek { get; set; }
    public int Amount { get; set; }

    public Forecast(DateTime startOfWeek, DateTime endOfWeek, int amount) : this()
    {
        StartOfWeek = startOfWeek;
        EndOfWeek = endOfWeek;
        Amount = amount;
    }
};