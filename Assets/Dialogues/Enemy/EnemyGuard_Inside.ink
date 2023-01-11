INCLUDE ../globals.ink
...
I have my eyes on you, just so you know.
{PLAYER_BETRAYED==1: ->escaped_prisoner_dialogue}
+ [Where is Steve?]
- He had to go
+ [Okay] Keep moving
+ [Sounds like you killed him] He is fine
  Err.. No. Our leader asked for only the main messenger to be brought to him
- Go right ahead. Our leader will see you now
->END

=escaped_prisoner_dialogue
You smell of filth.
Go on now. Our leader is right ahead
But keep your distance
->END