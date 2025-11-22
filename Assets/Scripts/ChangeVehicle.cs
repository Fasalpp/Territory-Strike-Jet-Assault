using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeVehicle : MonoBehaviour
{
    [HideInInspector] public int key = 0;
    public int colorCode = 0;
    [SerializeField] Mesh[] ships;
    [SerializeField] public Material[] materials;
    [SerializeField] GameObject[] colors;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI healthVal;
    [SerializeField] public string[] healths;
    public static ChangeVehicle instance;
    int previousColor = 0;
    MeshFilter mesh;
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
        mesh = player.GetComponent<MeshFilter>();
    }
    void Start()
    {
        mesh.sharedMesh = ships[key];
        healthVal.text = healths[key];
    }
    public void NextShip()
    {
        key++;
        if (key >= ships.Length)
        {
            key = 0;
        }
        mesh.sharedMesh = ships[key];
        healthVal.text = healths[key].ToString();

    }
    public void PreviousShip()
    {
        key--;
        if (key < 0)
        {
            key = ships.Length - 1;
        }
        mesh.sharedMesh = ships[key];
        healthVal.text = healths[key].ToString();

    }
    public void ChangeShip()
    {
        SceneManager.LoadScene(2);
    }
    public void ChangeColor()
    {
        
        GameObject newcolor = colors[colorCode];
        GameObject oldcolor = colors[previousColor];
        oldcolor.SetActive(false);
        newcolor.SetActive(true);
        player.GetComponent<MeshRenderer>().sharedMaterial = materials[colorCode];
    }
    public void Next()
    {
        previousColor = colorCode;
        colorCode++;
        if (colorCode >= colors.Length)
        {
            colorCode = 0;
        }
        ChangeColor();
    }
    public void Previous()
    {
        previousColor = colorCode;
        colorCode--;
        if (colorCode < 0)
        {
            colorCode = colors.Length - 1;
        }
        ChangeColor();
    }
}
