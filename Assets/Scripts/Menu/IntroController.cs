using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

    public Animation CameraFly, Fade;
    public SpriteRenderer Background;
    public Text Text;
    public Button Next, Finish, Skip;
    
    private int _currentLineIndex;
    
    private void Start() {
        Next.onClick.AddListener(OnNextLine);
        Finish.onClick.AddListener(OnFinish);
        Skip.onClick.AddListener(OnFinish);
        
        Next.gameObject.SetActive(true);
        Skip.gameObject.SetActive(true);
        Finish.gameObject.SetActive(false);
        
        Background.sprite = Resources.Load<Sprite>(Constants.IntroBacks[0]);
        Text.text = Constants.IntroText[0];
        _currentLineIndex = 0;

        CameraFly.Play();
    }

    public void FadeMid() {
        _currentLineIndex++;
        
        Background.sprite = Resources.Load<Sprite>(Constants.IntroBacks[_currentLineIndex]);
        Text.text = Constants.IntroText[_currentLineIndex];
        CameraFly.Play();
        
        if (_currentLineIndex < Constants.IntroText.Length - 1) return;
        Next.gameObject.SetActive(false);
        Finish.gameObject.SetActive(true);
    }
    
    private void OnNextLine() {
        Fade.Play();
    }

    private void OnFinish() {
        SceneManager.LoadScene(Constants.SN_LEVEL);
    }
    
}
