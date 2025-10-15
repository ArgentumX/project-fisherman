namespace Application.Interfaces.Usecases
{
    public interface ISleepUsecase
    {
        bool TrySleep();
        bool IsPossibleToSleep();
    }
}