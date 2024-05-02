namespace Core
{
    public interface IAppContext
    {
        T Resolve<T>();
    }
}
