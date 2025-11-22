using UnityEngine;

public class Trigger : MonoBehaviour
{
    [HideInInspector] public bool isTrigered = false;
    [SerializeField] GameObject enemy;
    [SerializeField] ParticleSystem laser;
    [SerializeField] ParticleSystem explod;
    float rotationSpeed = 5f;
    private Transform playerPosition;
    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (isTrigered && laser != null)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            ShootAtPlayer();
        }
        if (explod != null && explod.isPlaying)
        {
            laser.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTrigered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (laser != null)
            {
                isTrigered = false;
                laser.Stop();
            }
        }
    }
    void ShootAtPlayer()
    {
        if (playerPosition != null && !explod.isPlaying)
        {
            Vector3 direction = playerPosition.position - enemy.transform.position;

            if (direction != Vector3.zero)
            {
                if (enemy != null)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    

}
