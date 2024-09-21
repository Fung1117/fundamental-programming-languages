import sys
import parse
import random
import time
import os
import copy


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


def distance(A, B):
    return abs(A[0] - B[0]) + abs(A[1] - B[1])


def reflex_move(Game, position, ghosts, availableMoves):
    movement = {'E': (0, 1), 'N': (-1, 0), 'S': (1, 0), 'W': (0, -1)}
    best_score = -100
    best_move = availableMoves[0]

    for move in availableMoves:
        new_position = [position[i] + movement[move][i] for i in range(2)]
        score = 0
        # consider the distance to the closest food
        for food in Game['food']:
            dist = distance(new_position, food)
            if dist == 0:
                score = 10
                break
            score = max(score, 10 / dist)

        # consider the distance to the closest ghost
        ghosts_distance = distance(new_position, Game['W'])
        for ghost in ghosts:
            if ghost == 'P':
                continue
            ghosts_distance = min(ghosts_distance, distance(new_position, Game[ghost]))

        if ghosts_distance == 0:
            score -= 4000
        elif ghosts_distance == 1:
            score -= 2000
        else:
            score -= 10 / ghosts_distance

        # select the best move
        if score > best_score:
            best_score = score
            best_move = move
    return best_move

# able to pass testcase 8 and 9
# time for test 8: 62.92 for running 100, command: python.exe .\p4.py 8 100 0
# time for test 9: 26.78 for running 100, command: python.exe .\p4.py 9 100 0
def better_play_mulitple_ghosts(problem, verbose):
    # Your p4 code here
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
            move = reflex_move(Game, position, players,
                               availableMove(position, Game['board']))
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
        if verbose:
            solution += f"{turn}: {player} moving {move}\n"
            solution += '\n'.join([''.join(row) for row in Game['board']])
            solution += f"\nscore: {Game['score']}\n"

        if len(Game['food']) == 0:
            solution += "WIN: Pacman"
            return solution, "Pacman"
        elif Game["isGameOver"]:
            solution += "WIN: Ghost"
            return solution, "Ghost"


if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 4
    file_name_problem = str(test_case_id)+'.prob'
    file_name_sol = str(test_case_id)+'.sol'
    path = os.path.join('test_cases', 'p'+str(problem_id))
    problem = parse.read_layout_problem(os.path.join(path, file_name_problem))
    num_trials = int(sys.argv[2])
    verbose = bool(int(sys.argv[3]))
    print('test_case_id:', test_case_id)
    print('num_trials:', num_trials)
    print('verbose:', verbose)
    start = time.time()
    win_count = 0
    for i in range(num_trials):
        solution, winner = better_play_mulitple_ghosts(copy.deepcopy(problem), verbose)
        if winner == 'Pacman':
            win_count += 1
        if verbose:
            print(solution)
    win_p = win_count/num_trials * 100
    end = time.time()
    print('time: ', end - start)
    print('win %', win_p)
