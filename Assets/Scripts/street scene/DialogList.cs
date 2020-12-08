using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores the text options that appear as spoken text
/// </summary>
public class DialogList : MonoBehaviour {
	public List<string> tutorial, dogNames, femaleFirstNames, maleFirstNames, surnames, greeting, speech, request, dogSpeech;

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
	/// 
	/// do not add or remove strings in the tutorial
	/// the strings can be edited but the amount and the order cannot be changed without
	/// edits being made to tutorial manager and tutorial dialogue manager 
	/// </summary>
	private void Awake() { /// on load in, add text that say which button is the select button e.g. space bar 

		/// Tutorial -------------------------------------------------------------
		string[] explanations = {
			///intro
			" ", "Hey!", "Wake up!", " ", "What do you thing you're doing sleeping on the job? You're meant to be a security guard! Look around, all the paintings have been stolen!", " ",
			"The gallery is meant to open soon, what are we going to do?", "You have to replace some of these artworks, just some still lifes will do. I'll just say the rest of the rooms are under construction.",
			"Do you know how to paint?", "Brilliant, come find me in my office if you want any help. Everything you need is in the restricted area.", "Ok, let me show you how.",
			/// how to paint banana 
			"First look at the canvas and select it.", " ",  "Great, here I'll arrange some table top still lifes for you to draw. For now lets stick with this banana.", " ",
			" ", "Here is where you paint.", "In front of you is the banana and on the left there is a canvas. To paint, you can either trace the bananas shape with your eyes or draw freestyle on the canvas.",
			"First try looking at the banana and pressing your select button", "Great, can you see the canvas in the corner? thats what youve painted so far. to have a better look press the button at the top",
			"you can also paint directly onto the canvas, have a go now", "if you want to return to looking at the big picture of the banana you can click on the button at the top again.",
			"To the right you can choose to clear the canvas with the paper icon [ ] or complete the painting with the tick icon [ ].",
			"When you decide the paintings done you will normally have the option to save the painting or throw it away. For now though we really need a few paintings on these walls so I'll display whatever you make",
			"I'll leave you to it for now, if you need me select my icon.", " ",
			/// show banana painting 
			"Hey, not bad. I've displayed it over there. A few more paintings and we'll have this place ready.", "Not much colour or texture though. I think if i give you some better stuff you'd use it well. Lets try again with an apple.", " ",
			/// how to paint apple
			"let me just get you your colours", "Below there are various colours for you to choose from. Try changing the colour of your paint", "You can make some much better stuff with that. Oh also heres some different brushes.",
			"Change the brush shape by selecting the brush preview [] and change the size of the brush by clicking the arrows [] [].", "Ok you seem to know what youre doing now, give me a call if you need help.",  " ",
			/// show apple painting and map
			"That looks great next to the other painting. Well you probably know what youre doing now. Oh! i should give you your own painting space.", "Here ill show on your museum map where to go.",
			"We are currently in the still life display room as im sure you already know", "Over here ill set up some space for you to continue painting.", "Oh, i can set up a free style area as well so you can paint whatever your heart desires",
			"if you need me my office is here, just knock on the door.", "I've got to go now but I'll come check on you later. Bye", " ",
		
			/// banana painting help
			"Need a bit of help?", "Ok no worries.", "Sure. \nLook at the fruit or the canvas and hold your select button to paint. To change between the fruit image and the canvas, select the button at the top []. \nSelect the paper icon [] to clear the canvas and select the tick icon [] to finish painting. \nHope that helps.", " ",
			/// apple painting help 
			"Need a bit of help?", "Alright then.", "Of course. \nYour brush colour, size and shape is shown to the right. to change the colour of the brush, select a colour at the bottom of the screen. \nTo change the shape of the brush, select the brush preview and select a shape from the menu. to close the menu without choosing, simply select the preview again. \nTo change the size of the brush, select the arrows above and below the brush preview [] []. you can also change the brush size while the brush shape menu is open", " "
		};

		tutorial.AddRange(explanations);

		/// Names -------------------------------------------------------------
		/// 20
		string[] women = { "Sharon", "Maize", "Bethany", "Claire", "Amy", "Jannett", "Sara", "Abby", "Nia", "Zula",
		"Sofia", "Justine", "Ellie", "Linda", "Ava", "Lucy", "Jasmine", "Holly", "Amahle", "Mary" };
		femaleFirstNames.AddRange(women);

		/// 10
		string[] men = { "Dan", "Steve", "Newman", "Tom", "Sam", "Faraji", "Hamid", "Al", "Paul", "Wade" };
		maleFirstNames.AddRange(men);

		/// 10
		string[] lastNames = { "Beck", "Allan", "Chase", "Haims", "Johnson", "Ivers", "Lindon", "Mills", "Otieno", "Steed" };
		surnames.AddRange(lastNames);

		/// Street Sentences -------------------------------------------------------------
		/// 14 (may change punctuation at random between . and !)
		string[] greet = { "Hi.", "Hello.", "Hey.", "Wadup!", "Heya!", "Ooh.", "Yo.", "Greetings.", "Salutations.", "Um...",
			"Feast your eyes on my being!", "Æ¾ѾٻḖ₥╢ﬗﻹ!", "Woah!", "Wow",  };
		greeting.AddRange(greet);

		/// 10
		string[] talk = { "How you doing?", "Everything ok?", "You seem nice.", "Isn't it nice today.", "You do paintings?",
			"I'd make a beautiful muse.", "I saw you staring.", "You free right now?", "Your stuff looks cool.", "I would love to be an artist." };
		speech.AddRange(talk);

		/// 10
		string[] ask = { "Could you draw my picture?", "Would you mind drawing me?", "Can i have my picture done?", "Could you draw me?", "Would you draw me?",
			"Can you draw me?", "Could you do my portrait?", "Will you draw my portrait?", "You must complete my portrait.", "Can you paint me?" };
		request.AddRange(ask);

		/// 3
		string[] dogs = { "Dexter", "Turbo", "Perrtio" };
		dogNames.AddRange(dogs);

		/// 3
		string[] dogTalk = { "Woof woof ruff.", "Grrr wrufff.", "Rrr woof." };
		dogSpeech.AddRange(dogTalk);
		
		/// Reactions -------------------------------------------------------------
		 
	}
}
