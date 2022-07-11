namespace PluginBase
{
    public interface IOperation
    {
        char Name { get; }

        int NumberOfOperands { get; }
    }
}