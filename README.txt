PREMISE 
- You are Buzz, Georgia Tech's beloved mascot and you have been trapped on an unknown rival university's campus.
You must explore the area to find a way out before it's too late! Careful, because the campus police is watching you. 

CONTROLS  
- Use the arrow keys or wasd keys to move your character
- Left shift - Run 
- Ctrl or Left Click - Interact
- 1, 2, 3 - Abilities from power ups 
- Esc - Pause game
- i - Toggle inventory 

INFORMATION FOR THE TA 
- The ultimate goal is to knock out the students on campus. Each student has a chance to drop a power up or a bone fragment 
- Bone fragments will fill up the bone bar at the top right screen
- Once the bone bar is full, find the bulldogs that are guarding the exit to bribe them and leave campus
- The more bones you have, the more 

COMPLETED FOR ALPHA 
- Buzz can move around using root motion 
- Buzz can run with shift held down 
- Buzz can knock out students when it is near a student. This will cause the student to ragdoll 
- Buzz uses inverse kinematics to aim for the student's head 
- Students will walk around campus using AI pathing 
- Police will chase the buzz if buzz gets too close 
- Power Up Latte: Speed up
- Power Up White Claw: Speed down 
- Power Up Donut: Will eventually be used to distract police officers but for now only throwing has been implemented

FEATURES THAT ARE COMING 
- Graphics and models for buzz, power ups, police, students, and bulldogs 
- Bulldogs will respond with a hint to find bones to beat the level 
- Students will drop power ups or bone fragments when knocked out 
- Difficulty level increases as more bones are collected. This will spawn more police officers
- After 5 minutes, you will enter danger mode where many police officers will start chasing you, regardless of how many bones you have
- Safe zone that will reset the difficulty level when buzz stays in the area for a set amount of time 
- Make the White Claw a “popularity”-inducing drink that will cause students to want to slowly gravitate towards Buzz, making it easier to complete his goal (but also make it more dangerous since the police will have an easier time catching Buzz)
- To add a level of mastery to the game, Buzz will be able to slap the police
- Jumping animation
- Some interactable items we plan on adding include environment vending machines, where buzz can use a bone fragment to get a power up.
- Hiding spaces to evade police
- Advancements to police AI so that they will look more closely at areas where Buzz recently ragdolled a student
- The campus will be expanded and walled in with a fence to enforce spatial limitations of the campus

Technical requirements satisfied:

3D Game Feel:
- Game restarts in a clean state after game overs and game restarts.
- Goal of the game can be found in the introduction, hints will also be played by guarding bulldogs at the exit gates.
- Game is played in third person, in which the player will control and move buzz around the campus. Camera and collision bounds still need some work.

Precursors to Fun Gameplay:
- This game is intended to be wacky and chaotic
- Ragdolls are meant to add comedic effects, we plan to add in directional vector force to give slaps a bit of a weighty feeling.
- More NPCs will be added to that the player is constantly on the move, to either slap students or by running from the police. We don’t want the player to be standing still or idle for most of the game, we constantly want them to be moving.

3D Character with Real Time Control:
- Buzz can be moved with standard WASD movement, you can press left shift to run.
- Buzz comes with root motion enabled, which blends in smooth movement when transitioning from standing->walking->running
- Slap button is mapped to “fire1”. You can Left Mouse Click or L-CTRL when in close proximity to a student to begin a slap animation.
    - Slap animations are implemented with IK, this maps Buzz’s hand to the students’ face when the animation is played
    - Slap animations are accompanied by a slap noise, for now we have a “whoosh” sound for the hand when the animation plays to slap a student
- Turning speed of Buzz was adjusted to make turning feel more responsive, although movement overall might need some adjustment and polishing for the final version of the game.

3D World with Physical and Spatial Simulation:
- Custom built level design using 3rd party assets to generate a student campus-like feel
- Environmental physical interactions in the form of power ups located throughout campus that can be obtained as items from ragdolled students.
    - Donut: power up that can be dropped by the user that will cause the police to chase the donut instead of Buzz
    - White Claw: power up that will slow Buzz down
    - Latte: power up that speeds up movement speed as a result of caffeine.
- There are audio cues and stimulus to help reinforce the physical interaction when power ups and bone fragments are picked up. Likewise, we have implemented auditory features when Buzz ragdolls a student to help drive home physical interactions
- Inverse kinematics is used to align Buzz’s punching animation with the head of the student he is about to ragdoll to aid with the spatial simulation and physical interactions of the game
- Consistent running speed tied to framerate

Real Time NPC Steering/AI:
- Student NPCs (green) path find to various path nodes on the map. There are more waypoints near popular campus locations to drive home the feel of a campus environment. They select waypoints randomly in their waypoint array and will move from waypoint to waypoint. Some feedback we received and will plan on implementing include having students avoid areas in which buzz is seen slapping a student. To do this, we plan on having certain path nodes turn off when buzz is seen attacking another student. Students will have a chance to drop bones and power ups upon being slapped for the player to pick up and use.
- Police NPCs (red) currently stand still and will find a path to buzz if he gets too near. Police will prioritize donuts over buzz

Polish:
- UI
    - The game contains a start menu on load, where you may start/exit the game.
    - The game has a pause menu (press ESC), where you may restart or quit the game.
    - The aesthetic of each UI scene/component was meant to replicate the look and feel of the buildings/campus, in color and style. It is consistent throughout each menu and helps make the game feel more cohesive as a whole.
    - Inventory system that you can open with the (I) key. This shows what power ups are obtainable, as well as whether you have the item (the item will become filled in). It also shows which button will activate the item. This is modular and easily expandable to new powerups.
- Environment Acknowledges Player:
    - Students ragdoll with respect to the inverse kinematics of Buzz’s hand when he punches the students.
    - The police chase Buzz when he is in range.
    - There are unique sounds that activate for events, such as: powerup pickup, unique sounds when using each power up, sound for bone fragment pickup, and a unique sound for a student punch/slap.
- Cohesiveness / Unified Aesthetic
    - As previously mentioned, the UI was designed to fit the feel of the campus environment in the game in both color scheme and design aesthetic. Consistent assets were used to make sure the graphics feel cohesive and from one artist.

