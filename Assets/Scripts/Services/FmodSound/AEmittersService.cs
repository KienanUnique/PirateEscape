using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FMODUnity;
using Services.FmodSound.Utils.Fabrics;
using UnityEngine;
using UnityEngine.Pool;

namespace Services.FmodSound
{
    public abstract class AEmittersService<TSoundType> : IEmittersService<TSoundType> where TSoundType : Enum
    {
        private const string EmitterGameObjectsParentsName = "StudioEventEmitterPool";
        
        private readonly Dictionary<TSoundType, IObjectPool<StudioEventEmitter>> _pools = new();
        private readonly Dictionary<TSoundType, List<StudioEventEmitter>> _activeEmitters = new();
        
        private Transform _poolEmittersParent;
        
        protected abstract IEmittersPoolFabric<TSoundType> Fabric { get; }
        protected abstract bool IsGlobal { get; }

        public void PlaySound(TSoundType soundType, Transform parentTransform = null, Action onSoundFinished = null)
        {
            var emitter = GetActivatedEmitter(soundType);

            if (parentTransform != null)
            {
                var emitterTransform = emitter.transform;
                emitterTransform.SetParent(parentTransform);
                emitterTransform.position = parentTransform.position;
            }
            else
            {
                SetEmitterPoolParent(emitter);
            }

            emitter.Play();

            WaitUntilSoundEnd(emitter, soundType, onSoundFinished).Forget();
        }

        public StudioEventEmitter GetEmitter(TSoundType soundType)
        {
            var emitter = GetActivatedEmitter(soundType);
            return emitter;
        }

        public void ReleaseEmitter(TSoundType soundType, StudioEventEmitter emitter)
        {
            ReturnEmitterToPool(soundType, emitter);
        }

        public void SetPause(TSoundType soundType, bool isPaused)
        {
            foreach (var studioEventEmitter in _activeEmitters[soundType])
                studioEventEmitter.EventInstance.setPaused(isPaused);
        }

        public void Stop(TSoundType soundType)
        {
            foreach (var studioEventEmitter in _activeEmitters[soundType])
                studioEventEmitter.Stop();
        }

        private StudioEventEmitter GetActivatedEmitter(TSoundType soundType)
        {
            if (_pools.ContainsKey(soundType)) 
                return _pools[soundType].Get();
                
            var newPool = Fabric.CreatePool(soundType);
            _pools[soundType] = newPool;

            var emitter = newPool.Get();
            
            if (!_activeEmitters.TryGetValue(soundType, out var emittersList))
                _activeEmitters[soundType] = new List<StudioEventEmitter> { emitter };
            else
                emittersList.Add(emitter);

            return emitter;
        }

        private void ReturnEmitterToPool(TSoundType soundType, StudioEventEmitter emitterPoolItem)
        {
            _activeEmitters[soundType].Remove(emitterPoolItem);

            var emittersStack = _pools[soundType];
            emittersStack.Release(emitterPoolItem);
        }

        private void SetEmitterPoolParent(StudioEventEmitter emitterPoolItem)
        {
            var emitterTransform = emitterPoolItem.transform;

            if (_poolEmittersParent == null)
            {
                var poolEmittersParentGameObject = new GameObject($"{EmitterGameObjectsParentsName} + {typeof(TSoundType).Name}");
                if (IsGlobal)
                    UnityEngine.Object.DontDestroyOnLoad(poolEmittersParentGameObject);
                
                _poolEmittersParent = poolEmittersParentGameObject.transform;
            }

            emitterTransform.SetParent(_poolEmittersParent);
        }

        private async UniTaskVoid WaitUntilSoundEnd(StudioEventEmitter emitter, TSoundType soundType, Action onSoundFinished)
        {
            var cancellationToken = emitter.gameObject.GetCancellationTokenOnDestroy();

            try
            {
                await UniTask.WaitWhile(emitter.IsPlaying, cancellationToken: cancellationToken);
                ReturnEmitterToPool(soundType, emitter);
                onSoundFinished?.Invoke();
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}