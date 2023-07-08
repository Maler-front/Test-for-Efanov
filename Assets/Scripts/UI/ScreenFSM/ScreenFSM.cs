using UnityEngine;

public class ScreenFSM : EntryLeaf
{
    public static ScreenFSM Instance { get; private set; }

    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private EndScreen _winScreen;
    [SerializeField] private EndScreen _loseScreen;

    private ScreenState _currentState;

    protected override void AwakeComponent()
    {
        Instance = this;

        if (_gameScreen == null || _winScreen == null || _loseScreen == null)
            Debug.LogError($"{name} dont have all states!");
    }

    protected override void StartComponent()
    {
        _currentState = _gameScreen;
        _gameScreen.Entry();
    }

    public void ChangeScreen(State state)
    {
        _currentState.Exit();

        switch (state) 
        {
            case State.Game:
                {
                    _currentState = _gameScreen;
                    break;
                }
            case State.Win:
                {
                    _currentState = _winScreen;
                    break;
                }
            case State.Lose:
                {
                    _currentState = _loseScreen;
                    break;
                }
        }

        _currentState.Entry();
    }

    public enum State
    {
        Game,
        Win,
        Lose
    }
}
