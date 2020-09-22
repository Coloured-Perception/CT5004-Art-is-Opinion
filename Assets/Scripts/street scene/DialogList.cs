using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores the text options that appear in StreetScene
/// </summary>
public class DialogList : MonoBehaviour {
	public List<string> dogNames, femaleFirstNames, maleFirstNames, surnames, greeting, speech, request, dogSpeech;

	/// <summary>
	/// add whatever you like here 
	/// put end of sentence punctuation at the end of every addition unless theyre names
	/// do not put spaces after the punctuation
	/// search the code in case its already there
	/// list names in groups of 5 or 10
	/// 
	/// The general layout is:
	/// first name + lastname
	/// greet + talk + ask
	/// or
	/// animal
	/// animalTalk
	/// </summary>
	private void Awake() {

		// 20
		string[] women = { "Sharon", "Maize", "Bethany", "Claire", "Amy", "Jannett", "Sara", "Abby", "Nia", "Zula",
		"Sofia", "Justine", "Ellie", "Linda", "Ava", "Lucy", "Jasmine", "Holly", "Amahle", "Mary" };
		femaleFirstNames.AddRange(women);

		// 10
		string[] men = { "Dan", "Steve", "Newman", "Tom", "Sam", "Faraji", "Hamid", "Al", "Paul", "Wade" };
		maleFirstNames.AddRange(men);

		// 10
		string[] lastNames = { "Beck", "Allan", "Chase", "Haims", "Johnson", "Ivers", "Lindon", "Mills", "Otieno", "Steed" };
		surnames.AddRange(lastNames);

		// 12 (may change punctuation at random between . and !)
		string[] greet = { "Hi.", "Hello.", "Hey.", "Wadup!", "Heya!", "Ooh.", "Yo.", "Greetings.", "Salutations.", "Um...",
			"Feast your eyes on my being!", "Æ¾ѾٻḖ₥╢ﬗﻹ!", "Woah!", "Wow",  };
		greeting.AddRange(greet);

		// 10
		string[] talk = { "How you doing?", "Everything ok?", "You seem nice.", "Isn't it nice today.", "You do paintings?",
			"I'd make a beautiful muse.", "I saw you staring.", "You free right now?", "Your stuff looks cool.", "I would love to be an artist." };
		speech.AddRange(talk);

		// 10
		string[] ask = { "Could you draw my picture?", "Would you mind drawing me?", "Can i have my picture done?", "Could you draw me?", "Would you draw me?",
			"Can you draw me?", "Could you do my portrait?", "Will you draw my portrait?", "You must complete my portrait.", "Can you paint me?" };
		request.AddRange(ask);

		// 3
		string[] dogs = { "Dexter", "Turbo", "Perrtio" };
		dogNames.AddRange(dogs);

		// 3
		string[] dogTalk = { "Woof woof ruff.", "Grrr wrufff.", "Rrr woof." };
		dogSpeech.AddRange(dogTalk);
	}
}
