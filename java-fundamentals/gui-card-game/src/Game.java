/**
 * Represents a game of cards.
 * 
 * @author King Fung Pun
 * @version 1.0
 * @since 2023-04-18
 */
public class Game {
	private Player player;
	private Player dealer;
	private Desk desk;
	private int money;
	private int bet;
	private int replaceChance;

	/**
	 * create a new Game object with a player, dealer, deck, money, bet, and replace
	 * chance.
	 */
	Game() {
		player = new Player();
		dealer = new Player();
		desk = new Desk();
		money = 100;
		bet = 0;
		replaceChance = 2;
	}

	/**
	 * @return integer the amount of money the player has
	 */
	public int getMoney() {
		return money;
	}

	/**
	 * Starts a new game with the specified bet amount.
	 * 
	 * @param bet the amount of money the player is betting on the game
	 */
	public void startGame(int bet) {
		if (desk.getNumCards() < 8) {
			desk = new Desk();
		}
		desk.shuffle();
		player.drawCard(desk);
		dealer.drawCard(desk);
		this.bet = bet;
		replaceChance = 2;
	}

	/**
	 * @return an array of the cards currently in the player's hand
	 */
	public Card[] getPlayerCard() {
		return player.getHandCards();
	}

	/**
	 * @return an array of the cards currently in the dealer's hand
	 */
	public Card[] getDealerCard() {
		return dealer.getHandCards();
	}

	/**
	 * @return true if the gameOver (no money), false otherwise
	 */
	public boolean isGameOver() {
		return money <= 0;
	}

	/**
	 * @return true if the player can replace a card, false otherwise
	 */
	public boolean canReplaceCard() {
		return replaceChance > 0;
	}

	/**
	 * Replaces the card at the specified position in the player's hand with a new
	 * card.
	 * @param position the position of the handCards to replace
	 */
	public void replaceCard(int position) {
		if (replaceChance > 0) {
			player.replaceCard(desk, position);
			replaceChance--;
		}
	}

	/**
	 * 
	 * @return true if the player wins, false otherwise
	 */
	public boolean isPlayerWin() {
		int playerSpecial = player.countSpecialCards();
		int dealerSpecial = dealer.countSpecialCards();
		if (playerSpecial > dealerSpecial) {
			return true;
		} else if (playerSpecial < dealerSpecial) {
			return false;
		} else {
			int playerFaceValues = player.calculateRemainder();
			int dealerFaceValues = dealer.calculateRemainder();
			if (playerFaceValues > dealerFaceValues) {
				return true;
			} else {
				return false;
			}
		}
	}

	/**
	 * Updates the player's money based on whether or not they won the game.
	 * @param win true if the player won the game, false otherwise
	 */
	public void updatePlayerMoney(boolean win) {
		if (win) {
			money += bet;
		} else {
			money -= bet;
		}
	}
}
