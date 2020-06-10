using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject LevelsButtons;
    public Animation[] OpeningAnimations;
    
    private static readonly Dictionary<int, string> Levels = new Dictionary<int, string>() {
        { 0, "Test" }
    };

    private void Start() {
        LevelsButtons.SetActive(false);
    }

    public void OnStartClick() {
        foreach (var anim in OpeningAnimations) {
            anim.Play();
        }
        LevelsButtons.SetActive(true);
    }

    public void OnLevelClick(int levelNum) {
        SceneManager.LoadScene(Levels[levelNum]);
    }
    
    public void OnExitClick() {
        Application.Quit(0);
    }
    
}
