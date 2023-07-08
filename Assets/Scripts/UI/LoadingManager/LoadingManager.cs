using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadingManager : EntryLeaf
{   
    public static LoadingManager Instance { get; private set; }
    public static int _firstNotPassedLevel = 1;

    private int _sceneIndex;

    private Animator _animator;

    private static float _minTimeLoading = 1f;
    private AsyncOperation _loadingOperation;
    private float _loadingTime = 0f;

    protected override void AwakeComponent()
    {
        Instance = this;

        _animator = GetComponent<Animator>();

        base.AwakeComponent();
    }

    protected override void StartComponent()
    {
        _animator.SetTrigger("SceneOpening");

        base.StartComponent();
    }

    public void LoadFirstNotPassedLevel() => LoadScene(_firstNotPassedLevel);

    public void LoadScene(int sceneIndex)
    {
        if (_firstNotPassedLevel < sceneIndex)
            _firstNotPassedLevel = sceneIndex;

        _sceneIndex = sceneIndex;
        _animator.SetTrigger("SceneClosing");
    }

    private void OnClosingAnimationEnd()
    {
        if(_sceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.LogError($"You cannot load a scene with index {_sceneIndex}");
        }
        else
        {
            _loadingOperation = SceneManager.LoadSceneAsync(_sceneIndex);
            _loadingOperation.allowSceneActivation = false;

            StartCoroutine(ChangeSceneAfterMinLoadingTime());
        }
    }

    private IEnumerator ChangeSceneAfterMinLoadingTime()
    {
        while (_loadingTime < _minTimeLoading)
        {
            _loadingTime += Time.unscaledDeltaTime;
            yield return null;
        }

        _loadingOperation.allowSceneActivation = true;
    }
}
