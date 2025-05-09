using System;
using FMODUnity;
using UnityEngine;

namespace Services.FmodSound
{
    public interface IEmittersService<in TSound> where TSound : Enum
    {
        void PlaySound(TSound soundType, Transform parentTransform = null, Action onSoundFinished = null);

        StudioEventEmitter GetEmitter(TSound soundType);
        void ReleaseEmitter(TSound soundType, StudioEventEmitter emitter);
        void SetPause(TSound soundType, bool isPaused);
        void Stop(TSound soundType);
    }
}