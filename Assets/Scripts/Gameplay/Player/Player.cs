using System;
using UnityEngine;

[RequireComponent(typeof(FollowPath), typeof(DrawManager))]
public class Player : EntryLeaf
{
    private FollowPath _followPath;
    private DrawManager _drawManager;

    public Action ReadyToMove;
    public Action Die;
    public Action Finish;

    protected override void AwakeComponent()
    {
        _drawManager = GetComponent<DrawManager>();
        _followPath = GetComponent<FollowPath>();

        base.AwakeComponent();
    }

    protected override void StartComponent()
    {
        _drawManager.LineDrawed += () => ReadyToMove?.Invoke();
        _followPath.EndMoving += () => Finish?.Invoke();
        GameController.Instance.AllPlayersAreReady += () => _followPath.StartMoving();
        base.StartComponent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Player"))
        {
            Die?.Invoke();
            Destroy(gameObject);
        }
    }
}
