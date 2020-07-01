using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

    public Collider TriggerCollider;
    public Animation FlagAnimation;
    
    private void Start() {
        if (CheckpointsManager.CheckpointsVisited.Contains(GameLevel.CurrentLevelInstance.LevelIndex)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag(Constants.TAG_PLAYER)) return;
        GameLevel.CurrentLevelInstance.SaveLevelState(false);
        CheckpointsManager.CreateCheckpoint();
        CheckpointsManager.CheckpointsVisited.Add(GameLevel.CurrentLevelInstance.LevelIndex);
        TriggerCollider.enabled = false;
        FlagAnimation.Play();
    }
    
}
