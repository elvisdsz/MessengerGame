INCLUDE ../globals.ink

{JOINED_ENEMY==0: ->on_return}
{MET_KING>0: ->met_before}

...
Ah, the messenger! You are finally here!
How do I look? "Younger than ever", is the only response I would accept.
+ [Younger than ever] I think so too. Thank you. Anyway...
-I need you to deliver this message to our neighbours. We cannot afford to be enemies anymore.
We don't want any trouble from them. Call it a truce?
If you gather any information from the enemy, bring them directly to me. I've heard the commander is gate-keeping all information. I should handle that before it...
By the way, Steve has joined us as an intern for the messaging department. Go find him and take him with you for this journey.
+ [I'm fine on my own.] Your king commands, Steve goes with you. Your hesitation to follow orders is noted, messenger.
+ [Sure! Good to have some young blood in our dying department.]
+ [It's dangerous out there, can I take a guard instead?] No, the commander controls the guards. It's better if it's only you and Steve.

- Be on your way now. Be safe through the treacherous terrains. May the divine be with you and Steve.
~MET_KING=1
-> END

=met_before
{~What are you still doing here?|Get going.|Be on your way now, messenger.}
-> END


=on_return
{KING_ESCAPED==0: Get back to your work->END}
{KING_ESCAPED>0: ->END}
...
Hello, messenger! You are back!
Did you deliver the message to the other town leader?
+ [ Yes, I did ]
+ [ No, I couldn't reach the leader ] That's a shame
    We can try again later.
    You haven't been very useful. Get back to your postal deliveries now
    ->END
-What did he say?
+ [ He is preparing for an attack on our town ]
    My goodness! I had a hunch but I am not prepared for this
    Messenger, step out of the room for a bit. I need to collect my thoughts before calling a meeting.
    ~KING_ESCAPED=1
+ [ He will consider our request to bring peace ]
    That is wonderful news. Thank you for your service to this town. You are dismissed.
    ~KING_ESCAPED=0
-
->END