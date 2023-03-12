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

    float CooldownTime = 10f;
    bool isSlowTimeCooldown_P1 = false;
    bool isSlowTimeCooldown_P2 = false;

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
        SetPlayer1ActiveAbilities();
        SetPlayer2ActiveAbilities();
        
        if (Input.GetKeyDown("1") && Player1Abilities[AbilityType.SlowTime] == true)
        {
            isSlowTimeCooldown_P1 = true;
            Player1Abilities[AbilityType.SlowTime] = false;
            // do thing
            print("Player 1 slowed down time!");
            StartCoroutine(P1_SlowTimeCooldown());
        }

        if (Input.GetKeyDown("8") && Player2Abilities[AbilityType.SlowTime] == true)
        {
            isSlowTimeCooldown_P2 = true;
            Player2Abilities[AbilityType.SlowTime] = false;
            // do thing
            print("Player 2 slowed down time!");
            StartCoroutine(P2_SlowTimeCooldown());
        }
    }

    IEnumerator P1_SlowTimeCooldown()
    {
        print("Player 1 slow time cooldown begins...");
        yield return new WaitForSeconds(CooldownTime);
        print("Player 1 slow time cooldown is over!");
        isSlowTimeCooldown_P1 = false;
    }

    IEnumerator P2_SlowTimeCooldown()
    {
        print("Player 2 slow time cooldown begins...");
        yield return new WaitForSeconds(CooldownTime);
        print("Player 2 slow time cooldown is over!");
        isSlowTimeCooldown_P2 = false;
    }

    void SetPlayer1ActiveAbilities()
    {
        if (Ball.scoreLeft >= 9)
        {
            //Player1Abilities[AbilityType.ShrinkBall] = true;
        }
        else if (Ball.scoreLeft >= 6)
        {
            //Player1Abilities[AbilityType.FlipControls] = true;
        }
        else if (Ball.scoreLeft >= 3 && !isSlowTimeCooldown_P1)
        {
            Player1Abilities[AbilityType.SlowTime] = true;
        }        
    }

    void SetPlayer2ActiveAbilities()
    {
        if (Ball.scoreRight >= 9)
        {
            //Player2Abilities[AbilityType.ShrinkBall] = true;
        }
        else if (Ball.scoreRight >= 6)
        {
            //Player2Abilities[AbilityType.FlipControls] = true;
        }
        else if (Ball.scoreRight >= 3 && !isSlowTimeCooldown_P2)
        {
            Player2Abilities[AbilityType.SlowTime] = true;
        }
    }
}
