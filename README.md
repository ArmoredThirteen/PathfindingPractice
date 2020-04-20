# ProjectPathPractice
General practice with pathing. Some really rough stuff in there.

# ProjectDjikstras
Example Djikstra's implementation:
- To run the first example map:
  - Open "Assets/Scenes/VisualExample"
  - Select scene object "PathingExample"
  - In the component "Request Pathing Example" press "Request and display PathMap"
- The PathingExample object's settings have tooltips (hover over setting for tip)
- Multiple example maps can be found in "Assets/MapData"
  - By default, values of 0 or less are read as unpathable (settable)
  - Move costs higher than 1 will work
- To run other example maps:
  - Drag a .csv file to the PathingExample's MoveMap component's "Move Cost CSV" field
  - In Request Pathing Example, press "Reload CSV into MoveMap"
  - Then press "Request and display PathMap"
  - Note that the examples are intended to work with the (4,6) starting location
- Script overview:
  - MoveMap
    - Contains the movement cost map for pathing to be run on
	- Loads in a simple .csv to the internal data structure
  - MoveMapVisualizer
    - Creates a visual representation of the MoveMap by instantiating multicolored cubes
  - Pathing
    - Main driver that has actual pathing algorithm in it
	- Uses a version of Djikstras to handle any positive move costs
  - PathMap
    - Data for storing the results of a pathing request
  - PathNode
    - Data for storing node location and associated move cost
  - RequestPathingExample
    - Convenient example for interacting with all of the above
	- Includes custom editor found in "Assets/Scripts/Editor"