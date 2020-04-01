using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void quitGame()
    {
    	Debug.Log("QUIT");
    	//FindObjectOfType<PlayerMovement>().Done();
    	Application.Quit();
    }
}
