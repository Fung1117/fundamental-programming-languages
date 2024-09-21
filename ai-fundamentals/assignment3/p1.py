import sys
import grader
import parse
import random


def printGrid(grid, position, action, reward):
    state = ""
    for i, row in enumerate(grid):
        for j, cell in enumerate(row):
            if i == position[0] and j == position[1] and action != 'exit':
                state += 'P'.rjust(5)
            else:
                state += str(cell).rjust(5)
        state += '\n'
    state += f"Cumulative reward sum: {reward}"
    return state


def printAction(action, intended, step_reward):
    return f"Taking action: {action} (intended: {intended})\nReward received: {step_reward}\nNew state:\n"


def update_position(position, action):
    new_position = position.copy()
    if action == 'N':
        new_position[0] -= 1
    elif action == 'S':
        new_position[0] += 1
    elif action == 'E':
        new_position[1] += 1
    elif action == 'W':
        new_position[1] -= 1
    return new_position


def play_episode(problem):
    experience = 'Start state:\n'

    seed = problem['seed']
    random.seed(seed, version=1)
    grid = problem['grid']
    policy = problem['policy']
    noise = problem['noise']
    livingReward = problem['livingReward']
    position = problem['S']
    reward = 0.0
    action = ""

    d = {'N': ['N', 'E', 'W'], 'E': ['E', 'S', 'N'],
         'S': ['S', 'W', 'E'], 'W': ['W', 'N', 'S']}

    experience += printGrid(grid, position, "", reward)

    while action != 'exit':
        experience += '\n-------------------------------------------- \n'
        intended_action = policy[position[0]][position[1]]

        if intended_action != 'exit':
            action = random.choices(d[intended_action], weights=[
                                    1 - noise * 2, noise, noise])[0]
        else:
            action = intended_action

        new_position = update_position(position, action)

        step_reward = livingReward if grid[position[0]][position[1]] in [
            '#', '_', 'S', 'P'] else float(grid[position[0]][position[1]])
        reward += step_reward
        reward = float(round(reward, 4))

        experience += printAction(action, intended_action, step_reward)

        if new_position[0] < 0 or new_position[0] >= len(grid) or new_position[1] < 0 or new_position[1] >= len(grid[0]):
            position = position
        elif grid[new_position[0]][new_position[1]] == '#':
            position = position
        else:
            position = new_position

        experience += printGrid(grid, position, action, reward)

    return experience


if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 1
    grader.grade(problem_id, test_case_id, play_episode,
                 parse.read_grid_mdp_problem_p1)
