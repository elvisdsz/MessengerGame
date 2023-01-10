INCLUDE ../globals.ink
{TUT_COMPLETE<0: ->END}
{ MET_COMPANION<1: ->meet | ->test_loyalty}

=meet
...
Hi, I am Steve. Nice to meet you.
I am super excited to be on this delivery mission.
I am looking forward to experience the thrill of cross-kingdom messaging and learn more from you!
+ [I love your enthusiasm.]->chitchat
+ [Lesson \#1, Less talking, more walking]
    Yes, sir.
~MET_COMPANION=1
->END

=chitchat
Thanks! May I ask you why you became a messenger?  I heard it doesn't pay very well.
+ [It's not about the money, it's about sending a message.]
+ [The king had assigned everyone a duty during the last war.]
    And you didn't think of doing something else once the war was over?
    ++ [A messenger never abandons their post.]
- That's funny. Have to appreciate a messenger with a good delivery!
+ [I see, you are learning. Let's get going now. We need to reach before the sun sets.]
Yes, sir.
~MET_COMPANION=1
->END

=test_loyalty
{ LOYALTY_TEST_RESULT>-1: ->thank_for_saving}
...
What do you think of the king?
+ [Seems like a nice guy] Well, I've heard quite different
+ [He is a selfish old fool] He is, isn't he?
- I heard the people at the army camp say, the king has been hiding some magical metal for himself, which can help a lot of people.
+ [Yes, Xurinium.] The mythical metal!
    ++[The commander has info that the king might be hiding a part of it for himself]
        I feel the commander only wants it to make his cavalry stronger.
    ++[The king has been looking younger and more self-obsorbed lately]
        That would explain the reason for why he would hide that.
- And there is also some gossip that the leader from the other town wants to take it from the king and give it to all the people of both towns. Seems like the right thing, doesn't it?

What side would you take if you had a choice?
+ [I would help the other town leader. The common good must triumph.]
    Yes, it must. For a change, it's good to speak with someone who thinks rationally.
    ~LOYALTY_TEST_RESULT = 0
+ [Never would I betray my town and side with the enemies]
    Yes, same. I think it's the enemies themselves starting all these rumours.
    ~LOYALTY_TEST_RESULT = 1
-
->END

=thank_for_saving
{PLAYER_BETRAYED==0 && COMPANION_BETRAYED==0: ...| ->END}
I overheard the guard ask you about me. Thank you standing up for me.
Since you've been kind. I'd like to share a piece of advice
Don't trust any leaders. All the ones I have known have some hidden greed to amass power or wealth
They only care about themselves
+ [Well, thanks. Let's keep moving forward now]
    Since we are already close, I should tell you, I am working as a spy for this town leader. I truly believe in his vision. At least, I did
    Now I feel that every man who has tasted power only wants more of it
    Be careful in there. I'll put in a good word for you if it helps

+ [Shut up, Steve. Keep walking]
-
->END