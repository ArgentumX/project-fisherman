using UnityEngine;

namespace Infrastructure.Extensions
{
    public static class CharacterControllerExtensions
    {
        public static void Teleport(this CharacterController characterController, Vector3 position)
        {
            characterController.enabled = false;
            characterController.transform.position = position;
            characterController.enabled = true;
        }
    }
}