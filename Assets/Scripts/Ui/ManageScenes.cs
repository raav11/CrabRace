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

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void End()
    {
        SceneManager.LoadScene("End");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    private void OnTriggerEnter(Collider other)
    {
        //location will change this is temp for the prototype (likely)

        End();

    }
}
