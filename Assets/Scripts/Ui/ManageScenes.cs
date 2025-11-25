using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void Exit()
    {
        //Application.Quit();
        Debug.Log("game quit");
    }

    public void Level()
    {
        SceneManager.LoadScene("Level");
    }

    public void End()
    {
        SceneManager.LoadScene("End");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }

    private void OnTriggerEnter(Collider other)
    {
        //location will change this is temp for the prototype (likely)
       
        End();

    }
}
