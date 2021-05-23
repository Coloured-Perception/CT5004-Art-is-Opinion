using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores the text options that appear as spoken text
/// </summary>
public class DialogList : MonoBehaviour {
	public List<string> tutorial, dogNames, femaleFirstNames, maleFirstNames, surnames, greeting, speech, request, dogSpeech, reactions, reviewDescriptions;

	/// <summary>
	/// add whatever you like here 
	/// put punctuation at the end of every addition unless theyre names
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
	private void Awake() {  /// on load in, add text that say which button is the select button e.g. space bar 

							/// Tutorial -------------------------------------------------------------
		string[] explanations = {
			///intro  0
			" ", "Hey!", "Wake up!", " ", "What do you think you're doing sleeping on the job? You're meant to be a security guard! Look around, all the paintings have been stolen!", " ",
			"The gallery is meant to open soon, what are we going to do?", "You have to replace some of these artworks, starting with some still life depictions. I'll just say the rest of the rooms are under construction.",
			"Do you know how to paint?", "Brilliant, come find me in my office if you want any help. Everything you need is in the restricted area.", "Ok, let me show you how.", 
			/// how to paint banana   11  next 10
			"First look at the canvas and select it.", " ",  "Great, here, I'll arrange a table top still life for you to draw. For now let's stick with this banana.", " ",
			" ", "Here is where you paint.", "In front of you is the banana and on the left there is a canvas. To paint, you can either trace the banana's shape with your eyes or draw freestyle on the canvas.",
			"First try looking at the banana and pressing your select button.", "Great, can you see the canvas to the left? That's what you've painted so far. To have a better look press the button at the top.",
			"You can also paint directly onto the canvas, have a go now.", "If you want to return to looking at the big picture of the banana you can click on the button at the top again.",
			"To the left you can choose to clear the canvas with the bin icon or complete the painting with the tick icon.",
			"When you decide the painting is done you will normally have the option to save the painting or throw it away. For now though we really need a few paintings on these walls so I'll display whatever you make.",
			"I'll leave you to it for now, if you need me select my icon.", " ",
			/// show banana painting 
			"Hey, not bad. I've displayed it over there. A few more paintings and we'll have this place ready.", "Not much colour or texture though. I think if I give you some better stuff you'd use it well. Let's try again with an apple.", " ",
			/// how to paint apple
			"Let me just get you your colours.", "Below there are various colours for you to choose from. Try changing the colour of your paint.", "You can make some much better stuff with that. Oh also here are some different brushes.",
			"Change the size of the brush by clicking the arrows and change the brush shape by selecting the brush preview. Try clicking the brush prieview now.", "Ok you seem to know what youre doing now, give me a call if you need help.",  " ",
			/// show apple painting and map
			"That looks great next to the other painting. Well you probably know what you're doing now. Oh! I should give you your own painting space.", "Here I'll show on your museum map where to go.",
			"We are currently in the Still Life Display Room as I'm sure you already know.", "Everything you need for now will be in the Restricted Area here. You'll have option to paint more and change any settings.",
			"Over here I'll set up some space for you to continue painting Still Life works.", "Oh, I can set up a Free Style area as well so you can paint whatever your heart desires.",
			"If you need me my Office is here, just knock on the door.", "I've got to go now but I'll come check on you later. Bye.", " ",
		
			/// banana painting help
			"Need a bit of help?", "Ok no worries.", "Sure. \nLook at the fruit or the canvas and hold your select button to paint. To change between the fruit image and the canvas, select the button at the top.",
			"Select the bin icon in the top left to clear the canvas and select the tick icon to the left to finish painting. \nHope that helps.", 
			/// apple painting help    47
			"Need a bit of help?", "Alright then.", "Of course. \nYour current brush colour, size and shape is shown to the right. To change the colour of the brush, select a colour at the bottom of the screen.",
			"To change the shape of the brush, select the brush preview and choose a shape from the menu. To close the menu without choosing, simply select the preview again. \nTo change the size of the brush, select the arrows above and below the brush preview. You can also change the brush size while the brush shape menu is open.", " ",

			/// unlock portrait drawing   52
			"Hey, you've been doing really well, this gallery is filling up fast.", "Do you fancy an extra bit of viriatey? I'm going to unblock the portrait display room for you.", "The new portrait room is located here on the map, next to the still life room.",
			"If you want to find some people to draw portraits of I suggest you go for a walk out the entrance doors. You can always return by turning around and walking back towards the gallery.",
			"Your muses are sure to have some comments to say about their portraits as well. Everyone has their own opinion on art, but you get to decide if you want to listen.", "Maybe experiment with your style if you want to get some different reactions, but ultimately do whatever you think feels right.",
			"Ok, I need to go. If you need me I'll be in my office.", " ",

			/// visiting office
			"Hey, how are you doing? Do you need some help painting?", "Ok, have fun.", " ", " ", " ",
			//"Do you want to learn about the gallery?", "No ok, do you need help with the settings?", " ",
			///help painting   65
			"Sure, I'll show you what you need to know",
			"To paint you can look at the subject or at the canvas and hold your select button to paint. To change between looking at the subject and the canvas select the button at the top.",
			"To clear the painting select the bin icon in the top left, to finish the painting select the tick icon to the left. If you select the tick you will be given the option to save the painting, throw it away, or continue working on it.",
			"To change the colour of the brush select one of the colours below. The current brush colour can be seen in the brush preview to the right.",
			"To change the size of the brush select the arrows to the right. \nTo change the shape of the brush, select the brush preview and choose a shape from the menu. To close the menu without choosing, simply select the preview again. \nYou can also change the brush size while the brush shape menu is open.",
			"To start painting in the first place select a canvas in the still life section here...", "... or a canvas in the free draw section here.", "You can also take a walk out the entrance door to find people to paint portraits of. You can return any time by turning round and walking back.",
			"Peoples reactions to your art may depend on various factors such as the colours used or the time it took. Everyone has a different opinion. Experiment, but ultimately do whatever you think feels right.",
			"And that's it. If you need me again I'll be in my office.", " "
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

		/// 5
		string[] dogs = { "Dexter", "Turbo", "Perrtio", "Kira", "Barney" };
		dogNames.AddRange(dogs);

		/// Street Sentences -------------------------------------------------------------
		/// 14 (may change punctuation at random between . and !)
		string[] greet = { "Hi.", "Hello.", "Hey.", "Wadup!", "Heya!", "Ooh.", "Yo.", "Greetings.", "Salutations.", "Um...",
			"Feast your eyes on my being!", "Æ¾ѾٻḖ₥╢ﬗﻹ!", "Woah!", "Wow." };
		greeting.AddRange(greet);

		/// 10
		string[] talk = { "How you doing?", "Everything ok?", "You seem nice.", "Isn't it nice today.", "You do paintings?",
			"I'd make a beautiful muse.", "I saw you staring.", "You free right now?", "Your stuff looks cool.", "I would love to be an artist." };
		speech.AddRange(talk);

		/// 10
		string[] ask = { "Could you draw my picture?", "Would you mind drawing me?", "Can I have my picture done?", "Could you draw me?", "Would you draw me?",
			"Can you draw me?", "Could you do my portrait?", "Will you draw my portrait?", "You must complete my portrait.", "Can you paint me?" };
		request.AddRange(ask);


		/// 3
		string[] dogTalk = { "Woof woof ruff.", "Grrr wrufff.", "Rrr woof." };
		dogSpeech.AddRange(dogTalk);

		/// Reactions & Descriptions -------------------------------------------------------------
		/// 11
		string[] defaultReactions = { "Watching you work was interesting.", "We done?", "Let me see!", "You Finished?", "Well just look at that.", "Can I look?", "Hey, Dat's pretty Gud!", "ooh! Pretty!", "I inspire you that much huh?", "Is that meant to be me?", "Watching you paint was weird..." };
		reactions.AddRange(defaultReactions);

		/// 11
		string[] defaultDescriptions = { "I like the range of colours used", "That's a perfect representation of how I feel right now.", "Maybe I should learn how to paint too, can't be that hard!", "It really does say a thousand words.", "This says closer to 5 words but alright", "Love the quick sweeping lines.", "Theres so much energy in this piece!", "Love the details you added there.", "I see you’ve studied impressionism.", "You really got my likeness!", "That's a bold style." };
		reviewDescriptions.AddRange(defaultDescriptions);

		// More reactions and descriptions added depending on how long player takes in paint scene
		int paintTime = PlayerPrefs.GetInt("PaintTime");
		if (paintTime >= 300) {

			/// 5
			string[] longTimeReactions = { "What Took So Long?!?!?!?!", "SlowPoke!", "We done now?", "Thanks for spending the time on this.", "You really took your time!" };
			reactions.AddRange(longTimeReactions);

			/// 2
			string[] longTimeDescriptions = { "Can't put a time on art.", "love that you spent the time to add detail." };
			reviewDescriptions.AddRange(longTimeDescriptions);
		} else if (paintTime <= 60) {

			/// 3
			string[] shortTimeReactions = { "That was quick!", "You're already done!?", "That wasn't long!" };
			reactions.AddRange(shortTimeReactions);

			/// 5
			string[] shortTimeDescriptions = { "Not much effort here", "You finished that in the time it takes for me to finish a sketch!", "You've obviously had a lot of practice to be that quick", "I would have preferred if you put a bit more of an effort in", "It's lacking some details... but still looks cool!" };
			reviewDescriptions.AddRange(shortTimeDescriptions);
		}

		// Colour related reactions & descriptions
		int coloursUsed = PlayerPrefs.GetInt("ColoursUsed");
		if (coloursUsed == 1) {

			/// 1
			string[] monochromeReactions = { "Monochrome? Nice choice." };
			reactions.AddRange(monochromeReactions);
		} else if (coloursUsed > 3 && coloursUsed < 9) {

			/// 1
			string[] someColoursReactions = { "ooooh! The colours look great!" };
			reactions.AddRange(someColoursReactions);

			string[] someColoursDescriptions = { "Love the use of colour." };
			reviewDescriptions.AddRange(someColoursDescriptions);
		} else if (coloursUsed == 10) {

			/// 3
			string[] allColoursReactions = { "Whoa! That's very bright!", "You had fun with that one, didn't you?", "I can see the whole rainbow!" };
			reactions.AddRange(allColoursReactions);

			/// 1
			string[] allColoursDescriptions = { "This is a very dramatic use of colour." };
			reviewDescriptions.AddRange(allColoursDescriptions);
		}

		// Brush shape related reactions & descriptions
		int brushesUsed = PlayerPrefs.GetInt("BrushesUsed");
		if (brushesUsed == 1) {

			/// 1
			string[] oneBrushReactions = { "You did all that with one brush?" };
			reactions.AddRange(oneBrushReactions);

			/// 2
			string[] oneBrushDescriptions = { "I love the consistent texture.", "Very little contrast in textures there." };
			reviewDescriptions.AddRange(oneBrushDescriptions);
		} else if (brushesUsed > 5 && brushesUsed < 45) {

			/// 1
			string[] someBrushesUsedReactions = { "You got some cool textures there," };
			reactions.AddRange(someBrushesUsedReactions);

			/// 2
			string[] someBrushesUsedDescriptions = { "Some nice variations going on there.", "Those bits you done with the other brush really sticks out." };
			reviewDescriptions.AddRange(someBrushesUsedDescriptions);
		} else if (brushesUsed == 47) {

			/// 2
			string[] allBrushesReactions = { "Wow! There's a lot going on there.", "I can see every brush stroke!" };
			reactions.AddRange(allBrushesReactions);

			/// 2
			string[] allBrushesDescriptions = { "Everything's getting lost in the range of textures.", "The textures are so vibrant." };
			reviewDescriptions.AddRange(allBrushesDescriptions);
		}
	}
}