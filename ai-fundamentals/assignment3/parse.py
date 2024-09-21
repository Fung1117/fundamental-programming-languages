def read_grid_mdp_problem_p1(file_path):
    #Your p1 code here
    with open(file_path, "r") as f:
        lines = f.read().splitlines()
    problem = {
        "seed": int(lines[0].split(':')[1]),
        "noise": float(lines[1].split(':')[1]),
        "livingReward": float(lines[2].split(':')[1]),
        "grid": [],
        "policy": []
    }
    for i in range(3, len(lines)):
        if lines[i].startswith('grid'):
            for j in range(i + 1, len(lines)):
                if lines[j].startswith('policy'):
                    break
                else:
                    row = lines[j].split()
                    if "S" in row:
                        problem['S'] = [j - 4, row.index("S")]
                    problem['grid'].append(row)
        elif lines[i].startswith('policy'):
            for j in range(i + 1, len(lines)):
                problem['policy'].append(lines[j].split())
    return problem

def read_grid_mdp_problem_p2(file_path):
    #Your p2 code here
    with open(file_path, 'r') as f:
        lines = f.read().splitlines()
    problem = {
        "discount": float(lines[0].split(':')[1]),
        "noise": float(lines[1].split(':')[1]),
        "livingReward": float(lines[2].split(':')[1]),
        "iterations": int(lines[3].split(':')[1]),
        "grid": [],
        "policy": []
    }
    for i in range(4, len(lines)):
        if lines[i].startswith('grid'):
            for j in range(i + 1, len(lines)):
                if lines[j].startswith('policy'):
                    break
                else:
                    problem['grid'].append(lines[j].split())
        elif lines[i].startswith('policy'):
            for j in range(i + 1, len(lines)):
                problem['policy'].append(lines[j].split())
    return problem

def read_grid_mdp_problem_p3(file_path):
    #Your p3 code here
    with open(file_path, 'r') as f:
        lines = f.read().splitlines()
    problem = {
        "discount": float(lines[0].split(':')[1]),
        "noise": float(lines[1].split(':')[1]),
        "livingReward": float(lines[2].split(':')[1]),
        "iterations": int(lines[3].split(':')[1]),
        "grid": []
    }
    for line in lines[5:]:
        problem['grid'].append(line.split())
    return problem