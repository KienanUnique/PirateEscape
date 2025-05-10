using UnityEngine;

namespace Game.Utils.Layers
{
    public static class LayerMasks
    {
        public static int Player => PlayerMask.Value;
        public static int Ground => GroundMask.Value;
        public static int Interactable => InteractableMask.Value;
        public static int Grabbable => GrabbableMask.Value;
        
        private static readonly Mask PlayerMask = new Mask(Layers.Player);
        private static readonly Mask GroundMask = new Mask(Layers.Default);
        private static readonly Mask InteractableMask = new Mask(Layers.Interactable);
        private static readonly Mask GrabbableMask = new Mask(Layers.Grabbable);
        
        private class Mask
        {
            private readonly string[] _layerNames;

            private int? _value;

            public Mask(params string[] layerNames)
            {
                _layerNames = layerNames;
            }

            public int Value
            {
                get
                {
                    if (!_value.HasValue)
                        _value = LayerMask.GetMask(_layerNames);
                    return _value.Value;
                }
            }
        }
        
        private static class Layers
        {
            public const string Player = "Player";
            public const string Default = "Default";
            public const string Interactable = "Interactable";
            public const string Grabbable = "Grabbable";
        }
    }
}