INCLUDE ../globals.ink

The messenger, I presume. Greetings!
+ [Yes. Thank you, sir.]
-What news do you come bearing?
+ [I come with a message from my king]
++[He wants to end the enemity between our two towns and build a friendship.]
-Ha ha ha
Does this friendship include sharing of resources? Like Xurinium?
+ [I am unaware of what you are speaking, sir] A faithful messenger but a bad liar
+ [I don't think it does]
- Tell me messenger, why do you think your proud king wants to be friends now?
+ [He truly believes this could bring peace in both the towns]
+ [I do not know, sir]
-The children in our town have been victim to a new kind of sickness. Our maesters have told that even a small amount of Xurinium would help them formulate a medicine for this evil disease.
Your king knows all of this and yet denies having any Xurinium
+ [Maybe he really doesn't have any] Do you really believe that?
+ [How do you know he has any Xurinium?]
-Your dear friend, Steve, our spy has found out that your king indeed has some Xurinium
Your narcissistic king knows that we would attack and take it from him so he now wants to play political games
->exposition_choices
=exposition_choices
* [I saw your army preparing. You are calling for an attack on my town?] ->attack_reveal
* [Steve became a spy?] He always was our spy. He understands our goodwill in uniting both the towns.
    I should thank you for bringing him safely back to us with all the details he had collected. Your commander was making it hard for Steve to contact us.
* [It's a lot to process] Take your time, messenger.
- ->exposition_choices

=attack_reveal
We are prepared to march before dawn.
+ [So are you going to imprison or kill me, then?]
    Neither. You are a messenger. We don't hurt messengers here. Infact we would let you can go immediately.
    ++ [You know that our army will be alerted if I do not return within the day] Well, that too.
    Above all, we do not want a clash. We trust the inherent good in your people and believe they would turn on their evil king as they realise we are arriving.
    We have a much larger army than your town and would win anyway but we prefer to not hurt anyone.
    So if you do not alert your leaders, we will only look to seize the town and oust the king.
    But if you alert your leaders and they prepare a defence, we would have no choice but to fight and defeat them.
    So the lives of your soldiers are in your hands now, messenger. I trust you to make the right choice and keep all you might've seen and heard here a secret.
+ [Why did your reveal all this to me?] Because I need your help with creating a map of your town. Who'd know the paths better than a seasoned local messenger?
    So are you willing to be on the right side of history and help me?
+ [ I hate living under an incompetent selfish king. How can I help?] I need help creating a map of your town. Who'd know the paths better than a seasoned local messenger?
-
+ [ I'd be happy to help ]
    I could tell that you are a smart one.
    Our commander would need the areas where the influential families live - especially the ones that suport the king.
    We will need to stop them from helping the king escape while we seize the palace
    ~JOINED_ENEMY=1
+ [ I cannot betray my town. I'd like to leave now ]
    As you wish, messenger. I'll arrange for a guard to safely drop you back.
    ~JOINED_ENEMY=0
-

->END