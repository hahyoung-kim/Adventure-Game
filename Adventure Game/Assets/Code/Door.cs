using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public bool locked = true;

    int keyNum = 0;
    public string levelToLoad;
    bool canopen = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (levelToLoad == "level5" || levelToLoad == "victory") {
                if(!locked){
                SceneManager.LoadScene(levelToLoad);
            }
                for(int i = 0; i < 2; i++) {
                    print(PublicVars.hasKey[i]);
                    if(PublicVars.hasKey[i] == false){
                        canopen = false;
                    }
                }
                print(canopen);
                if (canopen) {
                    for(int i = 0; i < 2; i++) {
                        PublicVars.hasKey[i] = false;
                        SceneManager.LoadScene(levelToLoad);
                }
                }
            }
            else {
            if(!locked){
                SceneManager.LoadScene(levelToLoad);
            }
            else if(PublicVars.hasKey[keyNum]){
                PublicVars.hasKey[keyNum] = false;
                SceneManager.LoadScene(levelToLoad);
            }
            
        }
        }
}}
