namespace ESC_MANEJO.CORE.Interfaces
{
    public interface IParseService
    {
        T Deserealize<T>(dynamic model);
        string Serialize(dynamic model);
    }
}
