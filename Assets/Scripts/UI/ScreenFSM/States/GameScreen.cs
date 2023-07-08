using UnityEngine;

public class GameScreen : ScreenState
{
    [SerializeField] private Animator _animator;

    public override void Entry()
    {
        _animator.SetTrigger("SceneOpening");
        GameController.Instance.GameWin += () => ScreenFSM.Instance.ChangeScreen(ScreenFSM.State.Win);
        GameController.Instance.GameLose += () => ScreenFSM.Instance.ChangeScreen(ScreenFSM.State.Lose);
    }

    public override void Exit()
    {
        GameController.Instance.GameWin -= () => ScreenFSM.Instance.ChangeScreen(ScreenFSM.State.Win);
        GameController.Instance.GameLose -= () => ScreenFSM.Instance.ChangeScreen(ScreenFSM.State.Lose);
    }
}
