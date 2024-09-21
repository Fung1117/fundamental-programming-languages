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

def minimax(Game, depth, maximizing_player):
    if depth == 0 or Game['isGameOver'] or len(Game['food']) == 0:
        return evaluate(Game)

    if maximizing_player:
        max_value = float('-inf')
        player = 'P'
        position = Game['P']
        availableMoves = availableMove(position, Game['board'])
        for move in availableMoves:
            new_Game = copy.deepcopy(Game)
            makeMove(new_Game, player, position, move)
            value = minimax(new_Game, depth - 1, False)
            max_value = max(max_value, value)
        return max_value
    else:
        min_value = float('inf')
        ghost_players = [player for player in Game['players'] if player != 'P']
        for ghost in ghost_players:
            position = Game[ghost]
            availableGhostMoves = availableGhostMove(position, Game['board'])
            for move in availableGhostMoves:
                new_Game = copy.deepcopy(Game)
                makeMove(new_Game, ghost, position, move)
                value = minimax(new_Game, depth - 1, True)
                min_value = min(min_value, value)
        return min_value
    
def evaluate(Game):
    player = Game['players'][Game['turn'] % Game['num_players']]
    if player == 'P':
        pacman_position = Game['P']
        score = Game['score']
        food_positions = Game['food']
        ghost_positions = [Game[player] for player in Game['players'] if player != 'P']
        
        # Calculate the distance to the closest food
        closest_food_distance = float('inf')
        second_closest_food_distance = float('inf')
        for food_pos in food_positions:
            dist = distance(pacman_position, food_pos)
            if dist < closest_food_distance:
                second_closest_food_distance = closest_food_distance
                closest_food_distance = dist
            elif dist < second_closest_food_distance:
                second_closest_food_distance = dist
        if closest_food_distance <= 0:
            closest_food_distance = 0.1
        if second_closest_food_distance <= 0:
            second_closest_food_distance = 0.1
        if len(Game['food']) == 0:
            score += 10000
        
        # Example: Assign higher value to states where Pacman has higher score
        # and is closer to food but farther from ghosts
        pacman_value = score + 1 / closest_food_distance + 0.5 / second_closest_food_distance
        return pacman_value
    else:
        ghost_position = Game[player]
        pacman_position = Game['P']
        ghost_on_food = Game['ghostOnFood']
        dist = distance(ghost_position, pacman_position)
        if (dist <= 0):
            return -10000
        # Example: Assign higher value to states where ghost is closer to Pacman
        ghost_value = - 1 / dist

        if ghost_position in ghost_on_food:
            ghost_value -= 1 / 100

        return ghost_value


def min_max_mulitple_ghosts(problem, k, verbose):
    # Your p5 code here
    solution = ''

    seed = problem['seed']
    random.seed(seed, version=1)


    Game = {
        'board': problem['board'],
        'food': problem['food'],
        'ghostOnFood': [],
        'isGameOver': False,
        'players': problem['players'],
        'num_players': problem['#players'],
        'score': 0,
        'turn' : 0,
    }

    for player in Game['players']:
        Game[player] = problem[player]

    solution += f"seed: {seed}\n0\n"
    solution += '\n'.join([''.join(row) for row in Game['board']]) + '\n'

    while True:
        player = Game['players'][Game['turn'] % Game['num_players']]
        position = Game[player]

        if player == 'P':
            availableMoves = availableMove(position, Game['board'])
            max_value = float('-inf')
            best_move = availableMoves[0]
            for move in availableMoves:
                new_Game = copy.deepcopy(Game)
                makeMove(new_Game, player, position, move)
                value = minimax(new_Game, k, False)
                if value > max_value:
                    max_value = value
                    best_move = move
        else:
            availableGhostMoves = availableGhostMove(position, Game['board'])
            if not availableGhostMoves:
                Game['turn'] += 1
                solution += f"{Game['turn']}: {player} moving \n"
                solution += '\n'.join([''.join(row) for row in Game['board']])
                solution += f"\nscore: {Game['score']}\n"
                continue
            min_value = float('inf')
            best_move = availableGhostMoves[0]
            for move in availableGhostMoves:
                new_Game = copy.deepcopy(Game)
                makeMove(new_Game, player, position, move)
                value = minimax(new_Game, k, True)
                if value < min_value:
                    min_value = value
                    best_move = move

        
        makeMove(Game, player, position, best_move)

        Game['turn'] += 1
        if verbose:
            solution += f"{Game['turn']}: {player} moving {move}\n"
            solution += '\n'.join([''.join(row) for row in Game['board']])
            solution += f"\nscore: {Game['score']}\n"
            # if player == 'P':
            #     print(f"{Game['turn']}: {player} moving {move}\n")
            #     print('\n'.join([''.join(row) for row in Game['board']]))
            #     print(f"\nscore: {Game['score']}\n")
            #     input()


        if len(Game['food']) == 0:
            solution += "WIN: Pacman"
            return solution, "Pacman"
        elif Game["isGameOver"]:
            solution += "WIN: Ghost"
            return solution, "Ghost"


if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 5
    file_name_problem = str(test_case_id)+'.prob'
    file_name_sol = str(test_case_id)+'.sol'
    path = os.path.join('test_cases', 'p'+str(problem_id))
    problem = parse.read_layout_problem(os.path.join(path, file_name_problem))
    k = int(sys.argv[2])
    num_trials = int(sys.argv[3])
    verbose = bool(int(sys.argv[4]))
    print('test_case_id:', test_case_id)
    print('k:', k)
    print('num_trials:', num_trials)
    print('verbose:', verbose)
    start = time.time()
    win_count = 0
    for i in range(num_trials):
        solution, winner = min_max_mulitple_ghosts(
            copy.deepcopy(problem), k, verbose)
        if winner == 'Pacman':
            win_count += 1
        if verbose:
            print(solution)
    win_p = win_count/num_trials * 100
    end = time.time()
    print('time: ', end - start)
    print('win %', win_p)
