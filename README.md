
# Buzz Out

You are Buzz, Georgia Tech's beloved mascot and you have been trapped on an unknown rival university's campus. You must explore the area to find a way out before it's too late! Careful, because the campus police are watching you.

  

## Contacts

* Kevin Chen | kchen357 | kchen357@gatech.edu

* Eric Le | eric8 | eric8@gatech.edu

* Chanhee Lee | clee643 | clee643@gatech.edu

* Siyuan (Ally) Liu | sliu402 | ally.liu@gatech.edu

* Benjamin Vargas | bvargas3 | ben.var@gatech.edu

  

## Controls

- Use the arrow keys or WASD keys to move your character

- Space or Left Click - Interact

- 1, 2, 3, 4 - Abilities from power ups

- Esc - Pause game

- i - Toggle inventory

- r - Reverse camera view

  

## Game Requirements

  

### 3D Game Feel:

- Game restarts in a clean state after game overs and game restarts.

- Game starts with explaining the background story, objective, and interactable elements of the game

- Music cues/sound effects with every interactable object

- Music and visual cues when the game enters the “end game” state

- Police will yell “Stop right there criminal scum” when they begin chasing Buzz

- Game is played in third person, in which the player will control and move Buzz around the campus

  

### Precursors to Fun Gameplay:

- This game is intended to be wacky and chaotic

- Ragdolls are meant to add comedic effects; directional vector force is applied to give slaps a bit of a weighty feeling

- Item effects, costs (from vending machine), and drop rate, were balanced with playtested feedback

- Players are encouraged to collect and use items wisely in certain situations in order to beat the game

- Consequences/cost to buying strong powerups involve setbacks to your overall game progress

- Gentle learning curve. Players are initially set to a wanted level of 0 such that the police will not pursue Buzz

- With higher wanted levels, more police spawn in, police will have increased chase speed, and police will have a larger search radius as they begin to more carefully search for Buzz

- Player is constantly encouraged to be on the move to either slap students (students will slowly begin to aggregate in locations away from Buzz) or by running from the police

  

### 3D Character with Real Time Control:

- Buzz can be moved with standard WASD movement

- Buzz comes with root motion enabled, which blends in smooth movement when transitioning from standing → walking → running

- Slap button is mapped to “fire1”. You can Left Mouse Click or space when in close proximity to a student to begin a slap animation.

- Slap animations are implemented with IK, this maps Buzz’s hand to the students’ face when the animation is played

- Slap animations are accompanied by a slap noise for auditory cues

- Police and students smoothly transition with blending from their walking animations to their running animations

  

### 3D World with Physical and Spatial Simulation:

- Custom built level design to generate a student campus-like feel

- Purposefully created to fit the goofy and playful vibe of the game

- Environmental physical interactions in the form of power ups located throughout campus that can be obtained as items from ragdolled students

- Minimal clipping and players are confined to the bounds of the map (fence)

-Donut: power up that can be dropped by the user that will cause the police to chase the donut instead of Buzz

- White Claw: power up that will cause students to be attracted to Buzz’s popularity

- Latte: power up that speeds up movement speed as a result of caffeine.

- Honey: Makes Buzz temporarily invincible to the police

- Audio cues and stimuli to help reinforce the physical interaction when power ups and bone fragments are picked up

- Auditory features when Buzz ragdolls a student to help drive home physical interactions

- Inverse kinematics is used to align Buzz’s punching animation with the head of the student he is about to ragdoll to aid with the spatial simulation and physical interactions of the game

- Consistent running speed tied to framerate

  

### Real Time NPC Steering/AI:

- Students will naturally path find to various locations around campus

- Natural wandering behavior for the police

- Nearby students will run away from Buzz if they witness him slapping a student

- Nearby police will investigate the crime scene (area that Buzz slapped a student)

- Police states (wandering, investigating, chasing, following)

- Student states (pathing, fleeing, following)

- Police and students have a smooth transition from their walking animations to running animations depending on their state

- Police will yell when they begin to chase Buzz

- Items directly interact with the behavior of the police and student AI

- White claw (causes all nearby vicinity to begin following Buzz)

- Donut (causes all police to begin following the donut over Buzz)

  

### Polish:


* The game contains a start menu on load, where you may start/exit the game.

* The game contains several other UI screens: opening dialog, “how to play” menu, character models and power-up introduction scene. These are included to facilitate and explain gameplay.

* The game has a pause menu (press ESC), where you may restart or quit the game.

* The aesthetic of each UI scene/component was meant to replicate the look and feel of the buildings/campus, in color and style. It is consistent throughout each menu and helps make the game feel more cohesive as a whole.

* Inventory system is displayed at the bottom left of the screen. This shows what power ups are obtainable, as well as whether you have the item (the item will become filled in). It also shows which button will activate the item. This is modular and easily expandable to new powerups.

* Bone collectible progress tracking bar (“Bone bar”) at the top right of the screen. This is to show the progress of the player, as the main goal of the game is to maximize the bone bar.

* Minimap UI at the bottom right of the screen. Minimap gives the player a holistic view of their surroundings and allows them to see the placement of students and police.

* “Wanted Level” count tracker at the top left of the screen. The wanted level of Buzz increases as he slaps students. The UI keeps track of his wanted level (higher the wanted level, faster the police).

* Interactable vending machines with custom UI. Press “space” next to the vending machine to activate; press again to exit UI anytime during usage. UI allows the player to click and buy power-ups from the vending machine using bones as currency.

