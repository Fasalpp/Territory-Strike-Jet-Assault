using UnityEngine;

public class BossDestroy : MonoBehaviour
{
    [SerializeField] ParticleSystem explotion;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Instantiate(explotion, transform.position, Quaternion.identity);
            Invoke("Destroy", 2f);
        }
    }

    void Destroy()
    {
        Destroy(gameObject.transform.root.gameObject);
    }

    
}
