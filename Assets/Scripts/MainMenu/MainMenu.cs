using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour
{
    private static int previousScore;
    [SerializeField] private GameObject scoreLabelParent;
    [SerializeField] private TextMeshProUGUI scoreLabelText;
    private void Awake()
    {
        if (previousScore > 0)
        {
            scoreLabelParent.gameObject.SetActive(true);
            scoreLabelText.text = previousScore.ToString();
        }
    }
    public void OnPlayButtonPressed()
    {
        SceneLoadTransitions.LoadScene(SceneLoadTransitions.SCENE_GAMEPLAY);
    }
    public void OnCloseButtonPressed()
    {
        Application.Quit();
    }
    public static void SubmitScore(int score)
    {
        previousScore = score;
    }
}
