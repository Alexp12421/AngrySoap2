using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilities : MonoBehaviour
{
    [Header("Bubble Shot Ability")]
    public Image BubbleShotImage;
    public Text BubbleShotText;
    public float BubbleShotCooldown = 0.5f;

    private bool isBubbleShotCooldown = false;

    private float currentBubbleShotCooldown;


    [Header("Bubble Armor Ability")]
    public Image BubbleArmorImage;
    public Text BubbleArmorText;
    public float BubbleArmorCooldown = 1;

    private bool isBubbleArmorCooldown = false;

    private float currentBubbleArmorCooldown;


    [Header("Bubble Detonate Ability")]
    public Image BubbleDetonateImage;
    public Text BubbleDetonateText;
    public float BubbleDetonateCooldown = 5;

    private bool isBubbleDetonateCooldown = false;

    private float currentBubbleDetonateCooldown;


    [Header("Douche Dash Ability")]
    public Image DoucheDashImage;
    public Text DoucheDashText;
    public float DoucheDashCooldown = 7;

    private bool isDoucheDashCooldown = false;

    private float currentDoucheDashCooldown;


   
    void Start()
    {
        BubbleShotImage.fillAmount = 0;
        BubbleArmorImage.fillAmount = 0;
        BubbleDetonateImage.fillAmount = 0;
        DoucheDashImage.fillAmount = 0;

        BubbleShotText.text = "";
        BubbleArmorText.text = "";
        BubbleDetonateText.text = "";
        DoucheDashText.text = "";
        
    }

    
    void Update()
    {
        BubbleShotInput();
        BubbleArmorInput();
        BubbleDetonateInput();
        DoucheDashInput();

        AbilityCooldown(ref currentBubbleShotCooldown, BubbleShotCooldown, ref isBubbleShotCooldown, BubbleShotImage, BubbleShotText);
        AbilityCooldown(ref currentBubbleArmorCooldown, BubbleArmorCooldown, ref isBubbleArmorCooldown, BubbleArmorImage, BubbleArmorText);
        AbilityCooldown(ref currentBubbleDetonateCooldown, BubbleDetonateCooldown, ref isBubbleDetonateCooldown, BubbleDetonateImage, BubbleDetonateText);
        AbilityCooldown(ref currentDoucheDashCooldown, DoucheDashCooldown, ref isDoucheDashCooldown, DoucheDashImage, DoucheDashText);
        
    }

    private void BubbleShotInput()
    {
        if( Input.GetMouseButtonDown(0) && !isBubbleShotCooldown )
        {
            isBubbleShotCooldown = true;
            currentBubbleShotCooldown = BubbleShotCooldown;
        }
    }

     private void BubbleArmorInput()
    {
        if( Input.GetKeyDown(KeyCode.E) && !isBubbleArmorCooldown )
        {
            isBubbleArmorCooldown = true;
            currentBubbleArmorCooldown = BubbleArmorCooldown;
        }
    }
    private void BubbleDetonateInput()
    {
        if( Input.GetMouseButtonDown(1) && !isBubbleDetonateCooldown )
        {
            isBubbleDetonateCooldown = true;
            currentBubbleDetonateCooldown = BubbleDetonateCooldown;
        }
    }
    
    private void DoucheDashInput()
    {
        if( ( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) )  && !isDoucheDashCooldown )
        {
            isDoucheDashCooldown = true;
            currentDoucheDashCooldown = DoucheDashCooldown;
        }
    }

    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, Text skillText)
    {
        if(isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if(currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if(skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }

                if(skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if(skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }

                if(skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }
}
