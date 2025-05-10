using UnityEngine;

namespace Game.Views
{
    public interface IClickInteractable
    {
        Vector3 Position { get; }
        float Progress { get; }
        
        void Interact();
    }
}