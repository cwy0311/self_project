# Very simple tetris implementation
# 
# Control keys:
#       Down - Drop stone faster
# Left/Right - Move stone
#         Up - Rotate Stone clockwise
#     Escape - Quit game
#          P - Pause game
#     Return - Instant drop
#
# Have fun!

# Copyright (c) 2010 "Laria Carolin Chabowski"<me@laria.me>
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.

from random import randrange as rand
import pygame, sys, random, scipy, numpy, math, statistics
from weight_initialization import *
from file_IO import *
from genetic_algorithm import *
import math
import os
# The configuration
cell_size =     18
cols =          10
rows =          22
maxfps =        30


no_of_var =     6           # The number of features
# machine learning variables
population = 64
mutation_rate=0.025 
crossover_rate=0.8
total_training_epoch=100
is_explore = False #true=training

colors = [
(0,   0,   0  ),
(255, 85,  85),
(100, 200, 115),
(120, 108, 245),
(255, 140, 50 ),
(50,  120, 52 ),
(146, 202, 73 ),
(150, 161, 218 ),
(35,  35,  35) # Helper color for background grid
]

# Define the shapes of the single parts
tetris_shapes = [
        [[1, 1, 1],
         [0, 1, 0]],
        
        [[0, 2, 2],
         [2, 2, 0]],
        
        [[3, 3, 0],
         [0, 3, 3]],
        
        [[4, 0, 0],
         [4, 4, 4]],
        
        [[0, 0, 5],
         [5, 5, 5]],
        
        [[6, 6, 6, 6]],
        
        [[7, 7],
         [7, 7]]
]


def rotate_clockwise(shape):
        return [ [ shape[y][x]
                        for y in range(len(shape)) ]
                for x in range(len(shape[0]) - 1, -1, -1) ]

def check_collision(board, shape, offset):
        off_x, off_y = offset
        for cy, row in enumerate(shape):
                for cx, cell in enumerate(row):
                        try:
                                if cell and board[ cy + off_y ][ cx + off_x ]:
                                        return True
                        except IndexError:
                                return True
        return False

def remove_row(board, row):
        del board[row]
        return [[0 for i in range(cols)]] + board
        
def join_matrixes(mat1, mat2, mat2_off):
        off_x, off_y = mat2_off
        for cy, row in enumerate(mat2):
                for cx, val in enumerate(row):
                        mat1[cy+off_y-1 ][cx+off_x] += val
        return mat1

def copy_board(board0):
        board = [ [ 0 for x in range(cols) ]
                        for y in range(rows) ]
        board += [[ 1 for x in range(cols)]]
        for cy, row in enumerate(board0):
            for cx, val in enumerate(row):
                board[cy][cx] += val
        return board


def new_board():
        board = [ [ 0 for x in range(cols) ]
                        for y in range(rows) ]
        board += [[ 1 for x in range(cols)]]
        return board

