using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI yarnCollectibles;

    int score = 0;
    private void Awake() {
        // Don't Destroy on Load
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        yarnCollectibles.text = ": " + score;
        GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>().text = ": " + score;
    }
    public void resetScore(){
        score = 0;
    }
    public int getScore(){
        return score;
    }
    public void AddYarn() {
        score += 1;
        GameObject.FindGameObjectWithTag("score").GetComponent<TextMeshProUGUI>().text = ": " + score;
    }
    // Update is called once per frame
    void Update()
    {
        
#if !UNITY_WEBGL
        // Esc to Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
// #if !UNITY_WEBGL
//         // Esc to Exit
//         if(Input.GetKeyDown(KeyCode.Escape))
//         {
//             SceneManager.LoadScene("StartPage");
//         }
// #endif
    }
}
