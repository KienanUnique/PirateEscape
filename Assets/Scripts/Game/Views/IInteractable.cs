using UnityEngine;

namespace Game.Views
{
    public interface IInteractable
    {
        Vector3 Position { get; }
        void Interact();
    }
}