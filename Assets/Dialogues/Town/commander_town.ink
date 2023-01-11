INCLUDE ../globals.ink
{TUT_COMPLETE<0: ->END}
...
{JOINED_ENEMY==0: ->on_return}
{MET_COMMANDER>0: ->met_before}

->meet

=met_before
{~I believe you have a delivery to make.|Get on with your job now.|Come back when you have some information.}
-> END

=meet
Good to see you here, messenger.
{MET_KING<1: I believe the king has called for you. You should visit the palace and see what it's about. | I have come to know that the king has asked you to deliver a truce message to the enemies, without consulting the army. Very suspicious, isn't it?}
I have information that the king might be hiding some Xurinium.

+ [I have heard that rumour too] If it's true, we cannot let such a self-centered man have the power that he does. We need a better governance, a more systematic one run by principles.

+ [What's Xurinium?]
    It's the mythical metal that's supposed to be stronger than any substance. Weapons cast with even traces of Xurinium are ten times stronger and durable.
    ++ [Why would the king hide it, if it can help his own army?]
    Among other things, it is also believed to have youth regeneration properties. I have a feeling our vain king is holding it for his self-obsessions.
 
-If you happen to gather any important information, come to me first. We cannot trust the king with the welfare of this town, anymore.
The incompetency and greed of that man is ruining our town.
~MET_COMMANDER=1
->END

=on_return
{COMMANDER_INFORMED>-1: ->END}
I see that you are back from your job
What information do you have for me?
+[Nothing really]
    Maybe, no news from the enemy is good news
    I can spend some time on planning what we could do about the incompetency of leadership in this town
    ~COMMANDER_INFORMED=0
+[The enemy will attack today]
    Are you sure?
    Then we must prepare. Go back to a safer place now. I'll take up from here
    {KING_ESCAPED!=1: 
        ~COMMANDER_INFORMED=1
    }
    {KING_ESCAPED==1:
        ~COMMANDER_INFORMED=2
    }
-
->END