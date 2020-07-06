using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {
    
    public static EndScreen Instance { get; private set; }
    public GameObject EndPanel;

    private bool _end;
    
    private void Start() {
        _end = false;
        Instance = this;
    }

    public void OnExitToMenu() {
        _end = false;
        SceneManager.LoadScene(Constants.SN_MENU);
    }
    
    public void OnEnd() {
        if (_end) return;
        _end = true;
        
        var fadeAnimation = GameLevel.CurrentLevelInstance.LoadingFadeAnimation;
        if (fadeAnimation) fadeAnimation.Play(Constants.ANIM_FADE_OUT);
        
        EndPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
