using Assets.Scripts.Gameplay.Player;
using Assets.Scripts.Gameplay.Player.Movement_States;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DodgeState : GameplayStateBase
{
    private float DodgeSpeed => stateController.PlayerSettings.DodgeSpeed;
    private float DodgeTime => stateController.PlayerSettings.DodgeTime;

    float elapsedTime = 0f;
    Vector2 dodgeVelocity;

    public override bool CanInterrupt { get; protected set; } = false;

    public override void EnterState(PlayerStateController stateController, PlayerController playerController)
    {
        base.EnterState(stateController, playerController);

        dodgeVelocity = stateController.MoveInput * DodgeSpeed;
        playerController.rb2D.velocity = dodgeVelocity;
        elapsedTime = 0f;
    }

    public override void ExitState()
    {
        // TODO: adjust animators and wrap up anything else associated with state
        stateController.MoveInput = Vector2.zero;
        stateController.SmoothedInputVector = Vector2.zero;
    }

    public override void PerformState()
    {
        playerController.rb2D.velocity = dodgeVelocity;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= DodgeTime)
        {
            stateController.ChangeState(stateController.MoveState);
        }
    }
}
