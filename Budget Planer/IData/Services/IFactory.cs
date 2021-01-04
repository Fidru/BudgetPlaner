namespace IData.Services
{
    public interface IFactory<T>
    {
        T CreateEmpty();

        void Delete(T element);

        T Copy(T original);
    }
}