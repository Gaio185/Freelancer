# Freelancer


## Introduction
In Freelancer, you play as an independent contractor that is paid to conduct certain jobs discretely. One such job will be to steal something of interest to your client from a bank. You must use your weapons/tools arsenal and the environment to your advantage if you are to complete this job successfully and quietly. You will be able to chose which tools, if any, to bring into the mission during the preperation phase, as well as get yourself briefed on the map layout and mission objective.

The objective of each level is to extract with the required item and do so without arising as much suspicion as possible.

To complete this objective, the player must traverse the level to find clues as to the requested items’ whereabouts and how to acquire them. In doing so, the player will encounter several different security measures that will stop them in their tracks and force them to find any approach that allows them to stay hidden.



## Game Controls

A -> move left

S -> move backwards

W -> move forward

D -> move right

Q,E -> lean left, right respectively

F -> interact button

Control-> crouch

Space -> jump

Shift -> run 

Left Mouse Click -> action button

1,2,3 -> Tool Selection (Smuggled Item, Regular Item, Gadget)

Escape-> Pause Button/Close Button

J -> Note Inventory



## Player Arsenal
### Weapons
Weapons are combat tools that will help the player to work around detection in a more hostile manner, facilitating stealth by eliminating detection threats.
#### Taser
![Captura de Ecrã (70)](https://github.com/user-attachments/assets/5856c2f6-c8e9-4f50-a8d4-263fb2836582)

The taser is a ranged weapon that will allow the player to stun enemies from a distance. Due to its ease of use, given its range capabilities, it will have a moderately long cooldown. Enemies will only be stunned for a moment and will not be suspicious upon exiting stun.

#### Stun Prod
![Captura de Ecrã (71)](https://github.com/user-attachments/assets/9957c185-275c-4552-9718-12c9685c6efa)

The stun prod is a melee weapon that will allow the player to stun enemies up close. Due to its risk of use, given its range capabilities, it will have a shorter cooldown than the taser. Enemies will be stunned for a longer duration than the taser stun duration and they won’t be suspicious upon exiting stun.


### Gadgets
Gadgets are tools that will help the player to ease the constraints of certain systems, facilitating stealth by streamlining level progression.
#### Override Pen Drive
![Captura de Ecrã (76)](https://github.com/user-attachments/assets/52a7ede0-266b-4ac2-aa51-78d8580a9401)

The Override Pen Drive is a pen drive that can override terminals and other computer systems to bypass login requirements. This will enable the player to access terminals and computers without figuring out the PIN keycodes. The pen drive will have a finite amount of uses.

#### Override Keycard
![Captura de Ecrã (75)](https://github.com/user-attachments/assets/0ca101d8-b14a-41a2-98e8-ed4f8732d35f)

The Override Keycard is a keycard that can override keycard readers to bypass them. This will enable the player to access rooms without having to find a matching keycard. The keycard will have a finite amount of uses.

### Items
Items are tools that will help to unlock or disrupt the environment around the player, facilitating stealth through distraction and staying hidden.
#### Screwdriver
![Captura de Ecrã (72)](https://github.com/user-attachments/assets/8f76261b-edbf-4858-89a4-69b885c976d0)

The screwdriver is an item that can be used to unlock certain parts of the environment, such as vents. They will afford the player more freedom to move around the level and avoid its security forces by travelling through hidden paths.

#### Coins
![Captura de Ecrã (74)](https://github.com/user-attachments/assets/7e8ddcd6-63b8-4e67-8b08-4de178849470)

Coins are items that can be thrown near enemies to distract them through sound. They will afford the player more freedom to move around the level and avoid its security forces by being an easy and accessible means to distracting them. The amount given to the player is finite but, once thrown, they can be picked back up.



## Enemy Roster
### Security Bots
![image](https://github.com/user-attachments/assets/64db2ad5-1588-4a85-877a-028a092afeb7)

The bank features a suite of robots that function as security personnel. There exists at least one in each floor, with the vault floor having the highest concentration of any other. They can be stationary or patrol along pre-determined paths. Robots will always be on the lookout for the player but will only detect them if the player does not have clearance for the corresponding room or floor. The detection process demands that the player stay in the field of view of the security bots for a short span of time, so that they aren’t detected automatically. If the player is detected, the hunt state is triggered, making the robot initiate an alarm protocol which, after a few seconds of start-up, will warn other bots in the vicinity and become hostile. Once hostile, Robots will hunt for the player and, if the player is within range, fire a projectile. If the player manages to hide from the robots, they will reset their behaviour and go back to scanning for threats. Robots can be disabled for a short duration with the help of weapons that the player chooses from at the start of the game. Sound can be used to distract robots, which will trigger their investigative state, making them search for the sound source. 

### Cameras
![image](https://github.com/user-attachments/assets/c5ed20c4-4c4f-4a7a-ad56-d6c63c2018dd)

Cameras are present throughout the bank in all floors. Like the security bots, they will always search for the player, but only detect them if the player does not have clearance for the corresponding floor/room or if they are holding an illegal item. Cameras will have a higher field of view than robots but, of course, will always be stationary. Also like the security bots, the cameras will not instantly detect the player, only after they stay within the cameras field of view for a specific amount of time. Once the player is detected, the camera will instantly give out a signal to a selection of bots in that floor that informs them of the last position the player was at, which will prompt them to enter the investigate state and search for the player, continuing to update the signal until the player leaves its field of view for a certain amount of time. Cameras will be tied to security terminals in each floor, so the player can turn them off by interacting with them, having to first unlock them with a password.

### Sentries
![image](https://github.com/user-attachments/assets/3d9443e8-d93e-40fb-902f-49ca8bdce14d)

Sentries are present only in the vault floor. Like the camera and security bots, they will always be scanning for the player, only detecting them if the player does not have clearance for the corresponding floor/room or if they are holding an illegal item. Sentries will have the narrowest field of view and will have a higher range. Like the security bots and the cameras, sentries will take some time to detect the player once they are in its field of view. Once the player is detected, sentries will fire projectiles like the security bots but at a higher rate. Sentries cannot be warned of the players presence by other entities, having to rely on their own detection system. Like the cameras, sentries will be tied to a security terminal and the player can deactivate them by interacting with said terminal, by first inserting the PIN code. 

### Bank Staff
![image](https://github.com/user-attachments/assets/6a3c9f45-2e0d-4461-b1c2-c2b457976e9b)

The bank will be further populated by staff, who will aimlessly wander around specific zones, such as the break rooms and offices. They will make it harder for the player to sneak around, panicking if they spot the player with a weapon. This will trigger the bots to investigate. They can’t defend themselves and cannot be targeted by the players weapons. 



## AI Systems
To give the enemies their intended behaviors, we resorted to three AI techniques:

### Unity Navmesh Pathfinding (includes A*) 
Enemies like the security bots and the bank staff require that they traverse the level, so we decided to utilize navmesh pathfinding from unity. We started out by baking a navmesh surface in the level, with the appropriate settings and bounds, so as to not cause conflicts during play.

![image](https://github.com/user-attachments/assets/eb86a52f-ff49-478e-ba92-92e37f994cbd)

Then we gave all of the AI agents that necessitated movement a navmesh agent, and configured it so that they dont conflict with the enemies' models, animations and with the world terrain.

![image](https://github.com/user-attachments/assets/1e41cfe9-f49a-4fc9-abe8-af6c330b9d85)

This would allow us to make these enemies move around the map, but a question stands: how exactly do they know where to move and when? For an answer to that, we must look at the state machines.

### State Machines

In order to give enemies behaviors that matched those we outlined in their descriptions, we resorted to using statemachines to create states that enemies could enter into, update and exit out of depending on many conditions. We created a finite amount of states to be used in a statemachine for each enemy type [security bots, terminal objects (cameras and sentries) and bank staff]. The statemachine is then used by an AI Agent component, that lets us decide certain parameters like the agents initial state.

![image](https://github.com/user-attachments/assets/d65f2aa9-e4e4-4a45-bb41-e604e3f244ca)

To show to what end the statemachines were used, we will do a rundown of the statemachine for each enemy type.

#### Security Bots
 - **Idle State**: informs the bot to stay still in one spot and watch for the player, if the player is detected, it enters the Hunt state;
 - **Patrol State**: similar to the idle state, but the enemy is tasked with looping a patrol path. It also enters the Hunt state if it detects the player;
 - **Hunt State**: the bot chases the player down. if it loses track of the player, it enters the investigate state. If it sees the player, it enters the Shoot state;
 - **Shoot State**: the bot shoots at the player for as long as it the player remains in its range. If the player leaves the range, the bot enters the Hunt state;
 - **Investigate State**: the bot goes to the position of whatever may have distracted him (either the players last known position, if it just left the Hunt state or by the camera/worker Alert state, or the source of a coin distraction). Once the position is reached, it will move to positions within a certain range for a short interval. If within that interval it sees the player again, the interval resets and it keeps investigating. If it detects the player, the bot enters the Hunt state. If the interval runs out, the bot enters its initial state;
 - **Stunned State**: if the bot gets hit by the taser or the stun prod, it will be stunned for a short period of time, with the bot not being able see or move. After said short period of time, the bot will enter its initial state.

#### Terminal Objects
 - **On State**: informs the agent to look for the player. If the player is detected, the agent switches to the alert state if its a camera or it switches to the shoot state if it is the sentry;
 - **Alert State**: the camera warns certain bots in its floor of the players position and, for as long as the player remains in its field of view, the camera will keep giving the players location away. If the player leaves the cameras field of view for a short duration, the camera enters the On state;
 - **Shoot State**: just like the enemy shoot state, if the sentry sees the player, it will shoot in their direction. If the player leaves their shot range, the sentry will enter the On state;
 - **Off State**: for when the player deactivates the agent through the terminal. It can no longer see, which renders it useless. Can only enter or exit this state through the player interacting with the terminal.

#### Bank Staff
 - **Wander State**: informs the worker to wander aimlessly within the max bounds set for the room, pausing every time it reaches its destination, then calculating a new one. If the worker spots the player, it enters the Alert State.
 - **Alert State**: the worker gives a specific enemy enter the investigate state, giving them the players position. After a certain amount of time, the worker enters the Wander state.



### Behavior Trees
We used behavior trees to diversify the ways we could attribute behaviors to agents. We chose to implement them on the bank staff, overriding what we had with their state machine.
On our Behaviour Tree implementation we used two basic behaviours of example:
- One Patrol Behaviour, similar to the enemy behaviour using the state machine. However, in the BT we are using waypoints instead of navmesh. It should be noted however, that the navmesh can be implemented in this behaviour aswell.
- One behaviour that consists in following the player, should they be inside the workers range and field of view. If the player then gets out of this range and FOV the worker returns to it's patrol path.

On our implementation we utilized a base Node, from which the other nodes inherit their states (RUNNING, FAILURE and SUCCESS) and attributes.

On the tree itself, we simply used a Selector Node as a root, which contains a Sequencer Node and a Action Node (Patroling) as children.
The Sequencer is on the far left of the tree, so it is executed first. In the Sequencer children, firstly we verify if the player is inside the range and FOV and if that node succeeds, we advance to the next Sequencer child, walking towards the Player.
In the case that the first Sequencer child fails, the whole sequence fails as well, and the  Selector Node advances to the Patrol Node, which will be the action carried out by default, considering it is the node furthest on the right of the tree.


