using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public bool locked = true;

    int keyNum = 0;
    public string levelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!locked){
                SceneManager.LoadScene(levelToLoad);
            }
            else if(PublicVars.hasKey[keyNum]){
                PublicVars.hasKey[keyNum] = false;
                SceneManager.LoadScene(levelToLoad);
            }
            
        }
        //load victory scene
        if(SceneManager.GetActiveScene().name == "Level3"){
            if (other.gameObject.CompareTag("Player"))
            {
                if(!locked){
                    SceneManager.LoadScene(levelToLoad);
                }
                else if(PublicVars.hasKey[keyNum]){
                    PublicVars.hasKey[keyNum] = false;
                    SceneManager.LoadScene(levelToLoad);
                }
                
            }
        }
    }
}
