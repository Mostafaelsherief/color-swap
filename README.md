

# Color Swap
This is a Replica of a Mobile Puzzle Game Developed by 2024 Studios, To Solve the Puzzle each two connected nodes should be of different colors, you can drag the colored nodes and move them along the connection.

## Solution 
Each node contains a Child node(Colored Node) when The player touches a position near the Node the Child Node is Movable, when the player releases the child node it publishes its position and all the Parent nodes subscribe to this  message, if the released colored node is near the position of a connected node to the parent Node a Swap Occurs. 

After the Swap, The Graph is Traversed to check for any similar colored neighbors if that's the case the Game continues, otherwise, the puzzle is solved.

Communication between different components is achieved using a simple Pub-sub Pattern implemented using C# Action Events.

**LeanTween** Library is Used for Tweening to give a smoother look and improve the game feel.
