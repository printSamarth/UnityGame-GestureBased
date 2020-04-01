using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
    	int l = SceneManager.GetActiveScene().buildIndex + 1;
    	if( l < 5)
    	{
    		SceneManager.LoadScene(l);
    	}
    }
}    
