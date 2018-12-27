using Game.Boost;
using Game.Shared.Abstract;
using UniRx;

namespace Game.Services
{
    public class BoostService:AbstractService
    {
        public readonly ReactiveCommand<BoostType> OnBoostActivate;

        public BoostService()
        {
            OnBoostActivate = new ReactiveCommand<BoostType>();
        }

        public void ActivateBoost(BoostType boostType)
        {
            OnBoostActivate.Execute(boostType);
        }
    }
}