using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSoapArmour : MonoBehaviour
{

    [SerializeField]
    private GameObject BubbleArmour;

    [SerializeField]
    private GameObject BubbleTrail;

    [SerializeField]
    private PlayerWater playerWater;

    [SerializeField]
    private float CoolDown = 1f;

    [SerializeField]
    private int ArmourCost = 1;

    [SerializeField]
    private float ConsumeWaterInterval = 0.5f;

    [SerializeField]
    private float trailBubbleInterval = 0.25f;

    [SerializeField]
    private bool canToggleArmour = true;

    [SerializeField]
    private bool isArmourActive = false;
    
    private Vector3 armourTrailStart;

    private Coroutine bubbleTrailCoroutine;

    private void Start() {
        playerWater = GetComponent<PlayerWater>();
    }

    public void OnToggleArmour(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canToggleArmour)
            {
                if (isArmourActive)
                {
                    isArmourActive = false;
                    canToggleArmour = false;
                    DisableArmour();
                    StartCoroutine(ArmourCoolDown());
                }
                else
                {
                    if (playerWater.ConsumeWater(ArmourCost))
                    {
                        armourTrailStart = transform.position;
                        isArmourActive = true;
                        canToggleArmour = false;
                        EnableArmour();
                        StartCoroutine(ArmourCoolDown());
                    }
                    else
                    {
                        // Optionally, you can add feedback to the player here
                        Debug.Log("Not enough water to activate armour.");
                    }
                }
            }
        }
    }

    public IEnumerator ArmourCoolDown()
    {
        yield return new WaitForSeconds(CoolDown);
        canToggleArmour = true;
    }

    public void DisableArmour()
    {
        BubbleArmour.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().excludeLayers = 0;
        isArmourActive = false;
        if (bubbleTrailCoroutine != null)
        {
            StopCoroutine(bubbleTrailCoroutine);
            bubbleTrailCoroutine = null;
        }
    }

    public void EnableArmour()
    {
        if (playerWater.ConsumeWater(ArmourCost))
        {
            BubbleArmour.SetActive(true);
            gameObject.GetComponent<CapsuleCollider>().excludeLayers = LayerMask.GetMask("Enemy");
            bubbleTrailCoroutine = StartCoroutine(SpawnBubbleTrail());
            StartCoroutine(ConsumeWaterWhileActive()); 
        }
    }

    private IEnumerator ConsumeWaterWhileActive()
    {
        while (isArmourActive && playerWater.ConsumeWater(ArmourCost))
        {
            yield return new WaitForSeconds(ConsumeWaterInterval); // Adjust the interval as needed
        }
        DisableArmour();
    }

    private IEnumerator SpawnBubbleTrail()
    {
        while (isArmourActive)
        {
            AddTrail();
            yield return new WaitForSeconds(trailBubbleInterval); // Adjust the interval as needed
        }
    }

    private void AddTrail()
    {
        float minDistance = 0.2f; // Minimum distance to spawn a new bubble
        float maxDistance = 1f; // Maximum distance to spawn a new bubble
        float bubbleY = armourTrailStart.y; // Fixed Y coordinate for bubbles

        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(armourTrailStart, currentPosition);

        if (distance >= minDistance)
        {
            Vector3 spawnPosition = new(
                currentPosition.x + Random.Range(-maxDistance, maxDistance),
                0f,
                currentPosition.z + Random.Range(-maxDistance, maxDistance)
            );

            GameObject bubble = Instantiate(BubbleTrail, spawnPosition, Quaternion.identity);
            bubble.AddComponent<FloorBubble>();
            float randomScale = Random.Range(0.4f, 0.6f);
            bubble.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            armourTrailStart = currentPosition;
        }
    }

}
