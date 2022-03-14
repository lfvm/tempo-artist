# **Tempo Artist**
# *Videogame for Percussive Arts Society*

## Game design document

---


##
## Index

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## Game design

---

### **Summary**

A simple yet challenging game whose objective is to help players to learn percusion tab reading and practice rhythm sense.

The player will be able to play the game through our webpage. The player statisctics will also be available on our webpage.

### **Gameplay**

The player must press the corresponding keys to the rythm of the notes that will be apearing on screen. The more accurate the clicks are the more points the player gets. If the player misses a note the combo is reset, and if the player misses several notes in rapid succession the level ends.

 <img width="607" alt="Captura de Pantalla 2022-03-09 a la(s) 13 47 10" src="https://user-images.githubusercontent.com/57450093/157521932-d8ed2dcd-26c9-42d0-961c-06e978c9db43.png">


### **Mentality**

The game aims to feel the player motivated to learn about musical rhythms, as well as improve their coordination and cognitive skills.
It is a challenging game where the user must strive to improve and reach the next levels.


## Aspectos tecnicos

---


### **Screens**


1. Main Menu
    * Play
    * Options
2. Select Level
    * Back
    * Level Info (hover)
3. Game Screen
    * Pause (Esc key)
4. Pause Sceen
    * Resume
    * Re-try
    * Exit
5 Options
    * Back

### **Controls**

The player must click the notes to the rhytm.

## Game design 

---
The levels will be generated with an algorithm that generates "Random" note patterns. These levels after being generated with stored in the game
so that they are the same for all players.


## _Development_

---

### **Abstract Classes / Components**

1. Note
2. Lane


### **Derived Classes / Component Compositions**

1. GameController
2. LevelGenerator
3. Note
	1. SingleNote
	2. DoubleNote
	3. TripleNote
4. UIControler
5. Button
6. Life Bar
7. Music Sheet

## _Graphics_

Most game elements graphics will be made with neon colors, using sprites for the notes, music sheet, life bar.

### **Style Attributes**

Neon colors

For graphic styling, we plan to make our sprites and objects simple and minimalist to reduce distractions. Sprites and objects should maintain a consistent style.

### **Graphics Needed**

1. Background
2. Notes
	1. Single Note
	2. Double Note
	3. Triple Note
3. Lanes
4. HitArea 
5. Life bar

## _Sounds/Music_

1. Correct note hit
2. Fail note hit
3. Menu bgm?
4. Level Complete
5. Level failed
6. Menu button click

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Note hit
    2. Note miss
    3. Menu button click
2. Music
    1. Menu bgm
    2. Level failed
    3. Level Comlete
    3. Pause Screen

### **Music Needed**

1. Slow-paced, nerve-racking track
2. Exciting &quot;castle&quoe track
3. Happy ending credits track

## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_
