using Assets.Scripts.Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayState 
{
    public bool CanInterrupt { get; }

    public void EnterState();
    public void PerformState();
    public void ExitState();
}
