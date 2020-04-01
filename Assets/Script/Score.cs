using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
	public Text scoreText;
    public Transform player; 
    float t ;
    // Update is called once per frame
    void Update()
    {
    	t = player.position.z;
    	t = t + 7;
      scoreText.text  = t.ToString("0") ;
    }
}
