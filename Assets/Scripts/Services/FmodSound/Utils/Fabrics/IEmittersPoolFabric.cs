using System;
using FMODUnity;
using UnityEngine.Pool;

namespace Services.FmodSound.Utils.Fabrics
{
    public interface IEmittersPoolFabric<in TSoundType> where TSoundType : Enum
    {
        IObjectPool<StudioEventEmitter> CreatePool(TSoundType type);
    }
}