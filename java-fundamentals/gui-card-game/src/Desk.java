import java.util.ArrayList;
import java.util.Collections;

/**
 * Represents a deck of playing cards.
 * 
 * @author King Fung Pun
 * @version 1.0
 * @since 2023-04-18
 */
public class Desk {
	private ArrayList<Card> cards;

	/**
	 * Create a new Deck object and initializes it with a 52 cards.
	 */
	Desk() {
		cards = new ArrayList<Card>();
		for (int suit = 1; suit < 5; suit++) {
			for (int value = 1; value < 14; value++) {
				boolean isSpecial = value > 10;
				cards.add(new Card(value, suit, isSpecial));
			}
		}
	}

	/**
	 * @return integer the number of cards in the deck
	 */
	public int getNumCards() {
		return cards.size();
	}

	/**
	 * Shuffles the deck of cards.
	 */
	public void shuffle() {
		Collections.shuffle(cards);
	}

	/**
	 * Removes and returns the top card from the deck.
	 * 
	 * @return Card the top card from the deck
	 */
	public Card drawCard() {
		return cards.remove(0);
	}

}
