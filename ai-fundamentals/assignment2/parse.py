import os, sys
def read_layout_problem(file_path):
    #Your p1 code here
    problem = {'players': [],
               '#players': 0,
               'food': []}
    with open(file_path, 'r') as file:
        lines = file.readlines()
    seed = int(lines[0].strip().split()[1])
    board = [list(line.strip()) for line in lines[1:]]

    for row in range(len(board)):
        for col in range(len(board[row])):
            if board[row][col] == '.':
                problem["food"].append([row, col])
            elif board[row][col] in ['P', 'W', 'X', 'Y', 'Z']:
                problem[board[row][col]] = [row, col]
                problem['#players'] += 1
                problem['players'].append(board[row][col])
    problem['seed'] = seed
    problem['board'] = board
    problem['players'].sort()
    return problem

if __name__ == "__main__":
    if len(sys.argv) == 3:
        problem_id, test_case_id = sys.argv[1], sys.argv[2]
        problem = read_layout_problem(os.path.join('test_cases','p'+problem_id, test_case_id+'.prob'))
        print(problem)
    else:
        print('Error: I need exactly 2 arguments!')