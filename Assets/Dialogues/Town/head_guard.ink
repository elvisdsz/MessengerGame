INCLUDE ../globals.ink
~LOYALTY_TEST_RESULT=1
{LOYALTY_TEST_RESULT==-1: ->END}

Who goes there!
Ah, it's the messengers
We've been expecting you
Before you go any further, I need to talk to you both... separately.

{LOYALTY_TEST_RESULT==1: ->you_choose | ->steve_betrays}

=steve_betrays
Messenger, we have got to know about your intentions to help the enemy. We cannot let you go any further.
->END

=you_choose
We have got know that there's a spy between us
Someone is gathering information for the enemy town
Did you get to talk to Steve much?
+ [Yes, I did]
    Brilliant! What do you think of him?
    Did he mention any weird knowledge about the enemies? Do you think he could be the spy?
+ [Not much really]
    I will trust your instincts on this
    From all your interactions, could he be the spy we are looking for?
-
+ [Steve definitely is the spy. He even tried to convince me to defect.]
    I had a doubt! Thanks!
    You can carry on with your job
    We will take care of Steve
+ [Steve is a pretty chill non-spy guy. I can vouch for him.]
    Hmm.. Okay
    You both can go ahead
-
->END