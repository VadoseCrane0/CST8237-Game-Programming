using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(ChangeLevel());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeLevel() {
        GameObject menuController = GameObject.Find("MenuController");
        float time = menuController.GetComponent<FaderScript>().BeginFade(1);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
}
