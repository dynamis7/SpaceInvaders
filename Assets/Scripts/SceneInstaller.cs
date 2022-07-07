using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneInstaller : MonoBehaviour
{
    [SerializeField] private bool showHighScore = false;
    [ConditionalField("showHighScore"), SerializeField] private Text highScoreText = default;
	private void Awake()
	{
        if (showHighScore) highScoreText.text = "HIGH SCORE: " + GameData.PointsRecord;
    }
	public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
