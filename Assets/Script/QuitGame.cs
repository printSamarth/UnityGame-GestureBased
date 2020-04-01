
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quitGame()
    {
    	Debug.Log("QUIT");
    	//FindObjectOfType<PlayerMovement>().Done();
    	Application.Quit();
    }
}