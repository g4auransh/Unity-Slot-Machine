using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; 

public class SlotMachineController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text creditsText;
    
    [Header("Lever Animation")]
    public Image leverImage; 
    public Sprite leverUpSprite;   
    public Sprite leverDownSprite; 

    [Header("Player Stats")]
    public int currentCredits = 1000;
    private int currentBet = 0;

    [Header("Reels Setup")]
    public Image[] reel1; // Will hold Top, Middle, Bottom
    public Image[] reel2; // Will hold Top, Middle, Bottom
    public Image[] reel3; // Will hold Top, Middle, Bottom

    [Header("Symbols")]
    public Sprite[] possibleSymbols; // Your 7, Cherry, Bell, and BAR

    private bool isSpinning = false; // Prevents button spamming!

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        creditsText.text = "Credits: " + currentCredits + "G";
    }

    public void AttemptSpin(int betAmount)
    {
        // If the machine is already spinning, ignore the click
        if (isSpinning) return;

        if (currentCredits >= betAmount)
        {
            currentBet = betAmount;
            currentCredits -= betAmount;
            UpdateUI();
            
            StartCoroutine(PullLeverRoutine());
            StartCoroutine(SpinReelsRoutine()); // Start the reels!
        }
        else
        {
            Debug.Log("Not enough credits!");
        }
    }

    public void SetBet10() { AttemptSpin(10); }
    public void SetBet50() { AttemptSpin(50); }
    public void SetBet100() { AttemptSpin(100); }

    IEnumerator PullLeverRoutine()
    {
        leverImage.sprite = leverDownSprite;      
        yield return new WaitForSeconds(0.3f);    
        leverImage.sprite = leverUpSprite;        
    }

    // The Magic Spinning Logic
    IEnumerator SpinReelsRoutine()
    {
        isSpinning = true;

        float spinDuration = 2.0f; // Spins for 2 seconds
        float elapsedTime = 0f;
        float swapSpeed = 0.05f;   // How fast the images swap (lower is faster)

        // Rapidly swap images to simulate spinning
        while (elapsedTime < spinDuration)
        {
            RandomizeReel(reel1);
            RandomizeReel(reel2);
            RandomizeReel(reel3);

            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // TODO: Later, we will add the code here to check if the player won!
        
        isSpinning = false;
    }

    // Helper function that randomly picks new images for a single reel
    void RandomizeReel(Image[] reelSlots)
    {
        for (int i = 0; i < reelSlots.Length; i++)
        {
            int randomIndex = Random.Range(0, possibleSymbols.Length);
            reelSlots[i].sprite = possibleSymbols[randomIndex];
        }
    }
}