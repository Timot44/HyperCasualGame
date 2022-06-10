using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public TextMeshProUGUI textToPulse;
	[SerializeField] private GameObject panelCredits;
	[SerializeField] private GameObject menuParent;
	[SerializeField] private string url;
	private void Start()
	{
		textToPulse.LeanAlphaTextMeshPro(0f, 1f).setFrom(1f).setLoopPingPong();
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void CreditsPanel()
	{
		panelCredits.SetActive(true);
		menuParent.SetActive(false);
	}

	public void BackToMenu()
	{
		panelCredits.SetActive(false);
		menuParent.SetActive(true);
	}
	
	public void OpenUrl()
	{
		Application.OpenURL(url);
	}
}
