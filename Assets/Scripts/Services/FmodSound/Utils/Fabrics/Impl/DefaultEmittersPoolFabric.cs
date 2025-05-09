using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Services.FmodSound.Utils.Fabrics.Impl
{
    public class DefaultEmittersPoolFabric<TSoundType> : IEmittersPoolFabric<TSoundType> where TSoundType : Enum
    {
        private const string EmitterGameObjectName = "StudioEventEmitter";
        private const int DefaultEmittersCapacity = 2;

        private readonly IReadOnlyDictionary<TSoundType, EventReference> _referencesByType;
        private readonly bool _isGlobal;

        public DefaultEmittersPoolFabric(
            IReadOnlyDictionary<TSoundType, EventReference> referencesByType, 
            bool isGlobal
        )
        {
            _referencesByType = referencesByType;
            _isGlobal = isGlobal;
        }

        private StudioEventEmitter CreateEmitter(TSoundType soundType)
        {
            var eventReference = _referencesByType[soundType];

            var go = new GameObject($"{EmitterGameObjectName} {soundType.ToString()}");
            var source = go.AddComponent<StudioEventEmitter>();
            source.EventReference = eventReference;

            if (_isGlobal)
                Object.DontDestroyOnLoad(go);

            source.gameObject.SetActive(false);
            
            return source;
        }

        private void HandleReleaseEmitter(StudioEventEmitter emitter)
        {
            emitter.gameObject.SetActive(false);
        }
        
        private void HandleGetEmitter(StudioEventEmitter emitter)
        {
            emitter.gameObject.SetActive(true);
        }

        public IObjectPool<StudioEventEmitter> CreatePool(TSoundType type)
        {
            var pool = new ObjectPool<StudioEventEmitter>(
                () => CreateEmitter(type),
                HandleGetEmitter,
                HandleReleaseEmitter,
                null,
                true,
                DefaultEmittersCapacity
            );

            return pool;
        }
    }
}