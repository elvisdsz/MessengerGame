INCLUDE globals.ink

Hello, traveller!
What brings you here today?
{TEST_FLAG>0: I remember, you had chosen choice {TEST_FLAG} before. I ask again now...}
Is it the choice one or the choice two or it could might as well be choice three. Only you can tell.
+ [Choice number one]
    -> chosen(1)
+ [Choice number two which is a longer text]
    -> chosen(2)
+ [Choice number three is propbably the longest of them all with many words]
    -> chosen(3)

=== chosen(choiceNumber) ===
~ TEST_FLAG = choiceNumber
You chose choice {choiceNumber} but at this point it doesn't matter.
-> END