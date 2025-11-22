using System;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    [SerializeField] Mesh[] ships;
    [SerializeField] private HealthBarUI healthbar;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip hitEffectSFX;
    int key = 0;
    int colorCode = 0;
    int health = 0;
    MeshFilter mesh;
    MeshRenderer mR;
    int maxHealth = 0;
    bool isdesty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        int keys = ChangeVehicle.instance.key;
        Debug.Log(keys);
        PlayerPrefs.SetInt("key", keys);
        PlayerPrefs.Save();
        mR = GetComponent<MeshRenderer>();
        key = PlayerPrefs.GetInt("key");
        mesh = GetComponent<MeshFilter>();
        mesh.sharedMesh = ships[key];
        int colorCodes = ChangeVehicle.instance.colorCode;
        PlayerPrefs.SetInt("color", colorCodes);
        PlayerPrefs.Save();
        colorCode = PlayerPrefs.GetInt("color");
        Material[] materials = ChangeVehicle.instance.materials;
        mR.sharedMaterial = materials[colorCode];
        string[] healths = ChangeVehicle.instance.healths;
        health = Convert.ToInt32(healths[key]);
        maxHealth = health;
        healthbar.SetMaxHealth(maxHealth);
        PlayAudio();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isdesty)
        {
            GetComponent<AudioSource>().Stop();
            PlayerMovement.instance.Destry();
            isdesty = true;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        health = health - 5;
        if(hitEffect != null)
        {
            hitEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(hitEffectSFX);
        }
        healthbar.SetHealth(health);
    }
    private void OnCollisionEnter(Collision collision)
    {
        healthbar.SetHealth(0);
        PlayerMovement.instance.Destry();
    }

    void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }
    

}
