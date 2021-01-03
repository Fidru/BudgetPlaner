using IData.Interfaces;

namespace IData.Services
{
    public interface IElementFactory<T>
    {
        T CreateEmpty();

        T Copy(T original);
    }
}