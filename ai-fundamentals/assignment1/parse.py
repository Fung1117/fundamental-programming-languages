import os, sys
def read_graph_search_problem(file_path):
    #Your p1 code here
    with open(file_path, 'r') as file:
        lines = file.readlines()
    start_state = lines[0].split(":")[1].strip()
    goal_states = lines[1].split(":")[1].strip()
    stateSpaceGraph = {}
    h = {}

    for line in lines[2:]:
        if line.strip():
            data = line.split()
            if len(data) == 3:
                state1 = data[0]
                state2 = data[1]
                cost = float(data[2])
                if state1 not in stateSpaceGraph:
                    stateSpaceGraph[state1] = []
                stateSpaceGraph[state1].append((cost, state2))
            elif len(data) == 2:
                state = data[0]
                h_value = float(data[1])
                h[state] = h_value
    
    problem = {'h': h,
               'stateSpaceGraph': stateSpaceGraph,
               'start_state': start_state,
               'goal_states': goal_states }
    return problem

def read_8queens_search_problem(file_path):
    #Your p6 code here
    with open(file_path, 'r') as file:
        lines = file.readlines()
    lines = [line.replace(' ', '').replace('\n', '') for line in lines]
    problem = []
    for row, line in enumerate(lines):
        for col, char in enumerate(line):
            if char == 'q':
                problem.append((row, col))
    problem = sorted(problem, key=lambda x: x[1])
    return problem

if __name__ == "__main__":
    if len(sys.argv) == 3:
        problem_id, test_case_id = sys.argv[1], sys.argv[2]
        if int(problem_id) <= 5:
            problem = read_graph_search_problem(os.path.join('test_cases','p'+problem_id, test_case_id+'.prob'))
        else:
            problem = read_8queens_search_problem(os.path.join('test_cases','p'+problem_id, test_case_id+'.prob'))
        print(problem)
    else:
        print('Error: I need exactly 2 arguments!')