namespace IData.Services
{
    public interface IFactory<T>
    {
        T GetCreateEmpty();

        T Copy(T original);
    }
}