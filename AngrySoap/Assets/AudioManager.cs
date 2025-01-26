using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public AudioClip ClickAudio;

    public AudioClip AttackAudio;
    public AudioClip DashAudio;
    public AudioClip ShieldAudio;
    public AudioClip DetonateAudio;
    public AudioClip FootStepsAudio;


    public AudioClip EnemyJumpAudio;
    public AudioClip EnemyDeathAudio;
    public AudioClip EnemyStunAudio;


    public AudioClip WinAudio;
    public AudioClip LoseAudio;



    public AudioClip BackGroundSound;

    [SerializeField]
    private AudioSource audioSource;


    public void PlayAttack(){
        audioSource.PlayOneShot(AttackAudio);
    }

    public void PlayDash(){
        audioSource.PlayOneShot(DashAudio);
    }

    public void PlayShield(){
        audioSource.PlayOneShot(ShieldAudio);
    }

    public void PlayDetonate(){
        audioSource.PlayOneShot(DetonateAudio);
    }

    public AudioClip PlayFootSteps(){
        audioSource.PlayOneShot(FootStepsAudio, 0.2f);
        return FootStepsAudio;
    }

    public void StopFootSteps(){
        audioSource.Stop();
    }

    public void PlayClick(){
        audioSource.PlayOneShot(ClickAudio);
    }

    public void PlayAtPosition(AudioClip clip, Vector3 position){
        AudioSource.PlayClipAtPoint(clip, position);
    }

    public void PlayEnemyJump(Vector3 position){
        AudioSource.PlayClipAtPoint(EnemyJumpAudio, position);
    }

    public void PlayEnemyDeath(Vector3 position){
        AudioSource.PlayClipAtPoint(EnemyDeathAudio, position);
    }

    public void PlayEnemyStun(Vector3 position){
        AudioSource.PlayClipAtPoint(EnemyStunAudio, position, 1.5f);
    }

    public void PlayBackground(){
        audioSource.clip = BackGroundSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayWin(){
        audioSource.PlayOneShot(WinAudio);
    }

    public void PlayLose(){
        audioSource.PlayOneShot(LoseAudio);
    }

}
