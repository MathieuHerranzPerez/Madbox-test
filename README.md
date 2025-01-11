
## Work

I tackled the goal bullet points one by one, while keeping the subsequent tasks in mind to anticipate the architecture.

---

### Movement

#### Unit movement

First, I tried *Archero* to see how the character movement works.
I considered four options:
1. RigidBody, controlling the velocity
2. CharacterController
3. Manipulating transform.position
4. NavMeshAgent

- The NavMeshAgent could be useful for enemies, but unecessary for the player. => ~~NavMeshAgent~~
- Manipulating transform.position could work for for a small-scale project like this, but adding obstacles later would require implementing custom collision detection on our own... => ~~Manipulating transform.position~~
- Wy using physics-based movement (RigidBody) on a simple game like that, where performance is crucial? => ~~RigidBody~~

A *CharacterController* seemed like the best fit for the Hero!

To separate core movement logic from visuals, I created two scripts:
- *UnitMovementController*, handles actual movement
- *UnitMovementAnimator*, handles animations and Fx

Note that these scripts can also be reused for simple enemies. For that, we’d need to create an EnemyController (similar to the *PlayerController*) to link all enemy logic.

#### Inputs

To support both the Unity Editor and mobile platforms, I used *IPointerDownHandler*, *IDragHandler* and *IPointerUpHandler* interfaces. This avoided the need for platform-specific code (e.g. #IF UNITY_EDITOR).


#### Result

**3h**
This part took longer than I expected. I felt a bit rusty, and I debated whether to stick with *CharacterController* or switch to *RigidBody*. It took time to find my footing and move forward.


---

### Enemy spawner

#### Spawner

*UnitSpawner* is a simple script that spawns *Unit*s within the defined bounds at a specific spawnRate.
It uses a *UnitFactoryData* which is responsible for creating a *Unit*. While the factory doesn’t do much now, it was designed with flexibility in mind, so future levels could introduce different unit types or strengths.
I started implementing pooling in the factory but decided to abort it due to time constraints.
I wanted to go beyond the set goal, but I remembered the extra time I spent on the previous part.

#### Result

**0h30**
I left the possibility to extend the functionalities while not spending too much time on it. It works, but I would have liked to highlight the spawn location with a simple indicator (e.g. a circle on the ground a second before the spawn).


---

### Hero auto-attack

#### Auto-attack trigger

I created the *UnitAttackController* to handle launching an attack every *AttackRate* seconds. This script is driven by another controller (*PlayerController* for the Hero) to determine when attacks can occur.
Initially, *TriggerAutoAttack()* logged a message since the target system wasn’t implemented yet. By the end, the script supported attacking a *Unit* target and dealing damage.

To keep core logic separate from animations, I created *UnitAttackControllerAnimator*.

Note that these scripts can also be use for enemy attacks.

#### Hit

To synchronize damage application with animations (anticipation of "Timing to apply damage to fit the animation" of the [Weapons](#weapons) part), the *UnitAttackController* includes a *TriggerHit()* method.  This is triggered by an AnimationEvent in the attack animation, linked to the *UnitAttackControllerAnimator*.


#### Result

**1h30**

Nothing visually exciting here : the animation plays and a log appears in the console when the player stop moving.


---

### Auto-target

A health counter was required, so I added it to  the *Unit* script. Every *Unit* can now die and a death animation is triggered. To allow time for the death animation to play, *Unit* also includes an *IsAlive* property.

I added a SphereCollider to both enemies and the hero to anticipate some need of raycast (for targeting or to receive attack), and for the [Weapons](#weapons) part, where I wanted to create an Area Of Effect weapon.
A SphereCollider was chosen because it’s the simplest shape to use for physics.
I also created a the *Enemy* Layer.

For targeting logic, the *PlayerController*:
1. Identifies nearby enemies within range
2. Assigns the target to UnitAttackController
3. Faces the target using UnitMovementController
   
To make the system more flexible, I implemented a strategy pattern for targeting. Currently, only *UnitTargeterNearestStrategy* is implemented.

For the moment, I chose to target a Unit using a Physics.OverlapSphere. It looks appropriate with the specifications. But for performances reasons, we could also consider based on the direction the project could take :
- Iterating throught all the enemies (acceptable if enemies are close to the Hero)
- Sorting enemies on a grid to narrow the search space
- using DOTS?

Also, I was already thinking of the last part of the project : "each weapon will modify [stats]". But for now, all the values of the movement speed, attack damage, etc, were not centralized. I created the *UnitStatsController*. Initially, I used interfaces (IMovementStatsController, AttackStatsController, ...) injected into the Controllers by the *PlayerController*. But then refactored it as a MonoBehaviour with an hard references into the Controllers. I could have kept the Interfaces and the classic C# object within the new MonoBehaviour for easier UnitTest purposes, but I chose to remove it to make the debug part easier without writing specific code for stats to appear in the Inspector.

#### Result

**2h30**

---

### Weapons

This was the most complex part (as expected).
*UnitWeaponedAttackController* is an essential script here. Inheriting from *UnitAttackController*, with an added method: *EquipWeapon(WeaponData weaponData)*. This delegates attack logic to the equipped *Weapon* itself. Each weapon could have unique behaviors, such as AoE, laser beams, knockbacks, or projectiles. Unfortunately, I didn’t have time to implement these, so it works like the auto-attack.

I wanted each Weapon to not only change the animation duration based on the *AttackRate*, but also to play its own animation. In *UnitWeaponedAttackAnimator* (previously *UnitAttackAnimator*), the attack animation of the animator is overriden with the one given by the Weapon.

Like in the *UnitMovementAnimator*, a speedFactor for the attack animation is computed in order to designers to only change the value of AttackRate without worrying about animation timings.
This speedFactor is computed when the *Weapon* is changed or the value in *UnitStatsController* is modified.
This system anticipates future features, like slowing units (e.g. ice weapons) or stat changes during gameplay (e.g. boosts).


#### Result

**4h00**

Time was running out. I would have prefer keep the *UnitAttackAnimator* like it was to quickly attach it to the Enemies. But I chose to modify it without thinking (I rushed some decisions, I wanted the Goal to work well), and it became *UnitWeaponedAttackAnimator*.


---

### Weapon selection

I quickly bound a UI to let the player select a *Weapon* from available *WeaponData*s.

#### Result

**0h10**


---

### Performances

I made quick optimizations:
- set materials to Unlit
- disable the shadows on MeshRenderer
- reduce textures sizes
- checked that the environment was already static
- set the light to be baked and not real-time


#### Result

**0h05**

---

---

## What was difficult

- I felt rusty and slow since I hadn't worked on "non-UI projects" in a while.
- I underestimated the time required to reach an MVP. I expected 8–9 hours but was wrong.
- I was torn between the dilemma of doing things properly or making a fun game. Since this wasn’t a game jam, I prioritized architecture, readability, and performance over gameplay feel.

## The features you think you could do better and how

1. Minimum game feel, feedbacks, juice:
   - Fx on hit
   - sword slashes
   - hit damage display
   - camera shake
   - running Fx
  
2. The *PlayerController* could be a state machine if meant to grow
3. The *UnitWeaponedAttackAnimator* should inherite from *UnitAttackAnimator* : the enemies basically don't need weapons or complex attack logic and only want an auto-attack.


## What you would do if you could go a step further on this game

 - feedbacks on hit : Fx, Hit damage, camera shake
 - weapons : Slash Fx for the swords, different behaviours (projectiles, AOE)
 - enemies behaviour : try to kill the player
 - different enemies
 - endless level with a score counter
 - level design to have more interesting rooms