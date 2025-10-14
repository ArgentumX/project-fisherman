namespace Application.Interfaces
{
    public interface ITickProvider
    {
        void ProvideLogicTick(float deltaTime);
        void ProvideViewTick(float deltaTime);
    }
}