import sys, grader, parse
from copy import deepcopy

def printValue(values):
    return '\n'.join([''.join(['| ##### |' if col == '#' else f'|{col:7.2f}|' for col in row]) for row in values]) + '\n'

def printPolicy(policy):
    return '\n'.join([''.join(['|{:^3s}|'.format(col) for col in row]) for row in policy]) + '\n'

def initValue(grid):
    return [[x if x == '#' else 0 for x in row] for row in grid]

def initPolicy(grid):
    return [['#' if cell == '#' else 'N' if cell in ['_', 'S'] else 'x' for cell in row] for row in grid]

def validUpdate(new_values, i, j, direction):
    directions = {
        'N': [(-1, 0), (0, 1), (0, -1)],
        'S': [(1, 0), (0, 1), (0, -1)],
        'E': [(0, 1), (1, 0), (-1, 0)],
        'W': [(0, -1), (1, 0), (-1, 0)]
    }
    return [(i + x, j + y) if 0 <= i + x < len(new_values) and 0 <= j + y < len(new_values[0]) and new_values[i + x][j + y] != '#' else (i, j) for x, y in directions[direction]]


def value_iteration(problem):
    return_value = "V_k=0\n"
    discount = problem['discount']
    noise = problem['noise']
    livingReward = problem['livingReward']
    iterations = problem['iterations']
    grid = problem['grid']

    values = initValue(grid)
    return_value += printValue(values)
    values = [[livingReward if x in ['_', 'S'] else '#' if x == '#' else float(x) for x in row] for row in grid]
    return_value += "V_k=1\n" + printValue(values)

    policy = initPolicy(grid)
    return_value += 'pi_k=1\n' + printPolicy(policy)

    weight = [1 - noise * 2, noise, noise]

    for _ in range(2, iterations):
        new_values = deepcopy(values)
        for i in range(len(values)):
            for j in range(len(values[0])):
                if policy[i][j] != '#' and policy[i][j] != 'x':
                    max_value = -float('inf')
                    best_policy = policy[i][j]
                    for direction in ['N','E','S','W']:
                        possible_position = validUpdate(new_values, i, j, direction)
                        direction_value = sum(new_values[x][y] * weight[i] for i, (x, y) in enumerate(possible_position)) * discount + livingReward
                        if direction_value - max_value > 0.001:
                            max_value = direction_value
                            best_policy = direction
                    values[i][j] = max_value
                    policy[i][j] = best_policy
        return_value += 'V_k={}\n'.format(_) + printValue(values)
        return_value += 'pi_k={}\n'.format(_) + printPolicy(policy)

    return return_value[:-1]

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 3
    grader.grade(problem_id, test_case_id, value_iteration, parse.read_grid_mdp_problem_p3)