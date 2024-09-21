import sys, grader, parse, collections
def dfs_search(problem):
    #Your p1 code here
    startState = problem['start_state']
    goalState = problem['goal_states']
    stateSpaceGraph = problem['stateSpaceGraph']
    frontier = collections.deque([[startState]])
    exploredList = list()
    while frontier:
        node = frontier.pop()
        if (node[-1] == goalState):
            return(' '.join(exploredList) + '\n' + ' '.join(node))
        if node[-1] not in exploredList:
            exploredList.append(node[-1])
            if node[-1] in stateSpaceGraph:
                for child in stateSpaceGraph[node[-1]]:
                    frontier.append(node+[child[1]])
    return "No path found."

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 1
    grader.grade(problem_id, test_case_id, dfs_search, parse.read_graph_search_problem)