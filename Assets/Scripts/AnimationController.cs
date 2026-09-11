using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if (Mouse.current.leftButton.wasPressedThisFrame){
            animator.SetTrigger("CanExit");
        }
    }

    public void DesableObject(){
        gameObject.SetActive(false);
    }
}