/**
 * Represents a player in a card game.
 * 
 * @author King Fung Pun
 * @version 1.0
 * @since 2023-04-18
 */
public class Player {
	private Card[] handCards;

	/**
	 * create a new Player object with an empty hand of cards.
	 */
	Player() {
		this.handCards = new Card[3];
	}

	/**
	 * Draws three cards from the deck and adds them to the player's hand.
	 * 
	 * @param desk
	 */
	public void drawCard(Desk desk) {
		handCards[0] = desk.drawCard();
		handCards[1] = desk.drawCard();
		handCards[2] = desk.drawCard();
	}

	/**
	 * @return integer the number of special cards (JQK) in the player's hand
	 */
	public int countSpecialCards() {
		int count = 0;
		for (Card card : handCards) {
			if (card.isSpecial()) {
				count += 1;
			}
		}
		return count;
	}

	/**
	 * 
	 * @return integer the remainder of the sum of the non-special cards in the
	 *         player's hand when divided by 10
	 */
	public int calculateRemainder() {
		int total = 0;
		for (Card card : handCards) {
			if (!card.isSpecial()) {
				total += card.getValue();
			}
		}
		int remainder = total % 10;
		return remainder;
	}

	/**
	 * Replaces the card at the specified position in the player's hand with a new
	 * card.
	 * 
	 * @param desk     the deck of cards to draw from
	 * @param position the position of the handCards to replace
	 */
	public void replaceCard(Desk desk, int position) {
		handCards[position] = desk.drawCard();
	}

	/**
	 * @return an array of the cards currently in the player's hand
	 */
	public Card[] getHandCards() {
		return handCards;
	}
}
