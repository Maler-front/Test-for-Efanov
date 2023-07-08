using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : EntryLeaf
{
    private void Awake()
    {
        AwakeComponent();
    }

    private void Start()
    {
        StartComponent();
    }

    private void Update()
    {
        UpdateComponent();
    }

    private void OnEnable()
    {
        EnableComponent();
    }

    private void OnDisable()
    {
        DisableComponent();
    }
}
