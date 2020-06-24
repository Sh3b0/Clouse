using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 _offset;
    public static bool cloudVisible = true, cameraActive = true;
    public static Camera me;
    public Animation anim;
    private void Start()
    {
        me = Camera.main;
        _offset = new Vector3(0, 0, transform.position.z);
    }

    private void Update()
    {
        if (!cameraActive) return;
        target = Player.playerActive ? Player.me : Cloud.me;
        Vector3 pos = me.WorldToViewportPoint(Cloud.me.position);
        cloudVisible = pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1 && pos.z > 0;

        if (Player.playerActive && !anim.isPlaying)
        {
            if (Input.GetAxis("Vertical") < 0)
                anim.Play("PeekDown");
            
            else if (Input.GetAxis("Vertical") > 0)
                anim.Play("PeekUp");
        }
    }

    private void LateUpdate()
    {
        if (!cameraActive) return;
        Vector3 camPos = transform.position;
        Vector3 newPos = Vector3.Lerp(camPos, target.position + _offset, 0.01f);
        newPos.y = camPos.y; // Fix y position of Camera.
        transform.position = newPos;
    }
}