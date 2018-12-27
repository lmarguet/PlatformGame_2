using System;
using Game.Score;
using Game.Shared.Abstract;
using UniRx;
using Zenject;

namespace Game
{
    public class GameStateManager : AbstractDisposable, IDisposable
    {
        [Inject] private GameStateModel _gameStateModel;
        [Inject] private ScoreModel _scoreModel;

        public readonly ReactiveCommand OnGameDispose;
        public readonly ReactiveCommand OnGameRestart;
        public readonly ReactiveCommand OnGameResume;

        public GameStateManager()
        {
            OnGameDispose = new ReactiveCommand();
            OnGameRestart = new ReactiveCommand();
            OnGameResume = new ReactiveCommand();
        }

        private void DisposeCurrentGame()
        {
            OnGameDispose.Execute();
        }

        public void Restart()
        {
            _gameStateModel.Ready();
            OnGameRestart.Execute();
        }

        public void Continue()
        {
            _gameStateModel.Continue();
            OnGameResume.Execute();
        }

        public void StartNewLevel()
        {
            _gameStateModel.Ready();
            _scoreModel.Reset();
            OnGameRestart.Execute();
        }

        public void StartGame()
        {
            _gameStateModel.StartGame();
        }

        public void PlayerDied()
        {
            _gameStateModel.PlayerDied();
        }

        public void LevelComplete()
        {
            _gameStateModel.LevelComplete();
        }
    }
}