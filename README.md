Getting Started JurAIssic Park
===================================

1. Clone the repository to your computer
2. Open Unity 
3. Play the IntroScene at JurAIssic Park\Jur-AI-ssic Park\Assets\GameJam\IntroScene\Scene

## Game Modes
There are 3 modes in JurAIssic Park:

* Dinosaur player: collect AI humans as much as you can!
* Human player: prevent from getting killed by a dinosaur AI.
* AIvsAI: look how the AI dinosaurs try to eat humans, and how humans AI try to save their lives.

## Tank Controller
* W: Forward
* A: Rotate Left
* D: Rotate Right

Behind the Scenes
==================

* The dinosaurs and humans are completely trained with proximal policy optimization (PPO) by using Unity ml-agents. 
	- Dinosaurs are rewarded when they eat a humans
	- Humans are penalized when they are eaten by dinosaurs
	- Both dinosaurs and humans are penalized if they crash with an obstacle or wall.
