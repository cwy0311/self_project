import matplotlib
import matplotlib.pyplot as plt
from file_IO import *

def plot_score(filename):
    scores=read_scores(filename)
    plt.plot(scores)
    plt.show()


plot_score("tetris_lines")

