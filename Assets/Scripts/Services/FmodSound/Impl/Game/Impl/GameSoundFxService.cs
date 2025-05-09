using Db.Sounds;
using Services.FmodSound.Utils;
using Services.FmodSound.Utils.Fabrics;
using Services.FmodSound.Utils.Fabrics.Impl;
using Zenject;

namespace Services.FmodSound.Impl.Game.Impl
{
    public class GameSoundFxService : AEmittersService<EGameSoundFxType>, IGameSoundFxService, IInitializable
    {
        private readonly ISoundFxBase _soundFxBase;
        
        private IEmittersPoolFabric<EGameSoundFxType> _fabric;
        
        protected override IEmittersPoolFabric<EGameSoundFxType> Fabric => _fabric;
        protected override bool IsGlobal => false;

        public GameSoundFxService(ISoundFxBase soundFxBase)
        {
            _soundFxBase = soundFxBase;
        }

        public void Initialize()
        {
            _fabric = new DefaultEmittersPoolFabric<EGameSoundFxType>(_soundFxBase.GameSoundsEventReferences, IsGlobal);
        }
    }
}