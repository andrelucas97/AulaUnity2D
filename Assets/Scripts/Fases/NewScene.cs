using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public string levelToLoad;
    public bool wasLoaded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (collision.CompareTag("Player"))
        {
            // salvando estado atual de vida e nova cena

            wasLoaded = true;
            PlayerPrefs.SetInt("wasLoaded", 1);            
            PlayerPrefs.SetInt("Life", player.life);
            PlayerPrefs.SetString("LevelSaved", levelToLoad);

            // Chamando proxima cena

            SceneManager.LoadScene(levelToLoad);
        }
    }
}
