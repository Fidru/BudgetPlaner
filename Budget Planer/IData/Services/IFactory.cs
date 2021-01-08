namespace IData.Services
{
    public interface IFactory<T>
    {
        T Copy(T original);
    }
}