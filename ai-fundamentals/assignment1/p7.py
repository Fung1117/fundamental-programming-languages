import sys, parse, grader
from p6 import count_attack

def better_board(problem):
    #Your p7 code here
    solution = [[""] * 8 for _ in range(8)]
    min_pos = [count_attack(problem, 0, 0), (0, 0)]
    for row in range(8):
        for col in range(8):
            if min_pos[0] > count_attack(problem, row, col):
               min_pos =  [count_attack(problem, row, col), (row, col)]
            if (row, col) in problem:
                solution[row][col] = 'q'
            else:
                solution[row][col] = '.'
    for x in problem:
        if (x[0] == min_pos[1][0]):
            solution[x[0]][x[1]] = '.'
            solution[min_pos[1][0]][min_pos[1][1]] = 'q'
            break
    else:
        for x in problem:
            if (x[1] == min_pos[1][1]):
                solution[x[0]][x[1]] = '.'
                solution[min_pos[1][0]][min_pos[1][1]] = 'q'
                break
        
    return '\n'.join([' '.join(row) for row in solution])

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 7
    grader.grade(problem_id, test_case_id, better_board, parse.read_8queens_search_problem)