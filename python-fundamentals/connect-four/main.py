def createBoard():
    # Function to create the game board
    
    # Default size of the board is 6 rows and 7 columns
    r, c = 6, 7
    
    # Check if the user wants a custom-sized board
    if 'n' == input('Standard game? (y/n): '):
        r, c = int(input('r? (2 - 20): ')), int(input('c? (2 - 20): '))
    
    # Create the board using nested lists
    return [['·'] * c for i in range(r)]

def printBoard(board):
    # Function to print the game board
    
    r, c = len(board), len(board[0])
    spaces = 1
    
    # Adjust the spacing if the board is bigger than 9x9
    if r > 9 or c > 9:
        spaces = 2
    
    x = ''
    
    # Iterate over the rows in reverse order to print the board from top to bottom
    for row in range(r-1, -1, -1):
        x += f'{row:>{spaces}}'
        ss = ' '
        
        # Adjust the spacing between columns if the board is bigger than 9x9
        if spaces == 2:
            ss = '  '
        
        # Iterate over each column in the row
        for col in range(c):
            x += ss + board[row][col]
        
        x += ' \n'
    
    x += ' ' + ' ' * spaces
    
    # Print the column numbers at the bottom of the board
    for col in range(c):
        x += f'{col:>{spaces}}' + ' '
    
    print(x)

def createList(board):
    # Function to create a list of all possible winning combinations
    
    r, c = len(board), len(board[0])
    
    # Initialize empty lists to store the rows, columns, and diagonals
    x_list, y_list, diagonals1, diagonals2 = [""] * r, [""] * c, [""] * (r + c - 1), [""] * (r + c - 1)
    
    # Iterate over each cell in the board
    for row in range(r-1, -1, -1):
        for col in range(c):
            # Add the cell value to the corresponding row list
            x_list[row] += board[row][col]
            
    for col in range(c-1, -1, -1):
        for row in range(r):
            # Add the cell value to the corresponding column list
            y_list[col] += board[row][col]
    
    for x in range(c):
        for y in range(r):
            # Add the cell value to the corresponding diagonal lists
            diagonals1[x+y] += board[y][x]
            diagonals2[x-y-(r-1)] += board[y][x]
    
    # Combine all the lists into a single list of winning combinations
    win_list = x_list + y_list + diagonals1 + diagonals2
    return win_list

def makeMove(board, player, col):
    # Function to make a move on the board
    
    r = len(board)
    
    # Iterate over each row in the specified column
    for row in range(r):
        try:
            # Check if the current cell is occupied and the cell below is empty
            if board[row][col] != '·' and board[row+1][col] == '·':
                # Place the player's symbol in the empty cell below
                board[row+1][col] = player
                break
            elif board[row][col] == '·':
                # Place the player's symbol in the current empty cell
                board[row][col] = player
                break
        except IndexError:
            # Ignore the move if the column is full
            return True

def winner(win_list, player):
    # Function to check if a player has won
    
    for i in win_list:
        if i.count(player * 4) > 0:
            print("Player " + player + " has won!")
            return True

def makeDraw(board, count):
    # Function to check if the game is a draw
    
    time = len(board) * len(board[0])
    
    if time == count:
        print("Draw!")
        return True

# Create the initial game board
board = createBoard()

# Initialize the move counter
count = 1

# Print the initial game board
printBoard(board)

# Initialize the current player
player = 'X'

while True:
    # Get the player's move
    move = input('player' + player + ' (col #): ')
    
    if move == 'e':
        # Exit the game if the player enters 'e'
        break
    
    if makeMove(board, player, int(move)):
        # If the move is invalid, continue to the next iteration
        printBoard(board)
        continue
    
    printBoard(board)
    
    # Create a list of winning combinations
    win_list = createList(board)
    
    if winner(win_list, player):
        # Check if the current player has won
        break
    
    if makeDraw(board, count):
        # Check if the game is a draw
        break
    
    count += 1
    
    # Switch the player for the next turn
    if player == 'X':
        player = 'O'
    else:
        player = 'X'

print('bye')