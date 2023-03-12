using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum AbilityType 
    {
        SlowTime,
        FlipControls,
        ShrinkBall
    }
    // Boolean determines whether or not the ability is active
    public Dictionary<AbilityType, bool> Player1Abilities;
    public Dictionary<AbilityType, bool> Player2Abilities;
    private Ball Ball;
    public GameObject ball;

    void Start()
    {
        // Good code lmao
        Ball = ball.GetComponent<Ball>();
        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        Player1Abilities = new Dictionary<AbilityType, bool>()
        {
            {AbilityType.SlowTime, false},
            {AbilityType.FlipControls, false},
            {AbilityType.ShrinkBall, false}
        };
        Player2Abilities = new Dictionary<AbilityType, bool>()
        {
            {AbilityType.SlowTime, false},
            {AbilityType.FlipControls, false},
            {AbilityType.ShrinkBall, false}
        };
    }

    void Update()
    {
        if (Ball.scoreLeft >= 9)
        {
            Player1Abilities[AbilityType.ShrinkBall] = true;
            //print("Player 1 has 3 abilities.");
        }
        else if (Ball.scoreLeft >= 6)
        {
            Player1Abilities[AbilityType.FlipControls] = true;
            //print("Player 1 has 2 abilities.");
        }
        else if (Ball.scoreLeft >= 3)
        {
            Player1Abilities[AbilityType.SlowTime] = true;
            //print("Player 1 has 1 ability.");
        }

        if (Ball.scoreRight >= 9)
        {
            Player2Abilities[AbilityType.ShrinkBall] = true;
            //print("Player 2 has 3 abilities.");
        }
        else if (Ball.scoreRight >= 6)
        {
            Player2Abilities[AbilityType.FlipControls] = true;
            //print("Player 2 has 2 abilities.");
        }
        else if (Ball.scoreRight >= 3)
        {
            Player2Abilities[AbilityType.SlowTime] = true;
            //print("Player 2 has 1 ability.");
        }
    }
}
