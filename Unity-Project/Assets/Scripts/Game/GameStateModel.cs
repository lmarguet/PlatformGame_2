using Game.Shared.Abstract;
using UniRx;

namespace Game
{
    public class GameStateModel : AbstractModel
    {
        public readonly ReadOnlyReactiveProperty<GameState> State;
        private readonly ReactiveProperty<GameState> _state;

        public GameStateModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            State = _state.ToReadOnlyReactiveProperty();
        }

        public void Ready()
        {
            _state.Value = GameState.Ready;
        }

        public void StartGame()
        {
            _state.Value = GameState.Running;
        }

        public void PlayerDied()
        {
            if (_state.Value == GameState.Complete)
            {
                return;
            }
            
            _state.Value = GameState.Dead;
        }

        public void Continue()
        {
            _state.Value = GameState.Ready;
        }

        public void LevelComplete()
        {
            _state.Value = GameState.Complete;
        }
    }
}