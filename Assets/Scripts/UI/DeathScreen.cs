using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour {

    public static DeathScreen Instance { get; private set; }
    public GameObject DeathPanel;

    private bool _dead;
    
    private void Start() {
        _dead = false;
        Instance = this;
    }

    public void OnRestart() {
        _dead = false;
        DeathPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CheckpointsManager.LoadCheckpoint();
    }

    public void OnExitToMenu() {
        _dead = false;
        SceneManager.LoadScene(Constants.SN_MENU);
    }
    
    public void OnDeath() {
        if (_dead) return;
        _dead = true;
        
        var fadeAnimation = GameLevel.CurrentLevelInstance.LoadingFadeAnimation;
        if (fadeAnimation) fadeAnimation.Play(Constants.ANIM_FADE_OUT);
        
        DeathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
