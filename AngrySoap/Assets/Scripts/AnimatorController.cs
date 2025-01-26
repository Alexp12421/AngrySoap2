using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    
    [SerializeField]
    public SerializedDictionary<string, float> animationClipLengths;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        animationClipLengths = new SerializedDictionary<string, float>();
        foreach (AnimationClip clip in clips)
        {
            animationClipLengths[clip.name] = clip.length;
        }
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     bool isRunning = (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"));
    //     bool isDashing = Input.GetKey("left shift");

    //     if (isRunning)
    //     {
    //         animator.SetBool("isRunning", true);
    //     }

    //     if(!isRunning)
    //     {
    //         animator.SetBool("isRunning", false);
    //     }

    //     if(isRunning && isDashing)
    //     {
    //         animator.SetBool("isDashing", true);
    //     }

    //     if(!isRunning && !isDashing)
    //     {
    //         animator.SetBool("isDashing", false);
    //     }

    //     if (isRunning && !isDashing)
    //     {
    //         animator.SetBool("isDashing", false);
    //     }

    //     if (!isRunning && isDashing)
    //     {
    //         animator.SetBool("isDashing", false);
    //     }

    //     if(Input.GetKey("q"))
    //     {
    //         animator.SetBool("isShield", true);
    //     }

    //     if(!Input.GetKey("q"))
    //     {
    //         animator.SetBool("isShield", false);
    //     }

    //     if(Input.GetKey("e"))
    //     {
    //         animator.SetBool("isDetonate", true);
    //     }

    //     if(!Input.GetKey("e"))
    //     {
    //         animator.SetBool("isDetonate", false);
    //     }

    //     if(Input.GetKey("z"))
    //     {
    //         animator.SetBool("isDead", true);
    //     }
    // }

    public void startRunning()
    {
        animator.SetBool("isRunning", true);
    }

    public void stopRunning() 
    {
        animator.SetBool("isRunning", false);
    }

    public void startDashing()
    {
        animator.SetBool("isDashing", true);
    }

    public void stopDashing()
    {
        animator.SetBool("isDashing", false);
    }

    public void startShield()
    {
        animator.SetBool("isShield", true);
    }

    public void stopShield()
    {
        animator.SetBool("isShield", false);
    }

    public void startDetonate()
    {
        animator.SetBool("isDetonate", true);
    }

    public void stopDetonate()
    {
        animator.SetBool("isDetonate", false);
    }

    public void isDead()
    {
        animator.SetBool("isDead", true);
    }

    public void isNotDead()
    {
        animator.SetBool("isDead", false);
    }

    public void startAttack()
    {
        animator.SetBool("isAttack", true);
    }

    public void stopAttack()
    {
        animator.SetBool("isAttack", false);
    }

    public void startDancingGangnamStyle()
    {
        animator.SetBool("isDancingGangnamStyle", true);
    }

    public void stopDancingGangnamStyle()
    {
        animator.SetBool("isDancingGangnamStyle", false);
    }

    public void startDancingShakeItOff()
    {
        animator.SetBool("isDancingShakeItOff", true);
    }

    public void stopDancingShakeItOff()
    {
        animator.SetBool("isDancingShakeItOff", false);
    }

    public void startDancingOMG()
    {
        animator.SetBool("isDancingOMG", true);
    }

    public void stopDancingOMG()
    {
        animator.SetBool("isDancingOMG", false);
    }
}