## Introduction

I started with the design of the game system titled **Queens Make Bees**. The core mechanics are as follows:

### System 1: **Queens Make Bees**

1. For a **Queen** to make **Bees**, it needs **Hives**.
2. Hives can be purchased with **Honey**.
3. Players have an initial amount of **Honey** to spend on the first **Hive** and **Queen**.
4. Queens have 1 out of these 8 Species, reflecting real-life honey bee species:
    1. Apis Cerena 🔸The Eastern Honey Bee
    2. Apis Mellifera 🔸The Western Honey Bee
    3. Apis Andreniformis 🔸The Black Dwarf Honey Bee
    4. Apis Florea 🔸The Red Dwarf Honey Bee
    5. Apis Dorsata 🔸The Giant Honey Bee
    6. Apis Laborisa 🔸The Himalayan Giant Honey Bee
    7. Apis Koschevnikovi 🔸The Koschevnikovi’s Honey Bee
    8. Apis Nigrocincta 🔸The Philippine Honey Bee
5. Each species has its own strengths and weaknesses.

### System 2: **Bees Make Honey**

1. For bees to make honey, they need to be ordered by the queen (by extension, the player) to go to the flower field and harvest nectar.
2. Flower Fields are tiles in a Land.
3. Lands are grid-maps composed of hexagonal-shaped tiles.
4. Flower Fields have a distance property, indicating the distance from the Hive where the Queen Bee resides to that particular Flower Field.
5. The Distance property is directly proportional to the time it would take to harvest the nectar and its amount. In other words: Longer Distances = Longer Time to Harvest = More Nectar.
6. Flower Fields can provide bonus effects.
7. Bonus Effects are positive buffs, they trigger on the next trip to a new Flower Field.

## System Explanation


- **Inventory System**: This was a key feature, designed to interact with all other game systems. It allows players to manage their items .
- **Player Movement**: I built a system that facilitates the player movements.
- **Building System**: This system allows players to construct and customize structures within the game, adding a layer of creativity and strategy.
- **Beta System for Saving and Loading**: Ensuring that players can save their progress and load it accurately was critical. I assured a robust beta system for this purpose.
- **SFX Manager**: This component manages sound effects within the game, ensuring that audio cues and ambiance enhance the player's immersion.
