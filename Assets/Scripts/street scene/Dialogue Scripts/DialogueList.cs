using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains arrays to what character names and conversations that are randomly generated
/// </summary>
public class DialogueList : MonoBehaviour {
	public List<string> dogNames;
	public List<string> femaleFirstNames;
	public List<string> maleFirstNames;
	public List<string> surnames;

	public List<string> greeting;
	public List<string> speech;
	public List<string> request;
	public List<string> dogSpeech;

	public List<string> reactions;
	public List<string> reviewDescriptions;

	/// <summary>
	/// add whatever you like here 
	/// put end of sentence punctuation at the end of every addition unless their names
	/// do not put spaces after the punctuation
	/// search the code in case its already there
	/// </summary>
	private void Awake() {
		// Characters

		string[] dogs = { "Dexter", "Turbo", "Perrtio", "Kira", "Barney" };
		dogNames.AddRange(dogs);

		string[] women = { "Sharon", "Maize", "Bethany", "Claire", "Amy", "Jannett", "Sara", "Abby", "Nia", "Zula",
		"Sofia", "Justine", "Ellie", "Linda", "Ava", "Lucy", "Jasmine", "Holly", "Amahle", "Mary" };
		femaleFirstNames.AddRange(women);

		string[] men = { "Dan", "Steve", "Newman", "Tom", "Sam", "Faraji", "Hamid", "Al", "Paul", "Wade" };
		maleFirstNames.AddRange(men);

		string[] lastNames = { "Beck", "Allan", "Chase", "Haims", "Johnson", "Ivers", "Lindon", "Mills", "Otieno", "Steed" };
		surnames.AddRange(lastNames);


		// Paint request

		//	string[] greet = { "Æ¾ѾٻḖ₥╢ﬗﻹ!" };
		string[] greet = { "Hi.", "Hello.", "Hey.", "Wadup!", "Ooh.", "Yo.", "Greetings.", "Salutations.", "Um...", "Feast your eyes on my being!" };
		greeting.AddRange(greet);

		string[] talk = { "How you doing?", "Everything ok?", "You seem nice.", "Isn't it nice today.", "You do paintings?", "I'd make a beautiful muse." };
		speech.AddRange(talk);

		string[] ask = { "Could you draw my picture?", "Would you mind drawing me?", "Can i have my picture done?", "Could you draw me?", "Would you draw me?", "Can you draw me?", "Could you do my portrait?", "Will you draw my portrait?", "You must complete my portrait.", "Can you paint me?" };
		request.AddRange(ask);

		string[] dogTalk = { "Woof woof ruff.", "Grrr wrufff.", "Rrr woof." };
		dogSpeech.AddRange(dogTalk);


		// Reactions and descriptions
		string[] defaultReactions = { "Watching you work was interesting.", "We done?", "Let me see!", "You Finished?", "Well just look at that.", "Can I look?", "Hey, Dat's pretty Gud!", "ooh! Pretty!", "I inspire you that much huh?", "Is that meant to be me?", "Watching you paint was weird..." };
		reactions.AddRange(defaultReactions);

		string[] defaultDescriptions = { "I like the range of colours used", "That's a perfect representation of how I feel right now.", "Maybe I should learn how to paint too, can't be that hard!", "It really does say a thousand words.", "This says closer to 5 words but alright", "Love the quick sweeping lines.", "Theres so much energy in this piece!", "Love the details you added there.", "I see you’ve studied impressionism.", "You really got my likeness!", "That's a bold style." };
		reviewDescriptions.AddRange(defaultDescriptions);

		// More reactions and descriptions added depending on how long player takes in paint scene
		int paintTime = PlayerPrefs.GetInt("PaintTime");
		if (paintTime >= 300) {
			string[] longTimeReactions = { "What Took So Long?!?!?!?!", "SlowPoke!", "We done now?", "Thanks for spending the time on this.", "You really took your time!" };
			reactions.AddRange(longTimeReactions);

			string[] longTimeDescriptions = { "Can't put a time on art.", "love that you spent the time to add detail." };
			reviewDescriptions.AddRange(longTimeDescriptions);
		} else if (paintTime <= 60) {
			string[] shortTimeReactions = { "That was quick!", "You're already done!?", "That wasn't long!" };
			reactions.AddRange(shortTimeReactions);

			string[] shortTimeDescriptions = { "Not much effort here", "You finished that in the time it takes for me to finish a sketch!", "You've obviously had a lot of practice to be that quick", "I would have preferred if you put a bit more of an effort in", "It's lacking some details... but still looks cool!" };
			reviewDescriptions.AddRange(shortTimeDescriptions);
		}
	}
}