* Students ragdoll with respect to the inverse kinematics of Buzz’s hand when he punches the students.

* The police chase Buzz when he is in range.

* There are unique sounds that activate for events, such as: powerup pickup, unique sounds when using each power up, sound for bone fragment pickup, and a unique sound for a student punch/slap.

* UI was designed to fit the feel of the campus environment in the game in both color scheme and design aesthetic. Consistent assets were used to make sure the graphics feel cohesive and from one artist.

  

## External Resources

* **Simple City pack plain** | 255 PIXEL STUDIOS | [https://assetstore.unity.com/packages/3d/environments/urban/simple-city-pack-plain-100348](https://assetstore.unity.com/packages/3d/environments/urban/simple-city-pack-plain-100348)

* **Windridge City** | UNITY TECHNOLOGIES | [https://assetstore.unity.com/packages/3d/environments/roadways/windridge-city-132222](https://assetstore.unity.com/packages/3d/environments/roadways/windridge-city-132222)

* **Free Trees** | DATH_ARTISAN | [https://assetstore.unity.com/packages/3d/vegetation/trees/free-trees-103208](https://assetstore.unity.com/packages/3d/vegetation/trees/free-trees-103208)

* **25 Mixed Industrial Sign Pack** | VOLUMETRIC GAMES | [https://assetstore.unity.com/packages/3d/props/25-mixed-industrial-sign-pack-73180](https://assetstore.unity.com/packages/3d/props/25-mixed-industrial-sign-pack-73180)

* **3D Game Props** | WISSAM EL HAJJ | [https://assetstore.unity.com/packages/3d/props/3d-game-props-61815](https://assetstore.unity.com/packages/3d/props/3d-game-props-61815)

* **Basic motions Free pack** | Kevin Iglesias | [https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271](https://assetstore.unity.com/packages/3d/animations/basic-motions-free-pack-154271)

* **Police Badge Modern** | TurboSquid, Inc. | [https://assetstore.unity.com/packages/3d/props/police-badge-modern-2-113737](https://assetstore.unity.com/packages/3d/props/police-badge-modern-2-113737)

* **Bull Dog Head** | [https://free3d.com/3d-model/bull-dog-65717.html](https://free3d.com/3d-model/bull-dog-65717.html)

* **Police Baton** | [https://free3d.com/3d-model/police-baton-v1--317916.html](https://free3d.com/3d-model/police-baton-v1--317916.html)

* **Beverage FREE WEEK! Drinks and Bottles** | [GooPi(Misha)](https://assetstore.unity.com/publishers/38140) | [https://assetstore.unity.com/packages/3d/props/food/beverage-free-week-drinks-and-bottles-165228](https://assetstore.unity.com/packages/3d/props/food/beverage-free-week-drinks-and-bottles-165228)

* **3D Bakery Object** | Layer Lab | [https://assetstore.unity.com/packages/3d/props/food/3d-bakery-object-17167](https://assetstore.unity.com/packages/3d/props/food/3d-bakery-object-17167)

* **FREE Casual Game SFX Pack** | Dustyroom | [https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116](https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116)

* **Kerning City Music** | Nexon Inc. | [https://www.youtube.com/watch?v=mD2b0Xx7wPA](https://www.youtube.com/watch?v=mD2b0Xx7wPA)

* **SHOTS** | LMFAO [https://www.youtube.com/watch?v=h-VTua1tZQs](https://www.youtube.com/watch?v=h-VTua1tZQs)

* **Winnie The Pooh Theme Song** | Disney Studios Chorus [https://www.youtube.com/watch?v=V4IOrH_FMIs](https://www.youtube.com/watch?v=V4IOrH_FMIs)

* **Hennesys Music** | Nexon Inc. | [https://downloads.khinsider.com/game-soundtracks/album/maplestory](https://downloads.khinsider.com/game-soundtracks/album/maplestory)

* **God of War Music** | Sony | [https://downloads.khinsider.com/god-of-war](https://downloads.khinsider.com/god-of-war)

  
  

# Scenes to open

* Kevin_scene - Main game scene

  
  

# Work Credit

  

## Ally

* Menus: start, pause

* Menu scenes: opening dialog, win/lose scene, “how to play”

* In-game UI: bone-bar, minimap, wanted level tracker, inventory slots

* Vending machine UI, escape mode UI

* All logic and scripts associated with the above items (in Scripts>Menu)

  

## Eric

* Police and student controller scripts

* Police and student animation controllers

* Police and student models

* Preliminary AI pathing (Proof of concepts)

* Ragdoll effects/activation

* Student and police repawning

  

## Ben

* Powerup Items Buzz Effects and Collectible object code heirarchy

* Powerup Logic (Buzz speed up, student luring to player, Invinicibility, and donut drop, etc.)

* Sound Design and Implementation

* Music toggling and cues in menu and in-game

* Powerup and Bulldog Graphics

* Police AI - Police chase radius and speed management

* Student AI - Student controller state machine for ragdoll, wander and running away states

  

## Kevin

* Level design

* Students run away from Buzz when he slaps nearby students

* Student pathing

* Police wander

* Police investigates the crime scenes

* In game music

* Fixing ragdoll corpses

* Video submission

  

## Chan

* Buzz root motion script

* Buzz input controller script

* Buzz slappable script

* Student hittable script

* Buzz animation blending

* Buzz graphic model

* Powerup tweaks to work with root motion logic

* Camera position during game play

* Inverse Kinematics for punch directing to student's face

* Vending machine colliders

* Fixing unhittable student bug
