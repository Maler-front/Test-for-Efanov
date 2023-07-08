using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OnClickLoadScene : EntryLeaf
{
    [SerializeField] [Tooltip("The button that enables the transition to another scene")] private Button _activateButton;
    [SerializeField] [Tooltip("The index of the scene to go to when you click on the button")] private int _sceneIndex;
    [SerializeField] private LoadScene _sceneLoad = LoadScene.Variable;

    protected override void AwakeComponent()
    {
        if(_activateButton == null)
        {
            if(!TryGetComponent(out _activateButton))
            {
                Debug.LogError($"You dont have button on {name}");
                base.AwakeComponent();
                return;
            }
        }

        switch (_sceneLoad) 
        {
            case LoadScene.Current:
                {
                    _sceneIndex = SceneManager.GetActiveScene().buildIndex;
                    break;
                }
            case LoadScene.Next:
                {
                    _sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                    break;
                }
            case LoadScene.FirstNotPassedLevel:
                {
                    _sceneIndex = LoadingManager._firstNotPassedLevel;
                    break;
                }
        }

        bool activeButtonEnabledState = _activateButton.enabled;
        _activateButton.enabled = true;
        _activateButton.onClick.AddListener(() => LoadingManager.Instance.LoadScene(_sceneIndex));
        _activateButton.enabled = activeButtonEnabledState;

        base.AwakeComponent();
    }

    private enum LoadScene
    {
        Current,
        Next,
        FirstNotPassedLevel,
        Variable
    }
}
