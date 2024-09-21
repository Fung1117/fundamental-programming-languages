import sys, grader, parse, math, random

def availableGhostMove(position, board):
    row, col = position[0], position[1]
    available = []

    if board[row][col + 1] not in ['W', 'X', 'Y', 'Z', '%']:
        available.append('E')
    if board[row][col - 1] not in ['W', 'X', 'Y', 'Z', '%']:
        available.append('W')
    if board[row + 1][col] not in ['W', 'X', 'Y', 'Z', '%']:
        available.append('S')
    if board[row - 1][col] not in ['W', 'X', 'Y', 'Z', '%']:
        available.append('N')

    available.sort()
    return available


def availableMove(position, board):
    row, col = position[0], position[1]
    available = []

    if board[row][col + 1] != '%':
        available.append('E')
    if board[row][col - 1] != '%':
        available.append('W')
    if board[row + 1][col] != '%':
        available.append('S')
    if board[row - 1][col] != '%':
        available.append('N')

    available.sort()
    return available


def makeMove(Game, player, position, move):
    movement = {'E': (0, 1), 'N': (-1, 0), 'S': (1, 0), 'W': (0, -1)}

    row, col = position[0], position[1]
    if player in Game['ghostOnFood']:
        Game['board'][row][col] = '.'
        Game['ghostOnFood'].remove(player)
    else:
        Game['board'][row][col] = ' '

    new_position = [position[i] + movement[move][i] for i in range(2)]
    destionation = Game['board'][new_position[0]][new_position[1]]

    if player == 'P':
        Game['score'] -= 1
        if destionation == '.':
            Game['food'].remove(new_position)
            if len(Game['food']) == 0:
                Game['score'] += 500
            Game['score'] += 10
        elif destionation in ['W', 'X', 'Y', 'Z']:
            Game['isGameOver'] = True
            Game['score'] -= 500
    else:
        if destionation == '.':
            Game['ghostOnFood'].append(player)
        elif destionation == 'P':
            Game['score'] -= 500
            Game['isGameOver'] = True
    if not Game['isGameOver'] or player != 'P':
        Game["board"][new_position[0]][new_position[1]] = player
    Game[player] = new_position



def random_play_multiple_ghosts(problem):
    #Your p3 code here
    solution = ''

    seed = problem['seed']
    random.seed(seed, version=1)

    players = problem['players']
    num_players = problem['#players']

    Game = {
        'board': problem['board'],
        'food': problem['food'],
        'ghostOnFood': [],
        'isGameOver': False,
        'score': 0,
    }

    for player in players:
        Game[player] = problem[player]

    turn = 0
    solution += f"seed: {seed}\n0\n"
    solution += '\n'.join([''.join(row) for row in Game['board']]) + '\n'

    while True:
        player = players[turn % num_players]
        position = Game[player]

        if player == 'P':
            move = random.choice(availableMove(position, Game['board']))
        else:
            availableGhostMoves = availableGhostMove(position, Game['board'])
            if not availableGhostMoves:
                turn += 1
                solution += f"{turn}: {player} moving \n"
                solution += '\n'.join([''.join(row) for row in Game['board']])
                solution += f"\nscore: {Game['score']}\n"
                continue
            move = random.choice(availableGhostMoves)
            

        makeMove(Game, player, position, move)

        turn += 1
        solution += f"{turn}: {player} moving {move}\n"
        solution += '\n'.join([''.join(row) for row in Game['board']])
        solution += f"\nscore: {Game['score']}\n"

        if len(Game['food']) == 0:
            solution += "WIN: Pacman"
            return solution
        elif Game["isGameOver"]:
            solution += "WIN: Ghost"
            return solution

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 3
    grader.grade(problem_id, test_case_id, random_play_multiple_ghosts, parse.read_layout_problem)