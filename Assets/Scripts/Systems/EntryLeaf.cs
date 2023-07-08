using System.Collections.Generic;
using UnityEngine;

public class EntryLeaf : MonoBehaviour
{
    [SerializeReference] protected List<EntryLeaf> _leafes;
    private bool _dontHaveLeafes;

    protected virtual void AwakeComponent()
    {
        if (_leafes.Count == 0)
            _dontHaveLeafes = true;

        if (!_dontHaveLeafes)
        {
            foreach (EntryLeaf leaf in _leafes)
            {
                leaf.AwakeComponent();
            }
        }
    }

    protected virtual void StartComponent()
    {
        if (!_dontHaveLeafes)
        {
            foreach (EntryLeaf leaf in _leafes)
            {
                leaf.StartComponent();
            }
        }
    }

    protected virtual void UpdateComponent()
    {
        if (!_dontHaveLeafes)
        {
            foreach (EntryLeaf leaf in _leafes)
            {
                leaf.UpdateComponent();
            }
        }
    }

    protected virtual void EnableComponent()
    {
        if (!_dontHaveLeafes)
        {
            foreach (EntryLeaf leaf in _leafes)
            {
                leaf.EnableComponent();
            }
        }
    }

    protected virtual void DisableComponent()
    {
        if (!_dontHaveLeafes)
        {
            foreach (EntryLeaf leaf in _leafes)
            {
                leaf.DisableComponent();
            }
        }
    }

    private void OnDisable()
    {
        DisableComponent();
    }

    private void OnEnable()
    {
        EnableComponent();
    }
}
