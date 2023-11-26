/**
 * 
 * The TicTacToe class represents the game logic for a game of TicTacToe. It
 * keeps track of the current state of the game, including the state of the game
 * board, whose turn it is, and the total number of turns played.
 * 
 * @author pkf20
 * @version 2.0
 */
public class TicTacToe {
	private boolean isPlayer1Turn;
	private String[] board = { "", "", "", "", "", "", "", "", "" };
	private int turn;

	/**
	 * The default constructor initializes the game state to the start of a new
	 * game. The first player to move is player 1, and the game board is empty.
	 */
	TicTacToe() {
		this.isPlayer1Turn = true;
		this.turn = 0;
	}

	/**
	 * * The checkEndGame() method checks if the game has ended. It returns an
	 * integer value representing the state of the game:
	 * 
	 * 0: the game is not over yet
	 * 1: player 1 has won 
	 * 2: player 2 has won 
	 * 3: the game has ended in a draw
	 * 
	 * @return an integer value representing the state of the game
	 */
	public int checkEndGame() {
		for (int i = 0; i < 9; i = i + 3) {
			if (board[i] == board[i + 1] && board[i + 1] == board[i + 2] && !board[i].isEmpty()) {
				if (board[i].equals("X")) {
					return 1;
				}
				return 2;
			}
		}
		for (int i = 0; i < 3; i++) {
			if (board[i] == board[i + 3] && board[i + 3] == board[i + 6] && !board[i].isEmpty()) {
				if (board[i].equals("X")) {
					return 1;
				}
				return 2;
			}
		}

		if (board[0] == board[4] && board[4] == board[8] && !board[4].isEmpty()) {
			if (board[4].equals("X")) {
				return 1;
			}
			return 2;
		}

		if (board[2] == board[4] && board[4] == board[6] && !board[4].isEmpty()) {
			if (board[4].equals("X")) {
				return 1;
			}
			return 2;
		}

		if (turn == 9)
			return 3;

		return 0;
	}

	/**
	 * The makeMove() method makes a move in the game.
	 * 
	 * @param isPlayer1: indicating if the move is being made by player 1
	 * @param position: the position of the move in the TicTacToe
	 * @return true if the move was valid, false otherwise
	 */
	public synchronized boolean makeMove(boolean isPlayer1, int position) {
		if (!isPlayerTurn(isPlayer1))
			return false;

		String player;
		if (isPlayer1) {
			player = "X";
		} else {
			player = "O";
		}
		board[position] = player;
		turn += 1;
		isPlayer1Turn = !isPlayer1Turn;
		return true;
	}

	private boolean isPlayerTurn(boolean isPlayer1) {
		return (isPlayer1 && isPlayer1Turn) || (!isPlayer1 && !isPlayer1Turn);
	}
}
