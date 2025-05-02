using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenu : MonoBehaviour
{
            
        


    private void Awake()
    {
        Debug.Log("HataBuradamı4");
        if (PlayerPrefs.HasKey("SonBolum"))
        {
            Debug.Log("HataBuradamı1");
            PlayerPrefs.SetInt("SonBolum", 1);
            Debug.Log("HataBuradamı2");

        }
            Debug.Log("HataBuradamı3");

        SceneManager.LoadScene(PlayerPrefs.GetInt("SonBolum"));
        Debug.Log("HataBuradamı5");
    }
}
