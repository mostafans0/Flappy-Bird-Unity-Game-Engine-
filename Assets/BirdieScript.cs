using UnityEngine;
using System.Collections;

public class BirdieScript : MonoBehaviour
{
    public Rigidbody2D birdierigidbody2D;
    public float flapstrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public AudioSource flapSound;
    public SpriteRenderer wingRenderer;
    public Sprite wingUp;
    public Sprite wingDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "Birdie";
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        flapSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!birdIsAlive) return;

        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            StartCoroutine(FlapWing());
            flapSound.Play();
            birdierigidbody2D.linearVelocity = Vector2.up * flapstrength;
        }

        if (transform.position.y > 15 || transform.position.y < -15)
        {
            birdIsAlive = false;
            logic.gameOver();
        }
    }

    IEnumerator FlapWing()
    {
        wingRenderer.sprite = wingDown;
        yield return new WaitForSeconds(0.15f);
        wingRenderer.sprite = wingUp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!birdIsAlive) return;

        birdIsAlive = false;
        logic.gameOver();
    }
}
