README FILE FOR GATECH AUTO'S SILLY RACE
----------------------------------------

Full Names: Ines Bouzidi, Ishan Maheshwari, Miko Linsangan, Oscar Marginean, Pedro Marquez
Emails: ibouzidi3@gatech.edu, imaheshwari3@gatech.edu, mlinsangan3@gatech.edu, pmarquez3@gatech.edu
Canvas Account Names: ibouzidi3, imaheshwari3, mlinsangan3, pmarquez3

Main Scene: MainScene

Any requirement not completed: None for Alpha

Implementation:
<STILL TO BE FILLED>

Start Scene file: Menu

How to play:
1. Press Race on the Menu.
2. The race now begins, use arrow keys to move and space to jump.
3. Your goal is to beat the opponent to the finish line while avoiding obstacles and collecting as many coins as possible.

The map consists of 3 different sections (start/challenge/finish) that were assembeled together into one scene "Main Scene"
Each of these maps contains obstacles that the player will encounter while navigating their way toward the finish line.
These obstacles such as the moving platforms are animated using the Mecanim Animation system. Some of them will
display the animation and/or play a sound effect when the player approaches the area (Pendulums, crashing stones,Rolling balls,
laser shooters). Some however would get triggered when the player comes into direct contact (jumpable mashrooms, explosive boxes )
Currently the map has only one single checkpoint that will save the position of the player once the latter reaches it.
The moment the player fall off the map their position will be set to the saved one instead of the start line position.


Known problem areas: None

Manifest:
Ines Bouzidi - Built game environment.
Assets implemented: StartSection, ChallengeSection, EndSection.
Assets and Credits:
Assets:
FSP MAST https://fertile-soil-productions.itch.io/mast
ArcadeGameBGM#3: https://assetstore.unity.com/packages/audio/music/arcade-game-bgm-3-173560
Track Assets (start and finish line material)
ObstacleCoursePack
RollingBall: https://forum.unity.com/threads/randomly-enable-and-disable-gameobjects-after-a-certain-time.835528/
Event Manager, Laser and explosion sounds: Milestones
Files contributed to:
Checkpoint.cs, FinishLine.cs ,JumpableMashroom.cs,
Obstacles/: ExplosiveBox.cs, FallOnContact.cs, Laser.cs, PendulumAnimation.cs, Roller.cs, RollingBall.cs, RollingBallActivator.cs
AppEvents/*: (collaboration with Miko)

Ishan Maheshwari - Player control. Assets implemented: NPC. Files contributed to: PlayerController.cs.

Miko Linsangan - Collectibles
Assets implemented:
Collectible:/ BoxOfPandora Random, Random With Rigidbody Prop_Coin WithAnimation,Coins Vertical, Powerups Horizontal, Powerups with Rigid Body 
Files contributed to: 
PlayerRank.cs TextHelper.cs
Collectibles/: ResetPickUpBoxAnimation.cs, ShieldPowerup.cs, SpeedPowerup.cs, PickUpBox.cs, ItemGenerator.cs, CoinCollectorComponent.cs, PowerUpsCollector.cs
,PowerUpsCollectorComponent.cs, ShieldCollector.cs, SpeedCollector.cs,
AppEvents/*: (collaboration with Ines)

Oscar Marginean - Menu and character design. Assets implemented: Player. Files contributed to: MenuController.cs.

Pedro Marquez - AI for NPC.  Assets implemented: Waypoints, NPC(1). Files contributed to: AgentLinkMover.cs, NPCController.cs, FreeJumpLink.cs.
