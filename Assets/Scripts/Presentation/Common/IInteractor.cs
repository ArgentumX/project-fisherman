using Domain.Models.Common;

namespace Presentation.Common
{
    public interface IInteractor<out T> where T : BaseModel
    {
        T GetModel();
    }
}