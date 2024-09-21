import sys, parse, grader
from heapq import heappush, heappop

def astar_search(problem):
    #Your p5 code here
    startState = problem['start_state']
    goalState = problem['goal_states']
    stateSpaceGraph = problem['stateSpaceGraph']
    h = problem['h']
    frontier = []
    heappush(frontier, (h[startState], [startState]))
    exploredList = list()
    while frontier:
        node = heappop(frontier)
        if (node[1][-1] == goalState):
            return (' '.join(exploredList) + '\n' + ' '.join(node[1]))
        if node[1][-1] not in exploredList:
            exploredList.append(node[1][-1])
            if node[1][-1] in stateSpaceGraph:
                for child in stateSpaceGraph[node[1][-1]]:
                    heappush(frontier, (node[0]+child[0]-h[node[1][-1]]+h[child[1]], node[1]+[child[1]]))
    return "No path found."

if __name__ == "__main__":
    test_case_id = int(sys.argv[1])
    problem_id = 5
    grader.grade(problem_id, test_case_id, astar_search, parse.read_graph_search_problem)