class TetrisApp(object):
        def __init__(self):
                pygame.init()
                pygame.key.set_repeat(250,25)
                self.width = cell_size*(cols+6)
                self.height = cell_size*rows
                self.rlim = cell_size*cols
                self.bground_grid = [[ 8 if x%2==y%2 else 0 for x in range(cols)] for y in range(rows)]
                
                self.default_font =  pygame.font.Font(
                        pygame.font.get_default_font(), 12)

                self.screen = pygame.display.set_mode((self.width, self.height))
                pygame.event.set_blocked(pygame.MOUSEMOTION)
                self.next_stone = tetris_shapes[rand(len(tetris_shapes))]
                                

                self.action_list = []   # The action list for the movement
                self.feature_list = [0 for item in range (0,no_of_var)]     # The list for the features     
                self.time = 0       # The number of games that AI played
                self.q_weighting = self.init_q()        # Initial randomisation of the weightings


                self.init_game()



        def new_stone(self):
                self.stone = self.next_stone[:]
                self.next_stone = tetris_shapes[rand(len(tetris_shapes))]
                self.stone_x = int(cols / 2 - len(self.stone[0])/2)
                self.stone_y = 0
                
                if check_collision(self.board,
                                   self.stone,
                                   (self.stone_x, self.stone_y)):
                        self.gameover = True
                self.ai_new_stone(self.time,self.feature_list)

        def init_game(self):
                self.feature_list = [0 for item in range (0,no_of_var)]
                self.board = new_board()
                self.new_stone()
                self.level = 1
                self.score = 0
                self.lines = 0
                self.r_quota = 3
                self. m_left = True
                self. m_right = True
                if (is_explore is False): 
                    pygame.time.set_timer(pygame.USEREVENT+1, 1000)
        
        def disp_msg(self, msg, topleft):
                if (is_explore is True):
                    return

                x,y = topleft
                for line in msg.splitlines():
                        self.screen.blit(
                                self.default_font.render(
                                        line,
                                        False,
                                        (255,255,255),
                                        (0,0,0)),
                                (x,y))
                        y+=14
        
        def center_msg(self, msg):
                if (is_explore is True):
                    return

                for i, line in enumerate(msg.splitlines()):
                        msg_image =  self.default_font.render(line, False,
                                (255,255,255), (0,0,0))
                
                        msgim_center_x, msgim_center_y = msg_image.get_size()
                        msgim_center_x //= 2
                        msgim_center_y //= 2
                
                        self.screen.blit(msg_image, (
                          self.width // 2-msgim_center_x,
                          self.height // 2-msgim_center_y+i*22))
        
        def draw_matrix(self, matrix, offset):
                if (is_explore is True):
                    return
        
                off_x, off_y  = offset
                for y, row in enumerate(matrix):
                        for x, val in enumerate(row):
                                if val:
                                        pygame.draw.rect(
                                                self.screen,
                                                colors[val],
                                                pygame.Rect(
                                                        (off_x+x) *
                                                          cell_size,
                                                        (off_y+y) *
                                                          cell_size, 
                                                        cell_size,
                                                        cell_size),0)
        
        def add_cl_lines(self, n):
                linescores = [0, 40, 100, 300, 1200]
                self.lines += n
                self.score += linescores[n] * self.level
                if self.lines >= self.level*6:
                        self.level += 1
                        newdelay = 1000-50*(self.level-1)
                        newdelay = 100 if newdelay < 100 else newdelay
                        if (is_explore is False):
                            pygame.time.set_timer(pygame.USEREVENT+1, newdelay)
        
        def move(self, delta_x):
                if not self.gameover and not self.paused:
                        new_x = self.stone_x + delta_x
                        if new_x < 0:
                                new_x = 0
                        if new_x > cols - len(self.stone[0]):
                                new_x = cols - len(self.stone[0])
                        if not check_collision(self.board,
                                               self.stone,
                                               (new_x, self.stone_y)):
                                self.stone_x = new_x                   

        def quit(self):
                self.center_msg("Exiting...")
                pygame.display.update()
                sys.exit()
        
        def drop(self, manual):
                if not self.gameover and not self.paused:
                        self.score += 1 if manual else 0
                        self.stone_y += 1
                        if check_collision(self.board,
                                           self.stone,
                                           (self.stone_x, self.stone_y)):
                                self.board = join_matrixes(
                                  self.board,
                                  self.stone,
                                  (self.stone_x, self.stone_y))
                                self.new_stone()
                                cleared_rows = 0
                                while True:
                                        for i, row in enumerate(self.board[:-1]):
                                                if 0 not in row:
                                                        self.board = remove_row(
                                                          self.board, i)
                                                        cleared_rows += 1
                                                        break
                                        else:
                                                break
                                self.add_cl_lines(cleared_rows)
                                return True
                return False
        
        def insta_drop(self):
                if not self.gameover and not self.paused:
                        while(not self.drop(True)):
                                pass
        
        def rotate_stone(self):
                if not self.gameover and not self.paused:
                        new_stone = rotate_clockwise(self.stone)
                        if not check_collision(self.board,
                                               new_stone,
                                               (self.stone_x, self.stone_y)):
                                self.stone = new_stone        
        
        def toggle_pause(self):
                self.paused = not self.paused
        
        def start_game(self):
                if self.gameover:
                        self.init_game()
                        self.gameover = False

        # This function is used for imagining the future board after dropping the stone.
        # The returned board will be used for board evaluation.
        def imagine_insta_drop(self,board0,new_stone,offset,delta_x):    
            new_stone_x,new_stone_y = offset        
            inboard = copy_board(board0)
            outboard = copy_board(inboard)
            move_delta_y=1
            while check_collision(inboard,new_stone,(new_stone_x+delta_x, new_stone_y+move_delta_y)) and delta_x!=0:
                    if delta_x>0:
                            delta_x-=1
                    else:
                            delta_x+=1

            while not check_collision(inboard,new_stone,(new_stone_x+delta_x, new_stone_y+move_delta_y)):
                move_delta_y+=1
            outboard = join_matrixes(inboard,new_stone,(new_stone_x+delta_x, new_stone_y+move_delta_y))

            while True:
                for i, row in enumerate(outboard[:-1]):
                    if 0 not in row:
                        outboard = remove_row(outboard, i)
                        break
                else:
                    break

            return outboard
               
        # The function is used for extracting the feature of the board.
        # The extracted feature will be used for board evaluation.
        def board_feature(self,inboard,old_feature):
            # Indexing for the feature list          
            peak_index = 0
            empty_index = 1
            no_of_lines_index = 2            
            surface_roughness_index = 3            
            max_peak_index = 4
            total_uneven_index = 5

            # Variable initialisation
            new_feature = old_feature[:]  
            total_stone = 0 - cols
            empty = 0
            no_of_lines = -1   
            surface_roughness = 0
            peak=0            
            max_peak=0
            total_uneven=0
            
            stone_in_col = [-1 for x in range(cols)]
            peak_of_col = [0 for x in range(cols)]
            empty_in_col = [0 for x in range(cols)]
            
            # Counting for the required features from the board
            for y, row in enumerate(inboard):
                count = 1                
                for x, val in enumerate(row):
                    if val:
                        total_stone+=1
                        stone_in_col[x]+=1
                        if rows-y > peak_of_col[x]:
                            peak_of_col[x]=rows-y                        
                    else:
                        count = 0
                no_of_lines += count
                                        
            for i in range(cols):
                empty_in_col[i] = peak_of_col[i]-stone_in_col[i]
                empty += empty_in_col[i]
                peak+=peak_of_col[i]
                if (peak_of_col[i]>max_peak):
                    max_peak=peak_of_col[i]
                                        
            for i in range(0,cols-1):                
                surface_roughness+=abs(peak_of_col[i]-peak_of_col[i+1])
                if (peak_of_col[i]!=peak_of_col[i+1]):
                    total_uneven+=1
            # Returning the feature                       
            new_feature[peak_index] += peak
            new_feature[empty_index] += empty
            new_feature[no_of_lines_index] += no_of_lines
            new_feature[surface_roughness_index] += surface_roughness                             
            new_feature[max_peak_index] = max_peak                             
            new_feature[total_uneven_index] = total_uneven                             
            return new_feature
        
        # This function is to evaluate the board by the features       
        def board_value(self,inboard,old_feature):
            value = 0
            feature = self.board_feature(inboard,old_feature)
            for i,j in enumerate(feature):
                value += self.q_weighting[i]*feature[i]
            return value
            
        # This function is used for initialising the weightings
        def init_q(self):
            try:
                w=read_features("tetris_weighting")
            except FileNotFoundError:
                w=self.rand_w(no_of_var)
                write_features_1d(w,"tetris_weighting")
            return w
  
        def rand_w(self,num):
            weighting = random_initialization(num,1)
            return weighting

        # This function is used to return a list of movement for 
        # the AI to place the stone at the optimal loaction
        def ai_new_stone(self,time,old_feature):
            
            # Indexing of the movement
            insta_drop_index = 0
            rotate_index = 1
            left_index = 2
            right_index = 3
            drop_index = 4
            do_nothing_index = 5
            
            # Initialisation of variables
            self.action_list.clear()
            self.feature_list = self.board_feature(self.board,old_feature)        
            original_board = copy_board(self.board)[:]
            
            # Generate four different rotations of the current stone
            rotate_new_stone=[]
            rotate_new_stone = [self.stone[:] for i in range(0,4)]
            for i in range(1,4):
                rotate_new_stone[i] = rotate_clockwise(rotate_new_stone[i-1])
            
            # Generate four different rotations of the next stone
            imagine_next_stone=[]
            imagine_next_stone = [self.next_stone[:] for i in range(0,4)]
            for i in range(1,4):
                imagine_next_stone[i] = rotate_clockwise(imagine_next_stone[i-1])

            # Initialisation of variables
            rotate_q = -float('Inf')
            movement_q = -float('Inf')
            delta_x0 = 0
            delta_xr = 1
            delta_xl = -1
            movement = 0
            rotate = 0
            option=[]
            max_option=3
            
            # For each rotation, considering the locations that the
            # stone can be placed and evaluating the board value
            for num_of_rotate_time in range(0,4):
                delta_x0 = 0
                delta_xr = 1
                delta_xl = -1
                imagine_board_rotate = self.imagine_insta_drop(original_board,rotate_new_stone[num_of_rotate_time],     
                                        (self.stone_x,self.stone_y),delta_x0)
                new_q = self.board_value(imagine_board_rotate,old_feature)
                if new_q > movement_q:
                        if len(option)<max_option:
                                option.append([new_q,delta_x0,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum
                        else:
                                minimum_index=0
                                for index in range (1,len(option)):
                                        if option[index][0]<option[minimum_index][0]:
                                                minimum_index=index
                                option.pop(minimum_index)
                                option.append([new_q,delta_x0,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum                                
                                

                while self.stone_x+len(rotate_new_stone[num_of_rotate_time])+delta_xr-3<= cols:
                    imagine_board_rotate = self.imagine_insta_drop(original_board,rotate_new_stone[num_of_rotate_time],     
                                        (self.stone_x,self.stone_y),delta_xr)
                    new_q = self.board_value(imagine_board_rotate,old_feature)
                    if new_q> movement_q:
                            
                        if len(option)<max_option:
                                option.append([new_q,delta_xr,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum
                        else:
                                minimum_index=0
                                for index in range (1,len(option)):
                                        if option[index][0]<option[minimum_index][0]:
                                                minimum_index=index
                                option.pop(minimum_index)
                                option.append([new_q,delta_xr,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum                                

                    delta_xr += 1
                while self.stone_x+delta_xl >= 0 :
                    imagine_board_rotate = self.imagine_insta_drop(original_board,rotate_new_stone[num_of_rotate_time],     
                                        (self.stone_x,self.stone_y),delta_xl)
                    new_q = self.board_value(imagine_board_rotate,old_feature)
                    if new_q > movement_q:
                        if len(option)<max_option:
                                option.append([new_q,delta_xl,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum
                        else:
                                minimum_index=0
                                for index in range (1,len(option)):
                                        if option[index][0]<option[minimum_index][0]:
                                                minimum_index=index
                                option.pop(minimum_index)
                                option.append([new_q,delta_xl,num_of_rotate_time])
                                minimum=option[0][0]
                                for index in range (1,len(option)):
                                        current=option[index][0]
                                        if current<minimum:
                                                minimum=current
                                movement_q=minimum                                

                    delta_xl += -1
                    
            # Evaluating the future board inlcuding the next stone
            for index in range(len(option)):
                imagine_board_rotate=self.imagine_insta_drop(original_board,rotate_new_stone[option[index][2]],(self.stone_x,self.stone_y),option[index][1])
                for num_of_rotate_time2 in range (4):
                        x0=0
                        xr=1
                        xl=-1
                        imagine_second_time_board=self.imagine_insta_drop(imagine_board_rotate,imagine_next_stone[num_of_rotate_time2],(self.stone_x,self.stone_y),x0)
                        new_q=self.board_value(imagine_second_time_board,old_feature)
                        if new_q>option[index][0]:
                                option[index][0]=new_q
                        while self.stone_x+len(imagine_next_stone[num_of_rotate_time2])+xr-3<=cols:
                                imagine_second_time_board=self.imagine_insta_drop(imagine_board_rotate,imagine_next_stone[num_of_rotate_time2],(self.stone_x,self.stone_y),xr)
                                new_q=self.board_value(imagine_second_time_board,old_feature)
                                if new_q>option[index][0]:
                                        option[index][0]=new_q
                                xr+=1
                        while self.stone_x+xl >= 0 :
                                imagine_second_time_board=self.imagine_insta_drop(imagine_board_rotate,imagine_next_stone[num_of_rotate_time2],(self.stone_x,self.stone_y),xl)
                                new_q=self.board_value(imagine_second_time_board,old_feature)
                                if new_q>option[index][0]:
                                                option[index][0]=new_q
                                xl-=1                    

            
            # Determinin the number of rotation and movement required
            maximum_index=0
            for index in range(1,len(option)):
                    if option[index][0]>option[maximum_index][0]:
                            maximum_index=index
            movement=option[maximum_index][1]
            rotate=option[maximum_index][2]
            
            
            # Pushing the movement required for the optimal action
            for i in range (0,rotate):
                self.action_list.append(rotate_index)

            for i in range (0,abs(movement)):
                if movement > 0:
                    self.action_list.append(right_index)
                if movement < 0:
                    self.action_list.append(left_index)
                
            self.action_list.append(insta_drop_index)
        
        def run(self):
                key_actions = {
                        'ESCAPE':       self.quit,
                        'LEFT':         lambda:self.move(-1),
                        'RIGHT':        lambda:self.move(+1),
                        'DOWN':         lambda:self.drop(True),
                        'UP':           self.rotate_stone,
                        'p':            self.toggle_pause,
                        'SPACE':        self.start_game,
                        'RETURN':       self.insta_drop
                }
                
                # The action list for the AI to press the key
                AI_action = [   self.insta_drop,
                                self.rotate_stone,
                                lambda:self.move(-1),
                                lambda:self.move(+1),
                                lambda:self.drop(True)                                
                            ]

                # Variable initialisation         
                self.time = 1                
                max_score = 0
                current_population = 0  
                population_size = random_initialization(population,no_of_var)            
                #print(population_size)                   
                scores = []
                levels=[]
                lines=[]
                if (is_explore is True):
                    self.q_weighting=population_size[current_population]
                    print("start training tetris ai")
                    
                self.gameover = False
                self.paused = False
                if (is_explore is False):
                    dont_burn_my_cpu = pygame.time.Clock()
                while 1:
                
                        if (is_explore is False):
                            self.screen.fill((0,0,0))
                        if self.gameover:
                            if (is_explore is False): 
                                self.center_msg("""Game Over!\nYour score: %d Press space to continue""" % self.score)
                            
##############################################################################
##      ||      The code below is implemented for the AI
##      ||      This part of code is also the key of the AI. This would be called after each game,
##      ||      when the game is in the "gameover" state. The AI would calculated the optimised 
##      \/      weightings and the exploration weightings for the next game.


                            # Quit the game automatically if exceeds the maximum playing time
                            #if self.time > max_playing_time:
                            #    self.quit()                                              
                            
                            scores.append(self.score)
                            lines.append(self.lines)
                            levels.append(self.level)
                            
                            #save best performance
                            if (self.score>max_score):
                                max_score=self.score
                                write_features_1d(population_size[current_population].reshape(no_of_var,1),"tetris_weighting")
                            
                            #write score in txt
                            write_score(self.score,"tetris_scores")
                            write_score(self.lines,"tetris_lines")
                            write_score(self.level,"tetris_level")
                            print("game "+str(current_population+1)+" scores: "+str(self.score))

                            
                            
                            if (is_explore is True):
                                if (current_population+1==population):
                                    #write avg scores in txt
                                    write_average_scores(scores,"tetris_avg_scores")
                                    write_average_scores(lines,"tetris_avg_lines")
                                    write_average_scores(levels,"tetris_avg_levels")
                                    print("epoch "+str(self.time)+":")
                                    print("avg scores: "+str(scores))
                                    print("avg lines: "+str(lines))
                                    print("avg level: "+str(levels))
                                    print("best scores: "+str(max_score))

                                    #next generation setting
                                    if (sum(scores)<population*1000):
                                        population_size=next_generation(population_size,scores,mutation_rate=mutation_rate,crossover_rate=crossover_rate)
                                    else:
                                        population_size=next_generation(population_size,lines,mutation_rate=mutation_rate,crossover_rate=crossover_rate)
                                    #print(population_size)
                                    #reset parameter
                                    self.time+=1
                                    if (self.time>total_training_epoch):
                                        self.quit()
                                    current_population=0
                                    scores=[]
                                    lines=[]
                                    levels=[]
                                else:
                                    current_population+=1
                                    self.q_weighting=population_size[current_population]
                                    #print(self.q_weighting)
                            
                            # Auto-start the game for learning
                            write_features_1d(population_size[current_population].reshape(no_of_var,1),"tetris_current_weighting")
                            self.start_game()
                            
                            
                            

                            
##      /\
##      ||      The code above is implemented for the AI
##############################################################################
                        else:
                                if self.paused:
                                        self.center_msg("Paused")
                                else:
                                        if (is_explore is False): 
                                            pygame.draw.line(self.screen,
                                                    (255,255,255),
                                                    (self.rlim+1, 0),
                                                    (self.rlim+1, self.height-1))
                                            self.disp_msg("Next:", (
                                                    self.rlim+cell_size,
                                                    2))
                                            self.disp_msg("Score: %d\n\nLevel: %d\
    \nLines: %d\n\nGeneration: %d\n\nC.Gen.: %d\n\nM.Rate: %f\n\nC.Rate.: %f" % (self.score, self.level,self.lines,self.time,current_population,mutation_rate,crossover_rate),
                                                    (self.rlim+cell_size, cell_size*5))
                                            self.draw_matrix(self.bground_grid, (0,0))
                                            self.draw_matrix(self.board, (0,0))
                                            self.draw_matrix(self.stone,
                                                    (self.stone_x, self.stone_y))
                                            self.draw_matrix(self.next_stone,
                                                    (cols+1,2))
                                        AI_action[self.action_list.pop(0)]()
                                        
                                
                        if (self.score>2000000000 and is_explore is True):
                            print("train tetris ai successed!")
                            write_features_1d(population_size[current_population].reshape(no_of_var,1),"tetris_weighting")
                            self.quit()
                        

                        if (is_explore is False):                      
                            pygame.display.update()                        
                            pygame.display.update()                        
                        for event in pygame.event.get():
                                if event.type == pygame.USEREVENT+1:
                                        self.drop(False)
                                elif event.type == pygame.QUIT:
                                        self.quit()
                                elif event.type == pygame.KEYDOWN:                                                             
                                        for key in key_actions:
                                                if event.key == eval("pygame.K_"
                                                +key):
                                                    key_actions[key]()
                                        
                        if (is_explore is False):
                            dont_burn_my_cpu.tick(maxfps)



if __name__ == '__main__':
        App = TetrisApp()
        App.run()
