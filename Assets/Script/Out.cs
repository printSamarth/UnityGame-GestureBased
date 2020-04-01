using UnityEngine.SceneManagement;
using UnityEngine;

public class Out : MonoBehaviour
{ 
	// Restarting Game
	
    public void outGame()
    {
    	SceneManager.LoadScene("Level01");
    }
}
