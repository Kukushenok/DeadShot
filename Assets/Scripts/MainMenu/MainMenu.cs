using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIScoreDisplay scoreDisplay;
    public void OnPlayButtonPressed()
    {
        SceneLoadTransitions.LoadScene(SceneLoadTransitions.SCENE_GAMEPLAY);
    }
}
