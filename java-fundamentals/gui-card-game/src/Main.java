import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;

public class Main {
	public static void main(String[] args) {
		//
		Game game = new Game();

		// Create six JLabels to display the card images
		JLabel label_Image1 = new JLabel();
		JLabel label_Image2 = new JLabel();
		JLabel label_Image3 = new JLabel();
		JLabel label_Image4 = new JLabel();
		JLabel label_Image5 = new JLabel();
		JLabel label_Image6 = new JLabel();

		// Create three JButtons to allow the player to replace their cards
		JButton btn_rpcard1 = new JButton("Replace Card 1");
		JButton btn_rpcard2 = new JButton("Replace Card 2");
		JButton btn_rpcard3 = new JButton("Replace Card 3");

		// Disable the "Replace Card" buttons
		btn_rpcard1.setEnabled(false);
		btn_rpcard2.setEnabled(false);
		btn_rpcard3.setEnabled(false);

		// Create a JLabel to display the text "Bet: $"
		JLabel label_bet = new JLabel("Bet: $");

		// Create a JTextField for the player to enter their bet
		JTextField txt_inputbet = new JTextField(10);

		// Create a JButton to start the game
		JButton btn_start = new JButton("Start");
		// Disable the start button
		btn_start.setEnabled(true);

		// Create a JButton to display the player's hand and determine the winner
		JButton btn_result = new JButton("Result");
		// Disable the result button
		btn_result.setEnabled(false);

		// Create a JLabel to prompt the player to place their bet
		JLabel label_money = new JLabel("Please place your bet! ");
		// Create a JLabel to display the player's current amount of money
		JLabel label_info = new JLabel("Amount of money you have: $" + game.getMoney());

		// create an image that represent a card back
		ImageIcon Image1 = new ImageIcon("Images/card_back.gif");

		// set six JLabels'icon card_back as the image
		label_Image1.setIcon(Image1);
		label_Image2.setIcon(Image1);
		label_Image3.setIcon(Image1);
		label_Image4.setIcon(Image1);
		label_Image5.setIcon(Image1);
		label_Image6.setIcon(Image1);

		JPanel MainPanel = new JPanel();
		JPanel DealerPanel = new JPanel();
		JPanel PlayerPanel = new JPanel();
		JPanel RpCardBtnPanel = new JPanel();
		JPanel ButtonPanel = new JPanel();
		JPanel InfoPanel = new JPanel();

		// add the image to the dealer panel
		DealerPanel.add(label_Image1);
		DealerPanel.add(label_Image2);
		DealerPanel.add(label_Image3);

		// add the image to the player panel
		PlayerPanel.add(label_Image4);
		PlayerPanel.add(label_Image5);
		PlayerPanel.add(label_Image6);

		// add the button to RpCardBtnPanel
		RpCardBtnPanel.add(btn_rpcard1);
		RpCardBtnPanel.add(btn_rpcard2);
		RpCardBtnPanel.add(btn_rpcard3);

		// add the label, text input, button start and button result to the button panel 
		ButtonPanel.add(label_bet);
		ButtonPanel.add(txt_inputbet);
		ButtonPanel.add(btn_start);
		ButtonPanel.add(btn_result);

		// add the label to the info panel
		InfoPanel.add(label_money);
		InfoPanel.add(label_info);

		MainPanel.setLayout(new GridLayout(5, 1));
		MainPanel.add(DealerPanel);
		MainPanel.add(PlayerPanel);
		MainPanel.add(RpCardBtnPanel);
		MainPanel.add(ButtonPanel);
		MainPanel.add(InfoPanel);
		
		// set the background color
		DealerPanel.setBackground(Color.green);
		PlayerPanel.setBackground(Color.green);
		RpCardBtnPanel.setBackground(Color.green);

		// create a menu
		JMenuBar menuBar = new JMenuBar();
		JMenu menu = new JMenu("Control");
		JMenuItem menuItem = new JMenuItem("Exit");
		menu.add(menuItem);
		menuBar.add(menu);

		JFrame frame = new JFrame();
		frame.setJMenuBar(menuBar);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().add(MainPanel);
		frame.setTitle("A Simple Card");
		frame.setSize(400, 700);
		frame.setVisible(true);

		// perform exit action
		menuItem.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				frame.setVisible(false);
				frame.dispose();
			}
		});

		// for user to start the game
		btn_start.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				String bet_txt = txt_inputbet.getText();
				// if user did not enter anything, wont run the game
				if (bet_txt.isEmpty()) {
					return;
				}
				int bet = Integer.parseInt(bet_txt);
				// check if bet is valid, show the error message if it is invalid
				if (bet <= 0) {
					JOptionPane.showMessageDialog(null, "invalid input: bet should be positive!");
					return;
				} else if (bet > game.getMoney()) {
					JOptionPane.showMessageDialog(null, "invalid input: not enough money!");
					return;
				}
				// start a new round game
				game.startGame(bet);

				// display message
				label_money.setText("Your current bet is: $" + txt_inputbet.getText());

				// get the player card
				Card[] playerCard = game.getPlayerCard();

				// get the image according to the corresponding card
				ImageIcon player_Image1 = new ImageIcon(playerCard[0].getImagePath());
				ImageIcon player_Image2 = new ImageIcon(playerCard[1].getImagePath());
				ImageIcon player_Image3 = new ImageIcon(playerCard[2].getImagePath());

				// set the image to the corresponding card_image
				label_Image4.setIcon(player_Image1);
				label_Image5.setIcon(player_Image2);
				label_Image6.setIcon(player_Image3);

				// disable the button
				btn_start.setEnabled(false);
				btn_result.setEnabled(true);
				btn_rpcard1.setEnabled(true);
				btn_rpcard2.setEnabled(true);
				btn_rpcard3.setEnabled(true);
			}
		});

		btn_result.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				// get the dealer's card
				Card[] dealerCard = game.getDealerCard();

				// get the image according to the corresponding card
				ImageIcon dealer_Image1 = new ImageIcon(dealerCard[0].getImagePath());
				ImageIcon dealer_Image2 = new ImageIcon(dealerCard[1].getImagePath());
				ImageIcon dealer_Image3 = new ImageIcon(dealerCard[2].getImagePath());

				// set the image to the corresponding card_image
				label_Image1.setIcon(dealer_Image1);
				label_Image2.setIcon(dealer_Image2);
				label_Image3.setIcon(dealer_Image3);

				// if player win, display the message and give the money
				if (game.isPlayerWin()) {
					game.updatePlayerMoney(true);
					JOptionPane.showMessageDialog(null, "Congratulations! You win this round!");
				} else {
					game.updatePlayerMoney(false);
					JOptionPane.showMessageDialog(null, "Sorry! The Dealer wins this round!");
				}

				label_info.setText("Amount of money you have: $" + game.getMoney());
				
				// reset a new game
				btn_start.setEnabled(true);
				btn_result.setEnabled(false);
				btn_rpcard1.setEnabled(false);
				btn_rpcard2.setEnabled(false);
				btn_rpcard3.setEnabled(false);

				ImageIcon Image1 = new ImageIcon("Images/card_back.gif");

				label_Image1.setIcon(Image1);
				label_Image2.setIcon(Image1);
				label_Image3.setIcon(Image1);
				label_Image4.setIcon(Image1);
				label_Image5.setIcon(Image1);
				label_Image6.setIcon(Image1);

				// if player don't have money, end the game and show the error message
				if (game.isGameOver()) {
					btn_start.setEnabled(false);
					label_money.setText("You have no more money!");
					label_info.setText("Please start a new game!");
					JOptionPane.showMessageDialog(null,
							"Game Over!\nYou have no more money!\nPlease start a new game!");
				}
			}
		});

		btn_rpcard1.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				// replace the card
				game.replaceCard(0);

				// display the new card
				Card[] playerCard = game.getPlayerCard();
				ImageIcon player_Image1 = new ImageIcon(playerCard[0].getImagePath());
				label_Image4.setIcon(player_Image1);

				// disable the button
				btn_rpcard1.setEnabled(false);
				if (!game.canReplaceCard()) {
					// disable the button
					btn_rpcard2.setEnabled(false);
					btn_rpcard3.setEnabled(false);
				}
			}
		});

		btn_rpcard2.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				// replace the card
				game.replaceCard(1);

				// display the new card
				Card[] playerCard = game.getPlayerCard();
				ImageIcon player_Image2 = new ImageIcon(playerCard[1].getImagePath());
				label_Image5.setIcon(player_Image2);

				// disable the button
				btn_rpcard2.setEnabled(false);
				if (!game.canReplaceCard()) {
					// disable the button
					btn_rpcard1.setEnabled(false);
					btn_rpcard3.setEnabled(false);
				}
			}
		});

		btn_rpcard3.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				// replace the card
				game.replaceCard(2);

				// display the new card
				Card[] playerCard = game.getPlayerCard();
				ImageIcon player_Image3 = new ImageIcon(playerCard[2].getImagePath());
				label_Image6.setIcon(player_Image3);

				// disable the button
				btn_rpcard3.setEnabled(false);
				if (!game.canReplaceCard()) {
					// disable the button
					btn_rpcard1.setEnabled(false);
					btn_rpcard2.setEnabled(false);
				}
			}
		});

	}
}
