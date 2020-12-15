import numpy as np
import random

#populatin size= (population,num_weights), fitness= (population), num_offspring: num of new population, default=unchange population
def next_generation(population_size,fitness,num_offspring=0,selection='RHS',crossover='uniform',mutation='uniform',mutation_rate=0.01,crossover_rate=0.5):
    if (num_offspring<=0):
        num_offspring=len(population_size)
    #selection
    if (selection=='RHS'):
        selected_parents=RHS(population_size,fitness,num_offspring)
    else:
        selected_parents=RHS(population_size,fitness,num_offspring)
    #crossover
    if (crossover=='uniform'):
        next_gen=uniform_crossover(selected_parents,crossover_rate)
    else:
        next_gen=uniform_crossover(selected_parents,crossover_rate)
    #mutation
    if (crossover=='uniform'):
        next_gen=uniform_mutation(next_gen,mutation_rate)
    else:
        next_gen=uniform_mutation(next_gen,mutation_rate)    
    return next_gen
    
def RHS(population_size,fitness,num_offspring):
    ttl=sum(fitness)
    fitness_prob=fitness.copy()
    for i in range (len(fitness_prob)):
        fitness_prob[i]/=ttl
    new_population_size=[]
    result=np.random.choice(len(fitness),num_offspring,p=fitness_prob)
    for index in result:
        new_population_size.append(population_size[index].copy())
    return new_population_size

def uniform_crossover(parent,crossover_rate):
    current=0
    children=parent.copy()
    while(len(children)>current+1):
        for i in range(len(parent[0])):
            if (random.random()>crossover_rate):
                #crossover(swap)
                temp=children[current][i]
                children[current][i]=children[current+1][i]
                children[current+1][i]=temp
        current+=2
    return children

def uniform_mutation(children,mutation_rate):
    for i in range (len(children)):
        for j in range (len(children[i])):
            if (mutation_rate>=random.random()):
                children[i][j]=np.random.randn()
    return children
