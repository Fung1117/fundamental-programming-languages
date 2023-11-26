/**
 * Represents a playing card in a deck of cards.
 * 
 * @author King Fung Pun
 * @version 1.0
 * @since 2023-04-18
 */
public class Card {
	private int value;
	private int suit;
	private boolean isSpecial;

	/**
	 * Create a new Card object with the specified value, suit, and special.
	 * 
	 * @param value     the numeric value of the card
	 * @param suit      the suit of the card
	 * @param isSpecial whether or not the card is a special card
	 */
	Card(int value, int suit, boolean isSpecial) {
		this.value = value;
		this.suit = suit;
		this.isSpecial = isSpecial;
	}

	/**
	 * @return integer the value of the card
	 */
	public int getValue() {
		return value;
	}

	/**
	 * @return boolean whether or not the card is a special card.
	 */
	public boolean isSpecial() {
		return isSpecial;
	}

	/**
	 * @return String the file path for the image of the card
	 */
	public String getImagePath() {
		return "Images/card_" + suit + value + ".gif";
	}
}
