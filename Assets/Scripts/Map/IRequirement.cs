namespace TouhouPride.Map
{
    public interface IRequirement<T>
    {
        public void Unlock(T o);
    }
}