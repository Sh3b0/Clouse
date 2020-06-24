using UnityEngine;

public class GameLevel : MonoBehaviour {

    public Transform PlayerSpawnPoint, CloudSpawnPoint;
    public Transform PlayerExitSpawnPoint, CloudExitSpawnPoint;

    // Moves player to the place that is actually beginning of level
    // This should be called after level was generated
    public void MovePlayerToTheStart(Transform player) {
        player.position = PlayerSpawnPoint.position;
    }
    
    public void MoveCloudToTheStart(Transform cloud) {
        cloud.position = CloudSpawnPoint.position;
    }
    
    public void MovePlayerToTheExit(Transform player) {
        player.position = PlayerExitSpawnPoint.position;
    }
    
    public void MoveCloudToTheExit(Transform cloud) {
        cloud.position = CloudExitSpawnPoint.position;
    }

}
