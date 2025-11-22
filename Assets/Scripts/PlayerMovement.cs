
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Settings")]

    [Tooltip("How Fast the Plane moves up and down based upone player input")]
    [SerializeField] float controlSpeed = 0f;
    [Tooltip("How far the Plane should move Horizontelly")]
    [SerializeField] float clampXRange = 0f;
    [Tooltip("How far the Plane should move Vertically")]
    [SerializeField] float clampYRange = 0f;

    [Tooltip("Screen Position Tuning")]
    [SerializeField] float positionPitch = 0f;
    [SerializeField] float positionyaw = 0f;

    [Tooltip("Player Input Position")]
    [SerializeField] float controlPitch = 0f;
    [SerializeField] float controlRoll = 0f;


    [Tooltip("Laser File")]
    [SerializeField] GameObject[] laserse;
    
    [SerializeField] GameObject boss;
    [SerializeField] GameObject pauseManu;
    [SerializeField] GameObject tryAgainCanvas;
    [SerializeField] TextMeshProUGUI endText;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject explotionFX;
    float xValue, yValue;
    public static PlayerMovement instance;
    GameObject cameras;
    bool isdestroyd;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        cameras = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (!isdestroyd)
        {
            Movement();
            Rotation();
            Shooting();
            PauseMenu();
        }
            if (boss == null)
            {
                Finshed();
            }
            if (Time.timeScale == 0)
            {
                if (GetComponent<Camera>() != null)
                {
                    GetComponent<Camera>().GetComponent<AudioListener>().enabled = false;
                }
            }
            else
            {
                if (GetComponent<Camera>() != null)
                {
                    GetComponent<Camera>().GetComponent<AudioListener>().enabled = true;
                }
            }
        
    }

    void Movement()
    {
        xValue = Input.GetAxis("Horizontal");
        yValue = Input.GetAxis("Vertical");

        float xOffset = xValue * Time.deltaTime * controlSpeed;
        float yOffset = yValue * Time.deltaTime * controlSpeed;

        float realPos = transform.localPosition.x + xOffset;
        float yrealPos = transform.localPosition.y + yOffset;

        float clampxPos = Mathf.Clamp(realPos, -clampXRange, clampXRange);
        float clampyPos = Mathf.Clamp(yrealPos, -clampYRange, clampYRange);

        transform.localPosition = new Vector3(clampxPos, clampyPos, transform.localPosition.z);
    }
    void Rotation()
    {
        float pitch = transform.localPosition.y * positionPitch + yValue * controlPitch;
        float yaw = transform.localPosition.x * positionyaw;
        float roll = xValue * controlRoll;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void Shooting()
    {
        if (Input.GetButton("Fire1"))
        {
            ActivateLaser();
        }
        else
        {
            DeActivateLaser();
        }
    }

    void ActivateLaser()
    {
        //foreach(ParticleSystem laser in lasers)
        //{
        //    laser.Play();
        //}
        foreach (GameObject lase in laserse)
        {

            var emission = lase.GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            var audio = lase.GetComponent<AudioSource>();
            if(audio != null && !audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
    void DeActivateLaser()
    {
        //foreach (ParticleSystem laser in lasers)
        //{
        //    laser.Stop();
        //}
        foreach (GameObject lase in laserse)
        {
            var emission = lase.GetComponent<ParticleSystem>().emission;
            emission.enabled = false;
            var audio = lase.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Stop();
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            if(boss != null)boss.SetActive(true);
        }
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseManu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void Destry()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
        isdestroyd = true;
        Instantiate(explotionFX, transform.position, Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        DeActivateLaser();
        Invoke("DestoyCanvas", 2f);
    }
    void Finshed()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
        tryAgainCanvas.SetActive(true);
        endText.text = "Level Cleared";
        endText.color = Color.blue;
        panel.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        Time.timeScale = 0;
    }

    void DestoyCanvas()
    {
        tryAgainCanvas.SetActive(true);
        endText.text = "Destroyed";
        endText.color = Color.red;
        panel.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        Time.timeScale = 0;
    }
}
