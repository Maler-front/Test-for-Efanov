using System;
using UnityEngine;

public class GameController : EntryLeaf
{
    public static GameController Instance { get; private set; }

    private int _numberOfPlayers;
    private int _readyPlayers = 0;
    private int _playersOnFinish = 0;

    public Action AllPlayersAreReady;
    public Action GameWin;
    public Action GameLose;

    protected override void AwakeComponent()
    {
        Instance = this;

        foreach(EntryLeaf leaf in _leafes)
        {
            if(leaf.TryGetComponent(out Player player))
            {
                player.ReadyToMove += Player_OnReadyToMove;
                player.Die += Player_OnDie;
                player.Finish += Player_OnFinish;
                _numberOfPlayers += 1;
            }
        }

        base.AwakeComponent();
    }

    private void Player_OnReadyToMove()
    {
        if (++_readyPlayers == _numberOfPlayers)
            AllPlayersAreReady?.Invoke();
    }

    private void Player_OnDie()
    {
        GameLose?.Invoke();
    }

    private void Player_OnFinish()
    {
        if (++_playersOnFinish == _numberOfPlayers)
        {
            GameWin?.Invoke();
        }
    }
}
