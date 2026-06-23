using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotMachineController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text creditsText;
    public TMP_Text messageText; 
    
    [Header("Lever Animation")]
    public Image leverImage; 
    public Sprite leverUpSprite;   
    public Sprite leverDownSprite; 

    [Header("Player Stats")]
    public int currentCredits = 1000;
    private int currentBet = 0;

    [Header("Reels Setup")]
    public Image[] reel1; 
    public Image[] reel2; 
    public Image[] reel3; 

    [Header("Symbols")]
    public Sprite[] possibleSymbols; 

    [Header("Audio")] 
    public AudioSource audioPlayer;
    public AudioClip scrollingSound; 
    public AudioClip winSound;       

    [Header("Visual Effects")] // --- NEW PARTICLES SECTION ---
    public ParticleSystem jackpotParticles;

    private bool isSpinning = false; 

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
        if (isSpinning) return;

        if (currentCredits >= betAmount)
        {
            currentBet = betAmount;
            currentCredits -= betAmount;
            UpdateUI();
            
            if (messageText != null) messageText.text = "Spinning...";
            
            // --- START SCROLLING SOUND ---
            if (audioPlayer != null && scrollingSound != null)
            {
                audioPlayer.clip = scrollingSound;
                audioPlayer.loop = true; // Make it play on repeat
                audioPlayer.Play();
            }
            
            StartCoroutine(PullLeverRoutine());
            StartCoroutine(SpinReelsRoutine()); 
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

    IEnumerator SpinReelsRoutine()
    {
        isSpinning = true;

        float elapsedTime = 0f;
        float swapSpeed = 0.02f;   

        // Spin all 3 reels
        while (elapsedTime < 1.0f)
        {
            RandomizeReel(reel1);
            RandomizeReel(reel2);
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // Reel 1 stops
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            RandomizeReel(reel2);
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // Reel 2 stops
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            RandomizeReel(reel3);
            elapsedTime += swapSpeed;
            yield return new WaitForSeconds(swapSpeed);
        }

        // --- STOP SCROLLING SOUND ---
        if (audioPlayer != null)
        {
            audioPlayer.Stop();
            audioPlayer.loop = false; // Turn off looping just in case
        }

        CheckWin();
        isSpinning = false;
    }

    void RandomizeReel(Image[] reelSlots)
    {
        reelSlots[2].sprite = reelSlots[1].sprite;
        reelSlots[1].sprite = reelSlots[0].sprite;
        int randomIndex = Random.Range(0, possibleSymbols.Length);
        reelSlots[0].sprite = possibleSymbols[randomIndex];
    }

    void CheckWin()
    {
        Sprite slot1 = reel1[1].sprite;
        Sprite slot2 = reel2[1].sprite;
        Sprite slot3 = reel3[1].sprite;

        if (slot1 == slot2 && slot2 == slot3)
        {
            // JACKPOT!
            int winnings = currentBet * 10;
            currentCredits += winnings;
            
            if (messageText != null) messageText.text = "JACKPOT! You won " + winnings + "G!";
            UpdateUI(); 
            
            // --- PLAY WIN SOUND ---
            if (audioPlayer != null && winSound != null)
            {
                audioPlayer.PlayOneShot(winSound);
            }

            // --- FIRE PARTICLES ---
            if (jackpotParticles != null)
            {
                jackpotParticles.Play();
            }
        }
        else
        {
            // Lose (No sound plays)
            if (messageText != null) messageText.text = "Try Again!";
        }
    }
}