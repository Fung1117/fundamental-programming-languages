def PrintBoard(board,name_board):
    x=""
    for i in name_board: x += f"{i:<7}"
    x = x.strip()+"\n"
    for i in range(0,7,3):
        for j in name_board:
            if j != name_board[0]: x += "  "
            x += board[j][i]+" "+board[j][i+1]+" "+board[j][i+2]
        x += "\n"
    print((x.strip()+"\n").strip("\n"))

def check_vaild(name_board,move,board):
    ans=False
    if len(move) == 2 and move[0] in name_board and move[1] in ["0","1","2","3","4","5","6","7","8"]: 
        if board[move[0]][int(move[1])] != "X": ans=True
    if ans: return ans
    else: print("Invalid move, please input again")

def creatlist(board):
    vertical, horizontal = ["","",""], ["","",""]
    for i in range(len(board)):
        vertical[i // 3] += board[i]
        horizontal[i % 3] += board[i]
    win_check_list = vertical + horizontal + [board[0]+board[4]+board[8]] + [board[2]+board[4]+board[6]]
    return win_check_list

def removeX(templist,numberofx):
    temp = templist.copy()
    if numberofx == 1: temp[4]=="4"
    tempremoved = [j for i in creatlist(templist) if i.count("X") == numberofx for j in i if j !="X"]
    temp = [i for i in templist if i not in tempremoved and i != "X"]
    return temp [0]

def surewin(templist,move):
    temp = int([5 if int(move[1])<4 else -5][0])
    if templist.count("X") == 2: ans = str(int(move[1])+temp)
    else:  ans = removeX(templist,2)
    return ans

def canwin(templist):
    win = [True for i in creatlist(templist) if i.count("X") == 2]
    if any (win): ans = [ j for i in creatlist(templist) for j in i if j != "X" and  i.count("X") == 2][0]
    else: ans = removeX(templist,1)
    return ans

def lastmove(templist):
    time = int(["1" if templist.count("X") == 1 else "2"][0])
    return str([(abs(i-8)) for i in [0,1,2,3,6,5,8,7][::time] if templist[i] == "X"][0])
    
def ai(board,name_board,move):
    if len(name_board) == 3:
        if board[move[0]][4] == "X" and move[1] != "4": smartmove = move[0]+str(abs(int(move[1])-8))
        else: smartmove =  move[0]+canwin(board[move[0]])
    elif len(name_board) == 2:
        if  board[name_board[abs(name_board.index(move[0])-1)]][4] == "X":
            if board[move[0]][4] == "X" and move[1] != "4": smartmove = move[0]+str(abs(int(move[1])-8))
            else: smartmove = move[0]+canwin(board[move[0]])
        else: smartmove = move[0]+surewin(board[move[0]],move)
    elif len(name_board) == 1:
        if board[name_board[0]][4] == "4":
            if move[0] == name_board[0] and board[name_board[0]][abs(int(move[1])-8)] != "X":smartmove = move[0]+str(abs(int(move[1])-8))   
            elif board[name_board[0]].count("X") <= 3: smartmove = name_board[0]+lastmove(board[name_board[0]])
            else: smartmove = name_board[0]+surewin(board[name_board[0]],move)
        else: smartmove = name_board[0]+surewin(board[name_board[0]],move)
    return smartmove

def print_board(board, board_names):
    """
    Prints the tic-tac-toe board.
    """
    board_str = ""
    for name in board_names:
        board_str += f"{name:<7}"
    board_str = board_str.strip() + "\n"

    for i in range(0, 7, 3):
        for name in board_names:
            if name != board_names[0]:
                board_str += "  "
            board_str += board[name][i] + " " + board[name][i + 1] + " " + board[name][i + 2]
        board_str += "\n"

    print((board_str.strip() + "\n").strip("\n"))

def is_valid_move(player_names, move, board):
    """
    Checks if a move is valid.
    """
    valid = False

    # Check if the move has two characters and the first character is a valid player name
    if len(move) == 2 and move[0] in player_names:
        position = move[1]

        # Check if the second character is a valid position on the board
        if position in ["0", "1", "2", "3", "4", "5", "6", "7", "8"]:
            row = move[0]
            col = int(position)

            # Check if the position on the board is not already occupied by "X"
            if board[row][col] != "X":
                valid = True

    if valid:
        return True
    print("Invalid move. Please input again.")
    return False
    
def create_win_check_list(board):
    """
    Creates a list of win check combinations from the tic-tac-toe board.
    """
    verticals, horizontals, diagonals = ["", "", ""], ["", "", ""], ["", ""]

    # Generate vertical and horizontal combinations
    for i in range(len(board)):
        verticals[i // 3] += board[i]
        horizontals[i % 3] += board[i]

    # Generate diagonal combinations
    diagonals[0] = board[0] + board[4] + board[8]
    diagonals[1] = board[2] + board[4] + board[6]

    return verticals + horizontals + diagonals

def remove_x(templist, number_of_x):
    """
    Removes 'X' elements from a list and returns the remaining value.
    """
    temp = templist.copy()

    # If number_of_x is 1, replace 'X' at index 4 with '4'
    if number_of_x == 1:
        temp[4] = "4"

    # Generate a list of elements to be removed
    tempremoved = [j for i in create_win_check_combinations(templist) if i.count("X") == number_of_x for j in i if j != "X"]
    
    # Remove elements from templist that are in tempremoved and not equal to 'X'
    temp = [i for i in templist if i not in tempremoved and i != "X"]

    return temp[0]
    
def sure_win(templist, move):
    """
    Determines the next move for a sure win strategy.
    """
    temp = 5 if int(move[1]) < 4 else -5

    # If templist contains 2 'X' elements, calculate the next move based on move[1] and temp
    if templist.count("X") == 2:
        ans = str(int(move[1]) + temp)
    else:
        # If templist does not contain 2 'X' elements, call remove_x function to determine the next move
        ans = remove_x(templist, 2)

    return ans
    
def can_win(templist):
    """
    Determines if a player can win the game based on the given tic-tac-toe board.
    """
    win = [True for i in create_win_check_combinations(templist) if i.count("X") == 2]

    # If there is any win combination, find the first element that is not 'X' and has 2 'X' elements in its combination
    if any(win):
        ans = [j for i in create_win_check_combinations(templist) for j in i if j != "X" and i.count("X") == 2][0]
    else:
        # If there is no win combination, call the remove_x function to determine the next move
        ans = remove_x(templist, 1)

    return ans

def last_move(templist):
    """
    Determines the last move for the game based on the given tic-tac-toe board.
    """
    time = 1 if templist.count("X") == 1 else 2

    # Determine the index of the last 'X' element in the board
    last_x_index = [(abs(i - 8)) for i in [0, 1, 2, 3, 6, 5, 8, 7][::time] if templist[i] == "X"][0]

    return str(last_x_index)

def ai(board, name_board, move):
    """
    Determines the AI's move based on the current state of the game.
    """
    smartmove = ""
    
    if len(name_board) == 3:
        # If there are three boards, check if the center cell of the current board is 'X'
        if board[move[0]][4] == "X" and move[1] != "4":
            smartmove = move[0] + str(abs(int(move[1]) - 8))
        else:
            smartmove = move[0] + can_win(board[move[0]])
    elif len(name_board) == 2:
        # If there are two boards, check if the center cell of the opposite board is 'X'
        if board[name_board[abs(name_board.index(move[0]) - 1)]][4] == "X":
            if board[move[0]][4] == "X" and move[1] != "4":
                smartmove = move[0] + str(abs(int(move[1]) - 8))
            else:
                smartmove = move[0] + can_win(board[move[0]])
        else:
            smartmove = move[0] + sure_win(board[move[0]], move)
    elif len(name_board) == 1:
        # If there is only one board
        if board[name_board[0]][4] == "4":
            if move[0] == name_board[0] and board[name_board[0]][abs(int(move[1]) - 8)] != "X":
                smartmove = move[0] + str(abs(int(move[1]) - 8))
            elif board[name_board[0]].count("X") <= 3:
                smartmove = name_board[0] + last_move(board[name_board[0]])
            else:
                smartmove = name_board[0] + sure_win(board[name_board[0]], move)
        else:
            smartmove = name_board[0] + sure_win(board[name_board[0]], move)
    
    return smartmove

board = {
    "A": ["0", "1", "2", "3", "4", "5", "6", "7", "8"],
    "B": ["0", "1", "2", "3", "4", "5", "6", "7", "8"],
    "C": ["0", "1", "2", "3", "4", "5", "6", "7", "8"]
}
name_board = ["A", "B", "C"]
player = "1"
aiturn = True
aifirst = False
aisecond = False

while len(name_board) != 0:
    print_board(board, name_board)
    if aiturn:
        if not aifirst:
            move, aifirst = "A4", not(aifirst)
        elif not aisecond and move[0] != "A":
            move, aisecond = "B4" if move[0] == "C" else "C4", not(aisecond)
        else:
            move = ai(board, name_board, move)
        print('Player ' + player + ': ' + move)
    else:
        move = input('Player ' + player + ': ')
    if is_valid_move(name_board, move, board):
        board[move[0]][int(move[1])] = "X"
    else:
        continue
    player, aiturn = str("2" if player == "1" else "1"), not(aiturn)
    if any([i == "XXX" for i in create_win_check_list(board[move[0]])]):
        name_board.remove(move[0])

print("Player " + player + " wins game")