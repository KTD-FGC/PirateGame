using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;

    PlayerGroundedState groundedState = new PlayerGroundedState();
    PlayerAirState airState = new PlayerAirState();
    PlayerSpinningState spinState = new PlayerSpinningState();
    PlayerGrindingState grindState = new PlayerGrindingState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = groundedState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
}
