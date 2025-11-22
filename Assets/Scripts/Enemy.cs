using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem Explotion;
    [SerializeField] ParticleSystem FireSmoke;
    [SerializeField] ParticleSystem GroundExplotion;
    [SerializeField] AudioClip explotionSound;
    Rigidbody rb;
    MeshRenderer mR;
    AudioSource aS;
    bool exploded = false;
    bool smoke = false;
    bool isGrounded = false;
    public int health = 2;
    public int score = 20;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mR = GetComponent<MeshRenderer>();
        aS = GetComponent<AudioSource>();
        aS.Play();
    }

    void Update()
    {
        
        if (health <= 0) 
        {
            if (!exploded)
            {
                if (Explotion != null)
                {
                    Explotion.Play();
                    aS.Stop();
                    aS.PlayOneShot(explotionSound);
                }
                var pingpong = GetComponent<PingPong>();
                if(pingpong != null)
                {
                    pingpong.enabled = false;
                }
                ScoreManagement.instance.AddScore(score);
                exploded = true;
                smoke = true;
            }
            rb.useGravity = enabled;
            if (Time.timeScale == 1)
            {
                transform.Rotate(1f, 1f, 0f);
            }
        }
        Invoke("SmokeParticle", 1f);
    }
    void SmokeParticle()
    {
        if (exploded && smoke)
        {
            if (FireSmoke != null)
            {
                FireSmoke.Play();
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        var player = other.transform.parent.gameObject.transform.parent.gameObject;
        if(player.tag == "Player")
        {
            health--;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (FireSmoke != null)
            {
                FireSmoke.Stop();
            }
            smoke = false;
            if (GroundExplotion != null)
            {
                if (!isGrounded)
                {  //GroundExplotion.Play();
                    Instantiate(GroundExplotion, transform.position, Quaternion.identity);
                }
            }
            isGrounded = true;
            mR.enabled = false;
            Invoke("Destroing", 3f);
        }
    }

    private void Destroing()
    {
        Destroy(gameObject);
    }
}
