using UnityEngine;

public class BossEngine : MonoBehaviour
{
    int health = 30;
    bool isEngineDam = false;
    bool isexploded = false;
    bool isdamage = false;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] AudioClip explotionSFX;
    [SerializeField] GameObject damageEngine;
    AudioSource aS;
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (health <= 0)
        {
            if (!isexploded && explotion != null)
            {
                explotion.Play();
                aS.PlayOneShot(explotionSFX);
                isexploded = true;
            }
            if (!isEngineDam) transform.Rotate(0.5f, 0f, 0f);
            if (transform.rotation.x >= 0f)
            {
                isEngineDam = true;
            }
        }
        if (isdamage) Invoke("DamageEngine", 0.5f);
    }
    private void OnParticleCollision(GameObject other)
    {
        var player = other.transform.parent.gameObject.transform.parent.gameObject;
        Debug.Log(player.tag);
        if (player.tag == "Player")
        {
            health--;
            if (!isdamage)
            {
                damageEngine.SetActive(true);
                isdamage = true;
            }
        }
    }
    void DamageEngine()
    {
        damageEngine.SetActive(false);
        isdamage = false;
    }
}
