using System;
using Application.Interfaces.Factories;
using Domain.Models;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class MutationFactory : IMutationFactory
    {
        public Mutation Create(MutationType type)
        {
            switch (type) {
                case MutationType.Stamina:
                    // TODO default mutation settings (scriptable objs?)
                    return new StaminaMutation(50, MutationStage.Initial);
                case MutationType.Eyes:
                    return new EyesMutation(MutationStage.Initial);
                case MutationType.Movements:
                    return new MovementsMutation(MutationStage.Initial);
                case MutationType.Breath:
                    return new BreathMutation(MutationStage.Initial);
                default:
                    throw new Exception($"Unknown MutationType: {type}");
            }
        }
    }
}