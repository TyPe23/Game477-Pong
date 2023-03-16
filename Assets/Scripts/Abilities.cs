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

    float CooldownDuration = 15f;

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

    public GameObject P1_SlowTimeIcon;
    public GameObject P2_SlowTimeIcon;
    private SpriteRenderer P1_SlowTimeIcon_SpriteRenderer;
    private Color P1_SlowTimeColor;
    private SpriteRenderer P2_SlowTimeIcon_SpriteRenderer;
    private Color P2_SlowTimeColor;

    public GameObject P1_FlipControlsIcon;
    public GameObject P2_FlipControlsIcon;
    private SpriteRenderer P1_FlipControlsIcon_SpriteRenderer;
    private Color P1_FlipControlsColor;
    private SpriteRenderer P2_FlipControlsIcon_SpriteRenderer;
    private Color P2_FlipControlsColor;

    void Start()
    {
        // Good code lmao
        P1_SlowTimeIcon_SpriteRenderer = P1_SlowTimeIcon.GetComponent<SpriteRenderer>();
        P1_SlowTimeColor = new Color(255f,255f,255f,0f);
        P1_SlowTimeIcon_SpriteRenderer.color = P1_SlowTimeColor;

        P1_FlipControlsIcon_SpriteRenderer = P1_FlipControlsIcon.GetComponent<SpriteRenderer>();
        P1_FlipControlsColor = new Color(255f,255f,255f,0f);
        P1_FlipControlsIcon_SpriteRenderer.color = P1_FlipControlsColor;

        P2_SlowTimeIcon_SpriteRenderer = P2_SlowTimeIcon.GetComponent<SpriteRenderer>();
        P2_SlowTimeColor = new Color(255f,255f,255f,0f);
        P2_SlowTimeIcon_SpriteRenderer.color = P2_SlowTimeColor;

        P2_FlipControlsIcon_SpriteRenderer = P2_FlipControlsIcon.GetComponent<SpriteRenderer>();
        P2_FlipControlsColor = new Color(255f,255f,255f,0f);
        P2_FlipControlsIcon_SpriteRenderer.color = P2_FlipControlsColor;

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

        if (Input.GetKeyDown("q") && Player1Abilities[AbilityType.SlowTime] == true && !isActive_SlowTime && !isSlowTimeCooldown_P1)
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
        if (Input.GetKeyDown("e") && Player1Abilities[AbilityType.FlipControls] == true && !isActive_FlipControls && !isFlipControlCooldown_P1)
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
        

        if (Input.GetKeyDown("i") && Player2Abilities[AbilityType.SlowTime] == true && !isActive_SlowTime && !isSlowTimeCooldown_P2)
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
        if (Input.GetKeyDown("p") && Player2Abilities[AbilityType.FlipControls] == true && !isActive_FlipControls && !isFlipControlCooldown_P2)
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
        P1_FlipControlsColor = new Color(255f,255f,255f,0.3f);
        P1_FlipControlsIcon_SpriteRenderer.color = P1_FlipControlsColor;
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 1 flip controls cooldown is over!");
        P1_FlipControlsColor = new Color(255f,255f,255f,0.65f);
        P1_FlipControlsIcon_SpriteRenderer.color = P1_FlipControlsColor;
        isFlipControlCooldown_P1 = false;
    }

    IEnumerator P2_FlipControlsCooldown()
    {
        print("Player 2 flip controls cooldown begins...");
        P2_FlipControlsColor = new Color(255f,255f,255f,0.3f);
        P2_FlipControlsIcon_SpriteRenderer.color = P2_FlipControlsColor;
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 2 flip controls cooldown is over!");
        P2_FlipControlsColor = new Color(255f,255f,255f,0.65f);
        P2_FlipControlsIcon_SpriteRenderer.color = P2_FlipControlsColor;
        isFlipControlCooldown_P2 = false;
    }

    IEnumerator P1_SlowTimeCooldown()
    {
        print("Player 1 slow time cooldown begins...");
        P1_SlowTimeColor = new Color(255f,255f,255f,0.3f);
        P1_SlowTimeIcon_SpriteRenderer.color = P1_SlowTimeColor;
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 1 slow time cooldown is over!");
        P1_SlowTimeColor = new Color(255f,255f,255f,0.65f);
        P1_SlowTimeIcon_SpriteRenderer.color = P1_SlowTimeColor;
        isSlowTimeCooldown_P1 = false;
    }

    IEnumerator P2_SlowTimeCooldown()
    {
        print("Player 2 slow time cooldown begins...");
        P2_SlowTimeColor = new Color(255f,255f,255f,0.3f);
        P2_SlowTimeIcon_SpriteRenderer.color = P2_SlowTimeColor;
        yield return new WaitForSeconds(CooldownDuration);
        print("Player 2 slow time cooldown is over!");
        P2_SlowTimeColor = new Color(255f,255f,255f,0.65f);
        P2_SlowTimeIcon_SpriteRenderer.color = P1_SlowTimeColor;
        isSlowTimeCooldown_P2 = false;
    }

    void SetPlayer1ActiveAbilities()
    {
        if (!isSlowTimeCooldown_P1 && Ball.scoreLeft >= 2)
        {
            P1_SlowTimeColor = new Color(255f,255f,255f,0.65f);
            P1_SlowTimeIcon_SpriteRenderer.color = P1_SlowTimeColor;
        }
        if (!isFlipControlCooldown_P1 && Ball.scoreLeft >= 4)
        {
            P1_FlipControlsColor = new Color(255f,255f,255f,0.65f);;
            P1_FlipControlsIcon_SpriteRenderer.color = P1_FlipControlsColor;
        }

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
        if (!isSlowTimeCooldown_P2 && Ball.scoreRight >= 2)
        {
            P2_SlowTimeColor = new Color(255f,255f,255f,0.65f);
            P2_SlowTimeIcon_SpriteRenderer.color = P2_SlowTimeColor;
        }
        if (!isFlipControlCooldown_P2 && Ball.scoreRight >= 4)
        {
            P2_FlipControlsColor = new Color(255f,255f,255f,0.65f);;
            P2_FlipControlsIcon_SpriteRenderer.color = P2_FlipControlsColor;
        }

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
