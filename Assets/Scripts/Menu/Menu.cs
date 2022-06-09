using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public TextMeshProUGUI textToPulse;
	
	private void Start()
	{
		textToPulse.LeanAlphaTextMeshPro(0f, 1f).setFrom(1f).setLoopPingPong();
	}

	public void Play()
	{
		Handheld.Vibrate();
		SceneManager.LoadScene(1);
	}
}
