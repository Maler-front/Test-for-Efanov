using System.Collections;
using UnityEngine;

public class EndScreen : ScreenState
{
    [SerializeField] Animator _animator;

    public override void Entry()
    {
        StartCoroutine(ShowScreen());
    }

    public override void Exit() { }

    private IEnumerator ShowScreen()
    {
        yield return new WaitForSeconds(.1f);
        _animator.SetTrigger("Show");
    }
}
