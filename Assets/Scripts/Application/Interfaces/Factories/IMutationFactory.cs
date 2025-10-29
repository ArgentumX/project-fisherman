using Domain.Models;

namespace Application.Interfaces.Factories {
    public interface IMutationFactory {
        Mutation Create(MutationType type);
    }
}