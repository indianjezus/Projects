Zakk Loveall, Vihan Garg, Austin Barner

Final Notebook

26 April 2024

Senior Design

1. **Summary of Project Goals:**
   1. <https://github.com/SeniorDesign2023/darwin/tree/main/Senior%20Design>
   1. Our project goal started off as a goal to create a game where you could watch creatures adapt to a randomly generated environment. However, after realizing this scope was much too big to tackle in 8 months, we pivoted our goal to creating a program that will generate random 2D terrain, and have ants navigate through this terrain to find food. The key part of our project was to simulate some form of learning though and this was where our main goal came to light. To use a form of the popular Ant Colony Optimization algorithm to allow the ants to ‘teach’ other ants how to navigate towards the food via pheromone trails.  
1. **Links to All Status Updates:**
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/status1.md>
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/status2.md>
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/status3.md>
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/status4.md>
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/Status5.md>
   1. <https://github.com/SeniorDesign2023/darwin/blob/main/status/status6.md>
1. **Links to All Videos Created:**
   1. <https://www.youtube.com/watch?v=j3s3uv8O2vM>
1. **Project Planning and Execution:**
   1. **Link to Design Requirements and Specification:**
      1. <https://github.com/SeniorDesign2023/darwin/blob/main/specifications.md>
   1. **Finalized Plan of Work:**
      1. Aside from the obvious changes from our initial idea, our expected vs actual turned out to be almost identical. For instance, here’s what our expected MVP was at the end of the first semester (from our intermediate planning document): *“We can promise to deliver an executable file that, when opened, will have a UI with pseudo-randomly generated terrain and an agent in a starting position, and the user can see this agent attempt to navigate the terrain and make its way to the end as it learns which move is the best to make. There will be a clearly marked start and end spot that the user can identify (maybe via color or text on the tile).”* This description is exactly what we accomplished by the end of this semester. We were successfully able to create a program that will generate random 2D terrain based on a seed, and there is an agent (ant) that is able to learn where to go to get to an objective. Which is a clearly marked end tile. We don’t have clearly marked start tiles in our actual product because our ants start at different locations anyway.
1. **Summary of Final Implementation:**
   1. **Design:**
      1. The design of our project went through many highs and lows. The main design started with our terrain. We knew we wanted our terrain to have a look and feel of an ant colony, which is hard to pull off because this would mean that every path in the colony has to connect in some way to every other path. This also means the colors and feel would have to look right. Which led us to realizing we had to make art for this game. So we made stone and dirt tiles to represent the background and foreground of the terrain. We also made art for the look of the ant so it’s more pleasing to look at and keep track of them as they move throughout the terrain. The other design we had to deal with was the pheromone look. For this, we decided to go simple in order to preserve the behavior of the pheromone. So we decided to go with a color-based gradient system. This looked and felt great. It was easy to interpret the strength of the pheromone based on how shaded in it was.
   1. **Limitations:**
      1. The main limitation of our project was that there is no main screen to edit our settings. We would’ve liked to implement this, but didn’t have enough time. This makes it hard to test different terrains with a different number of creatures. There’s also an issue where on some big terrain maps, the ants will get stuck.
   1. **Future Work:**
      1. Our future work for this project mostly involves developing it into a full game. Specifically by having more complex environments and more complex creatures with more complex behaviors. This would expand our project a lot. We would also like to fix the previously mentioned issues with our project before attempting these.
   1. **Statement of Work:**
      1. **Whole Team:**
         1. Our whole team worked to ensure all of our ideas were sound and worked. We all did lots of testing along with lots of planning. Total, I’d say we’ve spent ~500 hours on this project between all of us. These hours involved a lot of planning, testing, and coding. 
      1. **1 Per Team:**
         1. Zakk - I mainly worked on getting all of our documents completed. I also created our project video. I also worked on the art for the game, specifically the stone and dirt tiles. For the coding part, I worked on implementing some of the logic for the pheromone movement system. (150 hours)
         1. Vihan - I mainly worked on the art for the ants and the documents for the project. I also worked on the logic for the tile system and the movement algorithm for the ants. (150 hours)
         1. Austin - I worked on implementing most of the code. Specifically, this involved implementing the code for the terrain generation and the code for the tile system to work with the terrain generation. I also helped with the pheromone system and movement algorithm. (200 hours)
1. **Reflection on Team’s Ability to Design, Implement, and Evaluate:**
   1. **Lessons Learned:**
      1. The main lessons we learned over the past 9 months have been that it’s very hard to compromise on ideas. We spent a lot of our time debating over what to do because we all had different ways we wanted to go about doing this project. Another lesson learned was that tasks are way harder to do than what they look like on paper. For example, our initial scope started off as a game where you can watch different creatures evolve over multiple generations in a randomly generated environment. Our mentor quickly made it apparent to us that our idea was a multi-year-long project, so we needed to scale back. Another main lesson learned was it’s hard to find the best way to do a task. For example, we spent a lot of time battling Unity and other search algorithms. 
   1. **“If you had to do it again”:**
      1. If we had to do it all over again we would start by not debating ideas so much. We spent a lot of time not agreeing on ways to approach the project and this took time away from our planning and coding. We would’ve also written more pseudo code for this project. We quickly realized when spring semester started that we had almost nowhere to reference besides our planning documents, which did help, but not as much as pseudo code would’ve. We also would’ve spent more time working on this project together. Most of this project was spent working on separately.
   1. **Advice for Future Teams:**
      1. If we had any advice to give to future teams it would be to start planning early. And this means having every bit of the project from start to finish planned out before the second part of the class in spring starts. We would also advise them to start messing around with ideas in code before Spring semester starts, even if this means writing pseudo code. Our main piece of advice would be to work together (in the same room) on the project. Being in the same place allows for the transfer of ideas easier as well as the implementation of those ideas to be easier. 
