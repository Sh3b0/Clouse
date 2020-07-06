using UnityEngine;

public class Corruption : MonoBehaviour {

    // Min scale is the current one
    public float MaxScale;
    public float ScaleIncreasePerSec, ScaleDecreasePerSec;
    public Enemy EnemyParent;

    private Transform _transform;
    private bool _increase, _decrease;
    
    private void Start() {
        _transform = transform;
        _increase = true;
        _decrease = false;
    }

    private void Update() {
        if (_increase) {
            _transform.localScale += new Vector3(ScaleIncreasePerSec * Time.deltaTime,
                ScaleIncreasePerSec * Time.deltaTime, 0);
            if (_transform.localScale.x >= MaxScale) _increase = false;
        }

        if (_decrease) {
            _transform.localScale -= new Vector3(ScaleDecreasePerSec * Time.deltaTime,
                ScaleDecreasePerSec * Time.deltaTime, 0);
            if (_transform.localScale.x <= ScaleDecreasePerSec * Time.deltaTime) {
                _decrease = false;
                EnemyParent.DecreasePower();
                Destroy(gameObject);
            }
        }
    }

    public void OnDisappear() {
        if (_decrease) return;
        _increase = false;
        _decrease = true;
    }

    // Player or cloud touched corruption - game ends
    public void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag(Constants.TAG_PLAYER) && !other.gameObject.CompareTag(Constants.TAG_CLOUD)) return;
        print("Hello :)");
        DeathScreen.Instance.OnDeath();
    }
}
