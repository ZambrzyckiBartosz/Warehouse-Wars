
public abstract class Building
{
    public int ID { get; init; }
    public int Level { get; protected set; }
    public abstract decimal CalculateRevenue();
    public abstract decimal CalculateIncome();
    public abstract decimal CalculateArea();

    public void Upgrade()
    {
        Level++;
    }
}
