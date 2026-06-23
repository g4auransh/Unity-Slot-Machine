# 🎰 Unity Slot Machine

A simple and engaging Slot Machine game built in Unity as part of a technical assignment. The project demonstrates reel animations, random number generation (RNG), win detection, payouts, and clean object-oriented code architecture.

## 🎮 Game Overview

The game consists of three spinning reels containing multiple symbols. When the player presses the **SPIN** button, all reels spin with smooth animations and stop at randomly selected symbols.

### Winning Condition
A player wins when all visible reels land on the same symbol.

Examples:

✅ Cherry | Cherry | Cherry

✅ Seven | Seven | Seven

❌ Cherry | Bell | Cherry

When a winning combination occurs, the game plays a winning sound and reward gets added to the player's credits.


## ✨ Features

- Smooth reel spinning animations
- Randomized reel outcomes using RNG
- Win detection system
- Payout calculation
- Responsive UI
- Clean symbol alignment
- Object-Oriented Programming (OOP) structure
- WebGL build support
- Organized project architecture


## 🛠️ Technologies Used

- Unity
- C#
- Unity UI System
- WebGL


## 🎯 Implementation Approach

### Reel System

Each reel is controlled independently and generates a random symbol when the spin button is pressed.

### RNG System

Unity's built-in randomization system is used to generate fair and unpredictable outcomes for each reel.

### Win Detection

After all reels stop spinning, the game compares the final symbols:

```csharp
if(symbol1 == symbol2 && symbol2 == symbol3)
{
    // Player Wins
}
```

### Payout Logic

Each winning combination awards a predefined payout value.


## 🚀 Running the Project

### Unity Project

1. Clone the repository

git clone https://github.com/g4auransh/Unity-Slot-Machine.git

2. Open the project using Unity.

3. Open the main scene.

4. Press Play.


## 🌐 WebGL Build

Play the game directly in your browser:

**Game URL:**

https://g4auransh.github.io/Unity-Slot-Machine/

If the build is unavailable, clone the repository and build the project locally using Unity.


## 📸 Gameplay

1. Click on the amount you want to bet(10G,50G,100G).
2. Watch the reels spin.
3. Wait for all reels to stop.
4. Receive a payout if all symbols match.


## 🧠 Design Considerations

- Simple and intuitive UI
- Smooth gameplay experience
- Maintainable code architecture
- Separation of concerns between UI, game logic, and reel control
- Easy scalability for future features


## 🔮 Possible Future Improvements

- Wild symbols
- Scatter symbols
- Bonus rounds
- background music
- Coin balance system
- Adjustable betting system
- Multiple paylines
- Particle effects for wins



## 👨‍💻 Author

**Gauransh Maheshwari**

GitHub: https://github.com/g4auransh

Thank you for reviewing this project.
