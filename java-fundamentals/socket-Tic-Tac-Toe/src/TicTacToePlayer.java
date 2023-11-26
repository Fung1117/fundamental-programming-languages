import java.io.*;
import java.net.*;

import javax.swing.*;
import javax.swing.border.Border;

import java.awt.*;
import java.awt.event.*;

/**
 * This class implements a player for the TicTacToe game. It connects to a
 * server and communicates with it to play the game.
 * 
 * @author pkf20
 * @version 2.0
 */
public class TicTacToePlayer implements Runnable {
	Socket sock;
	static JFrame frame;
	static PrintWriter writer;
	static BufferedReader reader;
	static JLabel label_info;
	static JButton[] buttons;

	/**
	 * Connects to the server and starts playing the game.
	 */
	public void go() {
		try {
			// setting up socket
			sock = new Socket("127.0.0.1", 5000);
			InputStreamReader streamReader = new InputStreamReader(sock.getInputStream());
			reader = new BufferedReader(streamReader);
			writer = new PrintWriter(sock.getOutputStream(), true);

			// enabling the labels
			for (int i = 0; i < buttons.length; i++) {
				buttons[i].setEnabled(true);
			}

			// start conversation with server
			String type, arg;
			while (true) {
				type = reader.readLine();
				arg = reader.readLine();

				// received message
				if (type.equals("move"))
					placeXO(arg);

				else if (type.equals("turn"))
					switchPlayer(arg);

				else if (type.equals("end")) {
					JOptionPane.showMessageDialog(frame, arg);
					break;
				}

			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}

	}

	/**
	 * Implements the Runnable interface. Starts the game by calling the go()
	 * method.
	 */
	public void run() {
		this.go();
	}

	private void switchPlayer(String str) {
		if (str.startsWith("Valid")) {
			for (int i = 0; i < buttons.length; i++) {
				buttons[i].setEnabled(false);
			}
		} else {
			for (int i = 0; i < buttons.length; i++) {
				buttons[i].setEnabled(true);
			}
		}
		label_info.setText(str);
	}

	private void placeXO(String str) {

		String[] info = str.split(":");
		String player = info[0];
		int position = Integer.parseInt(info[1]);
		if (player.equals("X")) {
			buttons[position].setForeground(Color.GREEN);
		} else {
			buttons[position].setForeground(Color.RED);
		}
		buttons[position].setText(player);
		return;
	}

	/**
	 * Creates the GUI and starts the game.
	 * 
	 * @param args
	 */
	public static void main(String[] args) {

		// Set up the menu bar
		JMenuBar menuBar = new JMenuBar();
		JMenu menu_control = new JMenu("Control");
		JMenu menu_help = new JMenu("Help");
		JMenuItem menuItem_exit = new JMenuItem("Exit");
		JMenuItem menuItem_instruction = new JMenuItem("Instruction");
		menu_control.add(menuItem_exit);
		menu_help.add(menuItem_instruction);
		menuBar.add(menu_control);
		menuBar.add(menu_help);

		// set up the info
		label_info = new JLabel("Enter your player name...");

		JPanel GameArea = new JPanel();
		GameArea.setLayout(new GridLayout(3, 3));

		buttons = new JButton[9];

		Font font = new Font("Courier New", 1, 50);
		Border border = BorderFactory.createLineBorder(Color.BLACK, 1);

		for (int i = 0; i < 9; i++) {
			buttons[i] = new JButton();
			buttons[i].setText("");
			buttons[i].setFont(font);
			buttons[i].setBackground(Color.WHITE);
			buttons[i].setBorder(border);
			buttons[i].setEnabled(false);
			final int _i = i;
			buttons[i].addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					if (buttons[_i].getText().isEmpty()) {
						writer.println(_i);
					}
				}
			});
			GameArea.add(buttons[i]);
		}

		JPanel NamePanel = new JPanel();
		JTextField name_field = new JTextField(20);
		JButton submit_button = new JButton("Submit");
		NamePanel.add(name_field);
		NamePanel.add(submit_button);

		JPanel MainPanel = new JPanel();
		MainPanel.setLayout(new BorderLayout());

		MainPanel.add(label_info, BorderLayout.NORTH);
		MainPanel.add(GameArea, BorderLayout.CENTER);
		MainPanel.add(NamePanel, BorderLayout.SOUTH);

		// JFrame
		frame = new JFrame();
		frame.setContentPane(MainPanel);
		frame.setJMenuBar(menuBar);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setTitle("Tic Tac Toe");
		frame.setSize(400, 400);
		frame.setVisible(true);

		// action listeners
		menuItem_exit.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				frame.setVisible(false);
				frame.dispose();
			}
		});

		menuItem_instruction.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				JOptionPane.showMessageDialog(frame,
						"Some information about the game:\nCriteria for a valid move:\n-The move is not occupied by any mark.\n-The move is made in the player's turn.\n-The move is made within the 3 x 3 board.\nThe game would continus and switch among the opposite player until it reaches either one of the following conditions:\n-Player 1 wins.\n-Player 2 wins.\n-Draw");
			}
		});

		submit_button.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				name_field.setEnabled(false);
				submit_button.setEnabled(false);
				frame.setTitle("Tic Tac Toe-player: " + name_field.getText());
				label_info.setText("WELCOME " + name_field.getText());
				TicTacToePlayer client = new TicTacToePlayer();
				Thread T = new Thread(client);
				T.start();
			}
		});
	}

}
