import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

/**
 * The TicTacToeServer class represents a server for a TicTacToe game. It allows
 * two clients to connect to it and play a game of TicTacToe.
 * 
 * @author pkf20
 * @version 2.0
 */
public class TicTacToeServer implements Runnable {
	private static TicTacToeServer server;
	private boolean isRunning;
	private Socket sockPlayer1;
	private Socket sockPlayer2;
	private PrintWriter writerPlayer1;
	private PrintWriter writerPlayer2;
	private BufferedReader readerPlayer1;
	private BufferedReader readerPlayer2;
	private TicTacToe game;

	/**
	 * The main method creates an instance of the TicTacToeServer class and calls
	 * its go() method to start the server.
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		server = new TicTacToeServer();
		server.go();
	}

	/**
	 * The go() method initializes the game object and opens a server socket to
	 * listen for incoming client connections.
	 */
	public void go() {
		game = new TicTacToe();
		try {
			ServerSocket serverSock = new ServerSocket(5000);

			System.out.println("Server is running...");

			server.sockPlayer1 = serverSock.accept();
			server.writerPlayer1 = new PrintWriter(server.sockPlayer1.getOutputStream(), true);
			server.readerPlayer1 = new BufferedReader(new InputStreamReader(server.sockPlayer1.getInputStream()));

			server.sockPlayer2 = serverSock.accept();
			server.writerPlayer2 = new PrintWriter(server.sockPlayer2.getOutputStream(), true);
			server.readerPlayer2 = new BufferedReader(new InputStreamReader(server.sockPlayer2.getInputStream()));

			Thread player1 = new Thread(server);
			Thread player2 = new Thread(server);
			player1.setName("player1");
			player2.setName("player2");

			isRunning = true;

			player1.start();
			player2.start();

			player1.join();
			player2.join();
			serverSock.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * The run() method is the main entry point for the server thread. It handles
	 * the communication with the clients, by reading the input from the clients and
	 * processing it. The method runs in a loop until the game is over or one of the
	 * clients disconnects.
	 */
	public void run() {
		boolean isPlayer1 = (Thread.currentThread().getName().equals("player1"));

		String response;

		while (isRunning) {
			try {
				response = read(isPlayer1);
				process(isPlayer1, response);
			} catch (IOException e) {
				writeToPlayers("end", "Game Ends. One of the players left.");
				isRunning = false;
			}
		}
	}

	private void process(boolean isPlayer1, String response) throws IOException {
		if (response.equals("ok"))
			return;
		int position = Integer.parseInt(response);
		boolean valid = game.makeMove(isPlayer1, position);
		String player;
		if (isPlayer1) {
			player = "X";
		} else {
			player = "O";
		}
		if (valid) {
			writeToPlayers("move", player + ":" + position);
			int check = game.checkEndGame();
			switch (check) {
			case 0: {
				if (isPlayer1) {
					writeToPlayer1("turn", "Valid move, wait for you opponent.");
					writeToPlayer2("turn", "Your opponent has moved, now is your turn.");
				} else {
					writeToPlayer1("turn", "Your opponent has moved, now is your turn.");
					writeToPlayer2("turn", "Valid move, wait for you opponent.");
				}
				break;
			}
			case 1: {
				writeToPlayer1("end", "Congratulations. You Win.");
				writeToPlayer2("end", "You lose.");
				isRunning = false;
				break;
			}
			case 2: {
				writeToPlayer1("end", "You lose.");
				writeToPlayer2("end", "Congratulations. You Win.");
				isRunning = false;
				break;
			}
			case 3: {
				writeToPlayers("end", "Draw.");
				isRunning = false;
				break;
			}
			default:
				throw new IllegalArgumentException("Unexpected value: " + Integer.toString(check));
			}
		} else {
			writeToPlayers("invalid", "");
		}
	}

	private void writeToPlayers(String situation, String message) {
		writeToPlayer1(situation, message);
		writeToPlayer2(situation, message);
	}

	private void writeToPlayer1(String situation, String message) {
		writerPlayer1.println(situation);
		writerPlayer1.println(message);
	}

	private void writeToPlayer2(String situation, String message) {
		writerPlayer2.println(situation);
		writerPlayer2.println(message);
	}

	private String read(boolean isPlayer1) throws IOException {
		String response;
		if (isPlayer1)
			response = readerPlayer1.readLine();
		else
			response = readerPlayer2.readLine();
		return response;
	}

}
