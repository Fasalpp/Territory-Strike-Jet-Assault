using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject engine1, engine2;
    [SerializeField] ParticleSystem explotion;
    [SerializeField] AudioClip explotionSFX;
    bool isExploded = false;
    Vector3 postion = new Vector3(-978, 121, -714);
    AudioSource aS;
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != postion) {
            transform.Translate(0f, -0.5f, 0f);
                }
        var eng1Rotaion = engine1.transform.rotation.x;
        var eng2Rotaion = engine2.transform.rotation.x;
        if(eng1Rotaion >= 0 && eng2Rotaion >= 0)
        {
            Explotion();
        }

    }

    void Explotion()
    {
        aS.Stop();
        if(explotion != null && !isExploded)
        {
            explotion.Play();
            aS.PlayOneShot(explotionSFX);
            isExploded = true;
        }
        GetComponent<Rigidbody>().useGravity = true;
        engine1.GetComponent<Rigidbody>().useGravity = true;
        engine2.GetComponent<Rigidbody>().useGravity = true;
        ScoreManagement.instance.AddScore(300);
        Invoke("Destry", 4f);
    }
    void Destry()
    {
        Destroy(gameObject);
    }
}
