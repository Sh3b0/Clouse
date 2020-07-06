using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform LeftUpSpawnBorder, RightBottomSpawnBorder;
    public Transform CorruptionOrigin;
    public Corruption CorruptionExample;
    public float CorruptionSpawnInterval, CorruptionSpreadIncrease;
    
    public AudioSource HitAudio;
    public AudioClip EnemyDeath, PowerDecrease;

    private float _timeCounter;
    private int _currentMultiplier, _currentRadiusSpawnGap, _spawnedOnThisRadius;
    private float _left, _right, _up, _bottom;
    private Corruption[] _corruptions;
    
    // TODO Consider creating new corruption in such way (think of it more)
    // TODO that their center would be closer to enemy than distance to each light ray is
    // Mapping from ray origin to distance to that ray
    // private Dictionary<Vector3, float> _distanceToRay = new Dictionary<Vector3, float>();
    
    private void Start() {
        _currentRadiusSpawnGap = 1;
        _spawnedOnThisRadius = 0;
        _currentMultiplier = 0;
        _timeCounter = 0.0f;

        var leftUp = LeftUpSpawnBorder.position;
        var rightBottom = RightBottomSpawnBorder.position;
        _left = leftUp.x;
        _right = rightBottom.x;
        _up = leftUp.y;
        _bottom = rightBottom.y;
    }

    public void Update() {
        _timeCounter += Time.deltaTime;

        // Spawn corruption if it is the appropriate time
        if (_timeCounter >= CorruptionSpawnInterval) {
            _timeCounter = 0.0f;
            SpawnCorruption();
        }
    }

    public void OnLightHit() {
        // If enemy has no corruptions left and being lighted - he dies
        if (_currentRadiusSpawnGap <= 1) {
            HitAudio.clip = EnemyDeath;
            HitAudio.Play();
            
            Destroy(gameObject);
        }
    }

    public void DecreasePower() {
        HitAudio.clip = PowerDecrease;
        HitAudio.Play();
        
        if (_spawnedOnThisRadius > 0)
            _spawnedOnThisRadius--;
        else if (_currentRadiusSpawnGap > 1) {
            _currentRadiusSpawnGap--;
            _currentMultiplier--;
            _spawnedOnThisRadius = _currentRadiusSpawnGap - 1;
        }
    }

    private void SpawnCorruption() {
        var newCorruption = Instantiate(CorruptionExample, CorruptionOrigin);
        newCorruption.EnemyParent = this;

        var interval = CorruptionSpreadIncrease * _currentMultiplier / 2;
        var x = Random.Range(-interval > _left ? -interval : _left, interval < _right ? interval : _right);
        var y = Random.Range(-interval > _bottom ? -interval : _bottom, interval < _up ? interval : _up);
        newCorruption.transform.localPosition = new Vector3(x, y, 0);

        _spawnedOnThisRadius++;
        if (_spawnedOnThisRadius >= _currentRadiusSpawnGap) {
            _spawnedOnThisRadius = 0;
            _currentRadiusSpawnGap++;
            _currentMultiplier++;
        }
    }
    
}
