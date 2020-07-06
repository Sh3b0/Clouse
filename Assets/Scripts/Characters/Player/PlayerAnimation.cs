using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static bool dir = true; // direction of player, true: facing right
    
    public Animator CharAnimation;
    public Transform StrictRight, StrictLeft;
    public float MovementMinimalDetection = .01f;
    
    // Cached indecies (Move to constants)
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int OnJump = Animator.StringToHash("OnJump");
    private static readonly int PushSpeed = Animator.StringToHash("PushSpeed");
    
    public void Move(float horizontalAxis) {
        if (horizontalAxis >= MovementMinimalDetection) 
            MoveRight(horizontalAxis);
        else if (horizontalAxis <= -MovementMinimalDetection) 
            MoveLeft(Math.Abs(horizontalAxis));
        else
            CharAnimation.SetFloat(Speed, .0f);
    }
    
    public void Jump() {
        CharAnimation.SetTrigger(OnJump);
    }

    public void Push(float speed) {
        CharAnimation.SetFloat(PushSpeed, Math.Abs(speed));
    }
    
    private void MoveRight(float speed)
    {
        dir = true;
        transform.rotation = StrictRight.rotation;    
        CharAnimation.SetFloat(Speed, speed);        
    }
    
    private void MoveLeft(float speed) {
        dir = false;
        transform.rotation = StrictLeft.rotation;    
        CharAnimation.SetFloat(Speed, speed);
    }
    
}
