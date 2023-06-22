# Unity_4Defenses

It's a Top Down Shooter game style. I wished to develop a complete game only by me and I made this game by this way.
The idea of this game is you need to protect your base from monsters that come to destroy it or you. So you need to kill them and keep alive yourself. You have some weapons and defends.

<strong>Purpose:</strong> Develop a whole game by me and get more experience in 2D game style. It'll be better than previous game(JetFire) which I developed and it'll have more features such as multiplayer online, a little shop system, upgrade system and more, and that it might seem like a good complete 2D game with multiplayer.

<strong>Link: 🚧</strong>

<br>
<!-- <br> -->

<details>
    <summary><strong><font size = "5">Versions</font></strong></summary>

    0️⃣v0.7.0 - 🏆🏆Beta Single Player has been done
    ✔️Add possibility to player switch your character
    ✔️Camera short shake when player fires
    ✔️Camera short shake when any enemy dies
    ✔️Camera big shake when boss die - big explosion
    ✔️Pause game functionality has been implemented in single player mode
    ♻️Cinemachine has been implemented so MainCamera is independent now and player and boss has your own camera
    🛠️Improve dinamic render layer of sprites
    🔥Removed - A little upgrade system(move, damage, life, speed shoot, countdown reloading...)
    🔥Removed - Implement cannon tower lvl 1
    🔥Removed - Implement flame tower lvl 1
    🔥Removed - Implement a kind of shop which to allow player buy news weapons, life, mine and towers
    🔥Removed - For every enemy the player kills, the enemies will drop some coins


    0️⃣v0.6.0 - 🏆Beta Single Player
    ✔️General balancement in the game
    ✔️Just draw the scenario
    ✔️Add collisions in some parts and somethings in scenario(tree, scenario limiter...)
    ✔️Implement NavMesh for AI enemies and boss
    ✔️Add possibility to player get more life
    ✔️Implement mine for player
    ✔️Damage along time system(flames)
    ✔️All Audios
    ✔️Configuration screen
    🛠️Change the style fire of flamegun to some spawn points flames
    🛠️Add effect of black smoke when the player shoots
    🛠️Add possibility to open and close windows through Escape button
    🐛Enemy and boss doesn't know choose the better way
    🔥Removed - Implement different stats(skills) for every characters like life, move and damage


    0️⃣v0.5.0
    ✔️Menu Game Pt.1 - Main functions and little design
    ✔️Victory
    ✔️Game Over (When base fall down or all players die)
    ✔️Implement AI boss
    ✔️Add Player's life bar
    ✔️Add some spawn points of enemies / Waves System
    ✔️Implement damage stats visual when player take any damage
    ✔️Add possibility for player can switch mouse's aim
    🛠️Implement damage stats visual when base receive any damage
    🛠️Explosion effect when any enemy spawn
    🛠️Add white effect when the guns' shoot collides
    🛠️When game over screen appears the player need to stay freeze
    🐛When the player shoots with shotgun downwards, he receives a knockback upwards


    0️⃣v0.4.0
    ✔️Put base in the scenario and implement your functions
    ✔️Implement green enemy(also your explosion)
    ✔️Add Enemies's life bar
    ✔️Make green enemy go to base and explode
    ✔️Implement new enemies with different color
    ✔️Make a big green enemy as a boss
    ✔️Implement AI in the minions enemies
    ♻️Code improved. Abstract Projectile. Hierarchy.


    0️⃣v0.3.2
    ✔️Implement each bullet of each gun.

    0️⃣v0.3.1
    🚧Implement each bullet of each gun. 🐛There are some bugs.

    0️⃣v0.3.0
    ✔️Implement 3 guns(Machinegun, Firegun, Shotgun) and to allow player switching between of them
    ✔️Improve Player's movimentation(collision, rigidbody, ...)
    🚧Implement each bullet of each gun


    0️⃣v0.2.0
    ♻️Readme updated
    🔥Remove .vscode and UserSettings from repository
    ⬆️Project version updated 2021.3.13f1 -> 2021.3.23f1


    0️⃣v0.1.0
    ✔️Implement 8 directions of player


    0️⃣v0.0.0
    ✔️Implement first character
    ✔️Project created
    ✔️Repository created

</details>

<br>

## To-Do List:

### ✔️Features:<br>

#### Player

- [x] Implement first character
- [x] Implement 8 directions of player
- [x] Implement 3 guns(Machinegun, Firegun, Shotgun) and to allow player switching between of them
- [x] Implement each bullet of each gun
- [x] Improve Player's movimentation(collision, rigidbody, ...)
- [x] Add possibility to player switch your character
- [x] Implement damage stats visual when player take any damage

#### Enemies

- [x] Implement green enemy(also your explosion)
- [x] Make green enemy follows player
- [x] Make green enemy go to base and explode
- [x] Add some spawn points of enemies / waves system
- [x] Implement AI in the minions enemies
- [x] Implement AI boss
- [x] Make a big green enemy as a boss
- [x] Implement new enemies with different color
- [x] Implement NavMesh for AI enemies and boss

#### Resources

- [x] Implement mine for player
- [x] Add possibility to player get life
- [ ] Implement multiplayer online until 4 player
- [x] Pause game functionality in single player mode

#### Mechanics

- [x] Damage along time system(flames)
- [x] Camera short shake when player fires
- [x] Camera short shake when any enemy dies
- [x] Camera big shake when boss die - big explosion

#### Scenario

- [x] Just draw the scenario
- [x] Put base in the scenario and implement your functions
- [x] Add collisions in some parts and somethings in scenario(tree, scenario limiter...)

#### UI

- [x] Add Player's life bar
- [x] Add Enemies's life bar
- [x] Add possibility for player can switch mouse's aim

#### Screens

- [x] Menu Game Pt.1 - Main functions and little design
- [ ] Menu Game Pt.2 - Complete functions and good responsive design
- [x] Configuration screen
- [x] Victory
- [x] Game Over (When base fall down or all players die)

#### Audios

##### Weapons

- [x] Machine gun
- [x] Shotgun
- [x] Flame gun
- [x] Land mine drop
- [x] Land mine explosion

##### Songs

- [x] Song during interval of waves
- [x] Song during in the waves
- [x] Song boss
- [x] Song victory
- [x] Song game over
- [x] Song menu

##### Others

- [x] Player hit
- [x] Enemies spawn
- [x] Enemies explosion
- [x] Enemies die
- [x] Boss damage explosion
- [x] Boss start death explosion
- [x] Boss death explosion
- [x] Button sound

<br>

### 🐛Fix Bugs:<br>

- [x] When the player shoots with shotgun downwards, he receives a knockback upwards
- ~~[ ] When time to follow player of enemy is between 0 and 3 and player inside in one of those circles, the enemy be lost until player get out the major area~~
- [x] Enemy and boss doesn't know choose the better way
- ~~[ ] Set parent of flamesprite to enemy(visual bug)~~

<br>

### 🛠️Some Ajusts/Upgrades:<br>

- [x] General balancement in the game
- [x] Improve dinamic render layer of sprites
- [x] Add possibility to open and close windows through Escape button
- [x] Change the style fire of flamegun to some spawn points flames
- [x] Add effect of black smoke when the player shoots
- [x] Add white effect when the guns' shoot collides
- [x] When game over screen appears the player need to stay freeze
- [x] Explosion effect when any enemy spawn
- [x] Implement damage stats visual when base receive any damage

<br>

---
