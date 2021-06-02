namespace ESC_MANEJO.CORE.Interfaces
{
    public interface ICryptoService
    {
        string Encode(string data);
        string Decode(string data);
    }
}
