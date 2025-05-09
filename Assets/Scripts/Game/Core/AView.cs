using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public abstract class AView : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<AModule> _modules;
        
        public void Initialize()
        {
            foreach (var module in _modules) 
                module.Initialize();

            OnInitialize();
        }
        
        protected virtual void OnInitialize() { }

#if UNITY_EDITOR
        [Button]
        public virtual void Autofill()
        {
            _modules = new List<AModule>();
            _modules.AddRange(GetComponentsInChildren<AModule>());
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}