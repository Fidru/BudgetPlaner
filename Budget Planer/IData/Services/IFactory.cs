namespace IData.Services
{
    public interface IFactory<T>
    {
        T GetCreateEmpty();

        T Copy(T original);

        void Delete(T toDelete);
    }
}