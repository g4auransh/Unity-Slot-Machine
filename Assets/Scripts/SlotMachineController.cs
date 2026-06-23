using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text creditsText;
    public TMP_Text messageText; // For "JACKPOT!" or "Try Again!"
    
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
        if (messageText != null) messageText.text = "Place your bet!";
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
            
            if (messageText != null) messageText.text = "Spinning...";
            
            StartCoroutine(PullLeverRoutine());
            StartCoroutine(SpinReelsRoutine()); // Start the reels!
        }
        else
        {
            if (messageText != null) messageText.text = "Not enough credits!";
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

        float elapsedTime = 0f;
        float swapSpeed = 0.02f;   // Super fast smooth spinning!

        // Spin all 3 reels for 1 second
        while (elapsedTime < 1.0f)
        {
            RandomizeReel(reel1);
            RandomizeReel(reel2);
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // Reel 1 stops! Spin Reels 2 & 3 for another half second
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            RandomizeReel(reel2);
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // Reel 2 stops! Spin Reel 3 for the final half second
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // REELS HAVE STOPPED! Check if the player won:
        CheckWin();
        
        isSpinning = false;
    }

    // The Waterfall Effect: Shifts symbols down instead of just flashing them
    void RandomizeReel(Image[] reelSlots)
    {
        // 1. Move the Middle symbol down to the Bottom
        reelSlots[2].sprite = reelSlots[1].sprite;
        
        // 2. Move the Top symbol down to the Middle
        reelSlots[1].sprite = reelSlots[0].sprite;
        
        // 3. Spawn a brand new random symbol at the Top
        int randomIndex = Random.Range(0, possibleSymbols.Length);
        reelSlots[0].sprite = possibleSymbols[randomIndex];
    }

    // --- PAYOUT LOGIC ---
    void CheckWin()
    {
        // Grab the middle image (Index 1) from all three reels
        Sprite slot1 = reel1[1].sprite;
        Sprite slot2 = reel2[1].sprite;
        Sprite slot3 = reel3[1].sprite;

        // Do they all match?
        if (slot1 == slot2 && slot2 == slot3)
        {
            // JACKPOT! Calculate winnings (10x their bet)
            int winnings = currentBet * 10;
            currentCredits += winnings;
            
            if (messageText != null) messageText.text = "JACKPOT! You won " + winnings + "G!";
            UpdateUI(); // Update the credits display
        }
        else
        {
            // You lose!
            if (messageText != null) messageText.text = "Try Again!";
        }
    }
}