using UniRx;

namespace Game.Shared.Abstract
{
    public abstract class AbstractService
    {
        
        protected readonly CompositeDisposable Disposer = new CompositeDisposable();
        
    }
}