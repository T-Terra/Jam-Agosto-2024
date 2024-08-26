using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGameManager : MonoBehaviour
{
    public GameObject InitObj;
    public GameObject IniObjGameplay;
    public GameObject GameOver;

    public GameObject Player;
    public GameObject Spawn_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.activeSelf == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
            }
        } else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                InitObj.SetActive(false);
                IniObjGameplay.SetActive(true);
                Player.SetActive(true);
                Spawn_.SetActive(true);
            }
        }
        
    }
}
