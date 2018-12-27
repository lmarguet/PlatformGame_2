using System;
using Zenject;

namespace Game.Initializers
{
    public interface IDisposableGameInitializer: IDisposable, IInitializable
    {

        void ResetDisposer();
    }
}