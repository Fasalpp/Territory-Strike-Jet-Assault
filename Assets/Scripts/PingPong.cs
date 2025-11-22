using UnityEngine;

public class PingPong : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 objectMovement;
    [SerializeField] float speed = 5;
    [SerializeField] float distance = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, distance);
        Vector3 offSet = objectMovement * pingPong;
        transform.position = startingPosition + offSet;
    }

}
