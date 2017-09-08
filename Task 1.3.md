GETAWAY DRIVER

Only drives between 1PM-2PM. Must go as close as possible to car max speed (80)

Requirements:

1. Ignore anything before the first non-zero speed in a day, and after the last
2. At every moment, assign rating such that:
	* Speeds between 0 and 80 map linearly to [0,1]
	* Speeds above 80 map to 1
	* Undocumented periods map to 0
