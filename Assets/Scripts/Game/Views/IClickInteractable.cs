using UnityEngine;

namespace Game.Views
{
    public interface IClickInteractable
    {
        Vector3 Position { get; }
        int Progress { get; }
        int MaxProgress { get; }
        
        void Interact();
    }
}