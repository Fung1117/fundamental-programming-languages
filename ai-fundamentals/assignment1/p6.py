import sys, parse, grader

def count_attack(problem, row, col):
    queens = problem[:col] + [(row, col)] + problem[col + 1:]
    attacks = 0
    for i in range(7):
        for j in range(i+1, 8):
            if queens[i][0] == queens[j][0] or abs(queens[j][0] - queens[i][0]) == abs(queens[j][1] - queens[i][1]):
                attacks += 1
    return attacks

def number_of_attacks(problem):
    #Your p6 code here
    solution = [[""] * 8 for _ in range(8)]
    for row in range(8):
        for col in range(8):
            attacks = count_attack(problem, row, col)
            solution[row][col] = f"{str(attacks):>2}"
    return '\n'.join([' '.join(row) for row in solution])

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 6
    grader.grade(problem_id, test_case_id, number_of_attacks, parse.read_8queens_search_problem)