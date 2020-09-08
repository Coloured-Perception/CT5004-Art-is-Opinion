using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour {
	public List<string> dogNames;
	public List<string> femaleFirstNames;
	public List<string> maleFirstNames;
	public List<string> surnames;
	public List<string> greeting;
	public List<string> speech;
	public List<string> request;
	public List<string> dogSpeech;

	/// <summary>
	/// add whatever you like here 
	/// put end of sentence punctuation at the end of every addition unless theyre names
	/// do not put spaces after the punctuation
	/// search the code in case its already there
	/// </summary>
	private void Awake() {
		string[] dogs = { "Dexter", "Turbo", "Perrtio", "Kira" };
		dogNames.AddRange(dogs);

		string[] women = { "Sharon", "Maize", "Bethany", "Claire", "Amy", "Jannett", "Sara", "Abby", "Nia", "Zula",
		"Sofia", "Justine", "Ellie", "Linda", "Ava", "Lucy", "Jasmine", "Holly", "Amahle", "Mary" };
		femaleFirstNames.AddRange(women);

		string[] men = { "Dan", "Steve", "Newman", "Tom", "Sam", "Faraji", "Hamid", "Al", "Paul", "Wade" };
		maleFirstNames.AddRange(men);

		string[] lastNames = { "Beck", "Allan", "Chase", "Haims", "Johnson", "Ivers", "Lindon", "Mills", "Otieno", "Steed" };
		surnames.AddRange(lastNames);

		//	string[] greet = { "Æ¾ѾٻḖ₥╢ﬗﻹ!" };
		string[] greet = { "Hi.", "Hello.", "Hey.", "Wadup!", "Ooh.", "Yo.", "Greetings.", "Salutations.", "Um...", "Feast your eyes on my being!" };
		greeting.AddRange(greet);

		string[] talk = { "How you doing?", "Everything ok?", "Death is inevitable.", "I see you Kane.", "You seem nice.", "Isn't it nice today.", "You do paintings?", "I'd make a beautiful muse.", "Did you know dogs have a habit of watching humans while they are on the toilet because when in the wild they watch other dogs going for a pee to watch for predators.", "I saw you staring." };
		speech.AddRange(talk);

		string[] ask = { "Could you draw my picture?", "Would you mind drawing me?", "Can i have my picture done?", "Could you draw me?", "Would you draw me?", "Can you draw me?", "Could you do my portrait?", "Will you draw my portrait?", "You must complete my portrait.", "Can you paint me?" };
		request.AddRange(ask);

		string[] dogTalk = { "Woof woof ruff.", "Grrr wrufff.", "Rrr woof." };
		dogSpeech.AddRange(dogTalk);
	}
}
