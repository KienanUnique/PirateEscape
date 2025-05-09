using Db.Sounds;
using Services.FmodSound.Utils;
using Services.FmodSound.Utils.Fabrics;
using Services.FmodSound.Utils.Fabrics.Impl;
using Zenject;

namespace Services.FmodSound.Impl.Ui.Impl
{
    public class UiSoundsService : AEmittersService<EUiSoundFxType>, IUiSoundFxService, IInitializable
    {
        private readonly ISoundFxBase _soundFxBase;
        
        private IEmittersPoolFabric<EUiSoundFxType> _fabric;
        
        protected override IEmittersPoolFabric<EUiSoundFxType> Fabric => _fabric;
        protected override bool IsGlobal => true;

        public UiSoundsService(ISoundFxBase soundFxBase)
        {
            _soundFxBase = soundFxBase;
        }

        public void Initialize()
        {
            _fabric = new DefaultEmittersPoolFabric<EUiSoundFxType>(_soundFxBase.UiSoundsEventReferences, IsGlobal);
        }
    }
}