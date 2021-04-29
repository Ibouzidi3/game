README FILE FOR GATECH AUTO'S SILLY RACE
----------------------------------------

Full Names: Ines Bouzidi, Ishan Maheshwari, Miko Linsangan, Oscar Marginean, Pedro Marquez
Emails: ibouzidi3@gatech.edu, imaheshwari3@gatech.edu, mlinsangan3@gatech.edu, oscar.marginean@gatech.edu, pmarquez3@gatech.edu
Canvas Account Names: ibouzidi3, imaheshwari3, mlinsangan3, pmarquez3

Main Scene: MainScene
Start Scene: LoadingScene

Any requirement not completed: None
Known problem areas: None

Game Goal:
Your goal is to beat the opponent to the finish line while avoiding obstacles and collecting as many coins as possible.

The map consists of 3 different sections (start/challenge/finish) that were assembeled together into one scene "Main Scene"
Each of these maps contains obstacles that the player will encounter while navigating their way toward the finish line.
These obstacles such as the moving platforms are animated using the Mecanim Animation system. Some of them will
display the animation and/or play a sound effect when the player approaches the area (Pendulums, crashing stones,Rolling balls,
laser shooters). Some however would get triggered when the player comes into direct contact (jumpable mashrooms, explosive boxes )
Currently the map has only one single checkpoint that will save the position of the player once the latter reaches it.
The moment the player fall off the map their position will be set to the saved one instead of the start line position.


Manifest:
=====================================================================================================
Ines Bouzidi - Built game environment.
=====================================================================================================
Assets implemented: StartSection, ChallengeSection, EndSection.
Assets and Credits:
FSP MAST https://fertile-soil-productions.itch.io/mast
ArcadeGameBGM#3: https://assetstore.unity.com/packages/audio/music/arcade-game-bgm-3-173560
3D Cartoon Box
Track Assets (start and finish line material)
ObstacleCoursePack
RollingBall: https://forum.unity.com/threads/randomly-enable-and-disable-gameobjects-after-a-certain-time.835528/
Event Manager, Laser and explosion sounds: Milestones
water footsteps: https://elements.envato.com/footsteps-water-8297-UHCYM7L
endofrace bgm: https://assets.johnleonardfrench.com/ultimate-game-music/
fireworks audio: https://psionicgames.itch.io/30-free-firework-sfx
fire : https://www.soundjay.com/fire-sound-effects.html
other sounds: https://freesound.org/
https://www.freesoundeffects.com/free-sounds/fire-10007/20/tot_sold/20/2/
https://weeklyhow.com/loading-bar-screen-in-unity/

Files contributed to:
LoadingManager.cs, NPCProjectile.cs, PauseMenuToggle.cs, Projectile.cs, ProjectileShooter.cs, 
Keyboard.cs, Checkpoint.cs, FinishLine.cs, JumpableMashroom.cs, WinningLosingAnimation.cs
Obstacles/*
AppEvents/*: (collaboration with Miko)

=====================================================================================================
Ishan Maheshwari - Player control. 
=====================================================================================================
Assets implemented: NPC. 
Files contributed to: PlayerController.cs.

=====================================================================================================
Miko Linsangan - Collectibles
=====================================================================================================
Assets implemented:
Collectible:/ BoxOfPandora Random, Random With Rigidbody Prop_Coin WithAnimation,Coins Vertical, Powerups Horizontal, Powerups with Rigid Body
Files contributed to:
PlayerRank.cs TextHelper.cs, SliderLocation.cs, GameStartCountdown.cs
Collectibles/: ResetPickUpBoxAnimation.cs, ShieldPowerup.cs, SpeedPowerup.cs, PickUpBox.cs, ItemGenerator.cs, CoinCollectorComponent.cs, 
PowerUpsCollector.cs, PowerUpsCollectorComponent.cs, ShieldCollector.cs, SpeedCollector.cs,
AppEvents/*: (collaboration with Ines)

=====================================================================================================
Oscar Marginean - Menu and character design. 
=====================================================================================================
Assets implemented: Player. 
Files contributed to: MainMenu/* , MenuController.cs, AvatarSettings.cs, GameState.cs, ResourceManager.cs

=====================================================================================================
Pedro Marquez - AI for NPC.  
=====================================================================================================
Assets implemented: Waypoints, NPC(1). 
Files contributed to: AgentLinkMover.cs, NPCController.cs, FreeJumpLink.cs.
