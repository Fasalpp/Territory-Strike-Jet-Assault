using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explotion;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject muzzle;
    public int health = 3;
    bool isExploded = false;
    private Transform playerPosition;
    public float rotationSpeed = 3f;
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(health <= 0)
        {
            Destoy();
        }
        ShootAtPlayer();
    }
    private void OnParticleCollision(GameObject other)
    {
        var player = other.transform.parent.gameObject.transform.parent.gameObject;
        if (player.tag == "Player")
        {
            health--;
            Debug.Log("Health :" + health);
        }
    }
    void ShootAtPlayer()
    {
        if (playerPosition != null)
        {
            Vector3 direction = playerPosition.position - turret.transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    void Destoy()
    {
        if (!isExploded)
        {
            explotion.Play();
            ScoreManagement.instance.AddScore(30);
            isExploded = true;
        }
        Destroy(gameObject);

    }
}
