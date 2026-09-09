using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame){
            animator.SetTrigger("CanExit");
        }   
    }

    public void DesableObject(){
        gameObject.SetActive(false);
    }
}
