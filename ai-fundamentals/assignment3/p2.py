import sys
import grader
import parse

from copy import deepcopy


def printValue(values):
    return '\n'.join([''.join(['| ##### |' if col == '#' else f'|{col:7.2f}|' for col in row]) for row in values]) + '\n'


def initValue(grid):
    return [[x if x == '#' else 0 for x in row] for row in grid]

def validUpdate(policy, new_values, i, j):
    directions = {
        'N': [(-1, 0), (0, 1), (0, -1)],
        'S': [(1, 0), (0, 1), (0, -1)],
        'E': [(0, 1), (1, 0), (-1, 0)],
        'W': [(0, -1), (1, 0), (-1, 0)]
    }
    updates = directions[policy[i][j]]
    return [(i + x, j + y) if 0 <= i + x < len(new_values) and 0 <= j + y < len(new_values[0]) and new_values[i + x][j + y] != '#' else (i, j) for x, y in updates]

def policy_evaluation(problem):
    return_value = "V^pi_k=0\n"

    discount = problem['discount']
    noise = problem['noise']
    livingReward = problem['livingReward']
    iterations = problem['iterations']
    grid = problem['grid']
    policy = problem['policy']

    weight = [1 - noise * 2, noise, noise]

    values = initValue(grid)
    return_value += printValue(values)
    values = [[livingReward if x in ['_', 'S'] else '#' if x == '#' else float(x) for x in row] for row in grid]
    return_value += "V^pi_k=1\n" + printValue(values)

    for _ in range(2, iterations):
        new_values = deepcopy(values)
        for i in range(len(values)):
            for j in range(len(values[0])):
                if policy[i][j] in ['N', 'S', 'E', 'W']:
                    valid_updates = validUpdate(policy, new_values, i, j)
                    values[i][j] = sum(new_values[x][y] * weight[i] for i, (x, y) in enumerate(valid_updates)) * discount + livingReward
        return_value += f'V^pi_k={_}\n' + printValue(values)

    return return_value[:-1]

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 2
    grader.grade(problem_id, test_case_id, policy_evaluation,
                 parse.read_grid_mdp_problem_p2)
