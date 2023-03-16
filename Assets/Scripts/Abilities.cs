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

    private Paddle Paddle;
    public GameObject Paddles;

    float CooldownDuration = 10f;

    bool isSlowTimeCooldown_P1 = false;
    bool isSlowTimeCooldown_P2 = false;
    bool isActive_SlowTime = false;

    bool isFlipControlCooldown_P1 = false;
    bool isFlipControlCooldown_P2 = false;
    bool isActive_FlipControls = false;
    float FlipControlsDuration = 5f;

    float SlowTimeDuration = 1f;
    float SlowTimeBallSpeed = 0.5f;
    float TempBallSpeed;

    public AudioSource SlowTimeAudioSource;
    public AudioSource Soundtrack;

    void Start()
    {
        // Good code lmao
        Ball = ball.GetComponent<Ball>();
        Paddle = Paddles.GetComponent<Paddle>();
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

        if (Input.GetKeyDown("1") && Player1Abilities[AbilityType.SlowTime] == true && !isActive_SlowTime && !isSlowTimeCooldown_P1)
        {
            // Disable ability
            isActive_SlowTime = true;
            isSlowTimeCooldown_P1 = true;
            Player1Abilities[AbilityType.SlowTime] = false;
            StartCoroutine(LowerSoundtrackVolume());
            SlowTimeAudioSource.Play();

            // Slow down the ball
            print("Player 1 slowed down time!");
            StartCoroutine(SlowTime());

            // Place ability on cooldown
            StartCoroutine(P1_SlowTimeCooldown());
        }
        if (Input.GetKeyDown("2") && Player1Abilities[AbilityType.FlipControls] == true && !isActive_FlipControls && !isFlipControlCooldown_P1)
        {
            // Disable ability
            isActive_FlipControls = true;
            isFlipControlCooldown_P1 = true;
            Player1Abilities[AbilityType.FlipControls] = false;

            // Flip enemy controls
            StartCoroutine(FlipControls("P2"));

            // Place ability on cooldown
            StartCoroutine(P1_FlipControlsCooldown());
        }
        

        if (Input.GetKeyDown(".") && Player2Abilities[AbilityType.SlowTime] == true && !isActive_SlowTime && !isSlowTimeCooldown_P2)
        {
            // Disable ability
            isActive_SlowTime = true;
            isSlowTimeCooldown_P2 = true;
            Player2Abilities[AbilityType.SlowTime] = false;
            StartCoroutine(LowerSoundtrackVolume());
            SlowTimeAudioSource.Play();

            // Slow down the ball
            print("Player 2 slowed down time!");
            StartCoroutine(SlowTime());

            // Place ability on cooldown
            StartCoroutine(P2_SlowTimeCooldown());
        }
        if (Input.GetKeyDown("/") && Player2Abilities[AbilityType.FlipControls] == true && !isActive_FlipControls && !isFlipControlCooldown_P2)
        {
            // Disable ability
            isActive_FlipControls = true;
            isFlipControlCooldown_P2 = true;
            Player2Abilities[AbilityType.FlipControls] = false;

            // Flip enemy controls
            StartCoroutine(FlipControls("P1"));

            // Place ability on cooldown
            StartCoroutine(P2_FlipControlsCooldown());
        }
        
    }

    IEnumerator LowerSoundtrackVolume()
    {
        Soundtrack.volume = 0.1f;
        yield return new WaitForSeconds(SlowTimeDuration + 0.5f);
        Soundtrack.volume = 0.5f;
    }

    IEnumerator SlowTime()
    {
        TempBallSpeed = Ball.speed;
        if (TempBallSpeed == 0)
        {
            // kind of a hacky fix, doesn't feel awful when playing, but suboptimal
            TempBallSpeed = Ball.acceleration + 2;
        }
        Ball.speed = SlowTimeBallSpeed;
        print("Ball is slowed...");
        yield return new WaitForSeconds(SlowTimeDuration);
        print("Ball is back to speed...");
        isActive_SlowTime = false;
        Ball.speed = TempBallSpeed;
    }

    IEnumerator FlipControls(string enemy)
    {
        if (enemy == "P1")
        {
            var P1_up = Paddle.P1_up;
            Paddle.P1_up = Paddle.P1_down;
            Paddle.P1_down = P1_up;
        }
        else if (enemy == "P2")
        {
            var P2_up = Paddle.P2_up;
            Paddle.P2_up = Paddle.P2_down;
            Paddle.P2_down = P2_up;
        }
        print(enemy + " controls flipped!");
        yield return new WaitForSeconds(FlipControlsDuration);
        if (enemy == "P1")
        {
            var P1_down = Paddle.P1_up;
            Paddle.P1_up = Paddle.P1_down;
            Paddle.P1_down = P1_down;
        }
        else if (enemy == "P2")
        {
            var P2_down = Paddle.P2_up;
            Paddle.P2_up = Paddle.P2_down;
            Paddle.P2_down = P2_down;
        }
        print(enemy + " controls back to normal!");
        isActive_FlipControls = false;
    }

    IEnumerator P1_FlipControlsCooldown()
    {
        print("Player 1 flip controls cooldown begins...");
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 1 flip controls cooldown is over!");
        isFlipControlCooldown_P1 = false;
    }

    IEnumerator P2_FlipControlsCooldown()
    {
        print("Player 2 flip controls cooldown begins...");
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 2 flip controls cooldown is over!");
        isFlipControlCooldown_P2 = false;
    }

    IEnumerator P1_SlowTimeCooldown()
    {
        print("Player 1 slow time cooldown begins...");
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 1 slow time cooldown is over!");
        isSlowTimeCooldown_P1 = false;
    }

    IEnumerator P2_SlowTimeCooldown()
    {
        print("Player 2 slow time cooldown begins...");
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 2 slow time cooldown is over!");
        isSlowTimeCooldown_P2 = false;
    }

    void SetPlayer1ActiveAbilities()
    {
        if (Ball.scoreLeft >= 4)
        {
            Player1Abilities[AbilityType.FlipControls] = true;
            Player1Abilities[AbilityType.SlowTime] = true;
        }
        else if (Ball.scoreLeft >= 2)
        {
            Player1Abilities[AbilityType.SlowTime] = true;
        }        
    }

    void SetPlayer2ActiveAbilities()
    {
        if (Ball.scoreRight >= 4)
        {
            Player2Abilities[AbilityType.FlipControls] = true;
            Player2Abilities[AbilityType.SlowTime] = true;
        }
        else if (Ball.scoreRight >= 2)
        {
            Player2Abilities[AbilityType.SlowTime] = true;
        }
    }
}
