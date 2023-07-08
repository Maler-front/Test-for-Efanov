using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrentLevelShower : EntryLeaf
{
    protected override void StartComponent()
    {
        if (!TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textMeshPro))
        {
            Debug.LogError($"There is not enough TextMeshProUGUI on the {name} object");
            return;
        }

        textMeshPro.text = LoadingManager._firstNotPassedLevel.ToString();
        base.StartComponent();
    }
}
