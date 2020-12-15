import statistics

def write_features_1d(w,filename):
	f = open(filename+".txt", "w")
	for i in range (len(w)-1):
		f.write(str(w[i][0])+"\n")
	f.write(str(w[len(w)-1][0]))
	f.close()
	
def read_features(filename):
	f = open(filename+".txt", "r")
	lines = f.read().split("\n")
	for i in range(len(lines)):
	    lines[i] = float(lines[i])
	f.close()
	return lines
    
def write_average_scores(scores,filename):
    f = open(filename+".txt", "a")
    f.write(str(statistics.mean(scores))+"\n")
    f.close()
    
def write_score(score,filename):
    f = open(filename+".txt", "a")
    f.write(str(score)+"\n")
    f.close()
    
def read_scores(filename):
	f = open(filename+".txt", "r")
	lines = f.read().split("\n")
	lines.pop()
	for i in range(len(lines)):
	    lines[i] = float(lines[i])
	f.close()
	return lines    