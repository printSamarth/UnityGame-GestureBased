using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevel;
    // Start is called before the first frame update
   public void EndGame()
    {
    	Debug.Log("end");
        //Call This function to stop client thread
        FindObjectOfType<PlayerMovement>().reset();
        //Invoke Restart function after 2 sec delay
    	Invoke("Restart",2f);
    }

    public void CompleteLevel()
    {
        // Enable the object so that we will see that screen
 		completeLevel.SetActive(true);

    }

    void Restart()
    {
        // Out scene will get loaded which will give option to restart or quit
    	SceneManager.LoadScene("Out");
    }
}
