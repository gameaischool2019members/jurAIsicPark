Getting Started JurAIssic Park
===================================

1. Clone the repository to your computer
2. Open the Unity project
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
[JurAIssic Park]images/jurassic.png

Set-up: A multi-agent environment where dinosaurs eat humans and humans escape from dinosuars
Goal: Eat humans or escape dinosaurs
Agents: The environment contains many dinosaurs linked to a single dinosaur brian and many humans linked to a single human brain.
Dinosaur Reward Function (independent):
+1 for interaction with humans
-1 for interaction with obstacles and walls
Humans Reward Function (independent):
-1 for interaction with dinosaurs
-1 for interaction with obstacles and walls
Brains: Two Brains with the following observation/action space.
Vector Observation space: 74 corresponding to velocity of agent (2) plus ray-based perception of objects around agent's forward (and backward for humans) directions  (12 raycast angles).
Vector Action space: (Discrete) 2 Branches:
Forward Motion (2 possible actions: Forward, No Action)
Rotation (3 possible actions: Rotate Left, Rotate Right, No Action)

Benchmark Mean Reward for Dinosaurs: 5
Benchmark Mean Reward for Humans: -1.8
