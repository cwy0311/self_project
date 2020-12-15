#   SNAKE AI
#   Original Snake Game Author: Apaar Gupta (@apaar97)
#   https://github.com/apaar97/SnakeGame/blob/master/snake_game.py
#   Python 3.5.2 Pygame
#
#
#
#   It is using greedy algorithm in my Snake AI:
#   1. use DFS to check:
#       a.whether there exist a path from Snake Head to Snake Tail
#       b.whether there exist a path from Snake Food to Snake Tail
#       c.whether there exist a path from Snake Head to Food
#
#   2. whether it is GameOver in next predicted move
#   3. the distance between Snake Head and Food



import pygame
import sys
import time
import random
import math
import numpy as np









# Pygame Init
init_status = pygame.init()
if init_status[1] > 0:
    print("(!) Had {0} initialising errors, exiting... ".format(init_status[1]))
    sys.exit()
else:
    print("(+) Pygame initialised successfully ")

# Play Surface
size = width, height = 200, 100
playSurface = pygame.display.set_mode(size)
pygame.display.set_caption("Snake Game")

# Colors
red = pygame.Color(255, 0, 0)
green = pygame.Color(0, 255, 0)
black = pygame.Color(0, 0, 0)
white = pygame.Color(255, 255, 255)
brown = pygame.Color(165, 42, 42)

# FPS controller
fpsController = pygame.time.Clock()

# Game settings
delta = 10
snakePos = [100, 50]
snakeBody = [[100, 50], [90, 50], [80, 50]]
foodPos = [160, 50]
foodSpawn = True
direction = 'RIGHT'
changeto = ''
score = 0



def GenerateFood(snakeBody):
    FoodList=[]
    for x in range(width//delta):
        for y in range(height//delta):
            FoodList.append([x*delta,y*delta])
    for n in snakeBody:
        FoodList.remove(n)
    if (len(FoodList)==0):
        return [-10,-10]
    index=random.randint(0,len(FoodList)-1)
    newFood=FoodList[index]
    return newFood

# Game Over
def gameOver():
    myFont = pygame.font.SysFont('monaco', 24)
    GOsurf = myFont.render("Game Over", True, red)
    GOrect = GOsurf.get_rect()
    GOrect.midtop = (100, 25)
    playSurface.blit(GOsurf, GOrect)
    showScore(0)
    pygame.display.flip()
    time.sleep(4)
    pygame.quit()
    sys.exit()


# Show Score
def showScore(choice=1):
    SFont = pygame.font.SysFont('monaco', 20)
    Ssurf = SFont.render("Score  :  {0}".format(score), True, black)
    Srect = Ssurf.get_rect()
    if choice == 1:
        Srect.midtop = (-100, 10)
    else:
        Srect.midtop = (100, 40)
    playSurface.blit(Ssurf, Srect)


def checkGameOver(snakePos,snakeBody,width,height):
    if snakePos[0] >= width or snakePos[0] < 0:
        return True
    if snakePos[1] >= height or snakePos[1] < 0:
        return True
    for block in snakeBody[1:]:
        if snakePos == block:
            return True
    return False

def ExistPathLoop(original,board,target,loop):
    if (original==target):
        return True
    stack=[[original[0]+1,original[1]],[original[0]-1,original[1]],[original[0],original[1]+1],[original[0],original[1]-1]]
    while (len(stack)>0):
        loop+=1
        current=stack.pop()
        if (current==target):
            return True
        elif (current[0]<0 or current[1]<0 or current[0]>=len(board) or current[1]>=len(board[0]) or  board[current[0]][current[1]]==True):
            continue
        else:
            board[current[0]][current[1]]=True
            stack.append([current[0]+1,current[1]])
            stack.append([current[0]-1,current[1]])
            stack.append([current[0],current[1]+1])
            stack.append([current[0],current[1]-1])
    return False


    
            
    


def ExistPath(Original_Pos,Body,Target_Pos,delta,width,height):
    if (checkGameOver(Original_Pos,Body,width,height)):
        return False
    
    visited = [[False for y in range(height//delta)] for x in range(width//delta)]
    for x in Body:
        if (x[0]<0 or x[1]<0 or x[0]>=width or x[1]>=height):
            return False
        visited[x[0]//delta][x[1]//delta]=True
    original=[Original_Pos[0]//delta,Original_Pos[1]//delta]
    target=[Target_Pos[0]//delta,Target_Pos[1]//delta]
    
    return ExistPathLoop(original,visited,target,0)
    


def NextState(snakePos,snakeBody,foodPos,Dir,ChangeTo,delta,width,height):

    Pos=snakePos.copy()
    Body=snakeBody.copy()
    Food=foodPos.copy()

    loss=0
    if ((ChangeTo=='RIGHT' and Dir=='LEFT') or (ChangeTo == 'LEFT' and Dir == 'RIGHT') or
        (ChangeTo == 'UP' and Dir == 'DOWN') or (ChangeTo == 'DOWN' and Dir == 'UP')):
        return 99999 #reduce calculation because the move is reduntant
    distance=0



    if ChangeTo == 'RIGHT' and Dir != 'LEFT':
        Dir = ChangeTo
    if ChangeTo == 'LEFT' and Dir != 'RIGHT':
        Dir = ChangeTo
    if ChangeTo == 'UP' and Dir != 'DOWN':
        Dir = ChangeTo
    if ChangeTo == 'DOWN' and Dir != 'UP':
        Dir = ChangeTo    
    if Dir == 'RIGHT':
        Pos[0] += delta
    if Dir == 'LEFT':
        Pos[0] -= delta
    if Dir == 'DOWN':
        Pos[1] += delta
    if Dir == 'UP':
        Pos[1] -= delta 
    Body.insert(0, list(Pos))
    if Pos != Food:
        Body.pop()
    IsGameOver=checkGameOver(Pos,Body,width,height)


    ExistPathFromHeadToTail=ExistPath(Pos,Body,Body[len(Body)-1],delta,width,height)
    ExistPathFromFoodToTail=ExistPath(Food,Body,Body[len(Body)-1],delta,width,height)
    ExistPathFromHeadToFood=ExistPath(Pos,Body,Food,delta,width,height)
    if (ExistPathFromHeadToTail==False):
        loss+=7000
    if (ExistPathFromFoodToTail==False):
        loss+=4000
        if (len(snakeBody)>(width//delta*height//delta*3//5)):
            distance-=math.sqrt((Pos[0]-Body[len(Body)-1][0])**2+(Pos[1]-Body[len(Body)-1][1])**2)
        else:
            distance-=math.sqrt((Pos[0]-Food[0])**2+(Pos[1]-Food[1])**2)
    else:
        if (len(snakeBody)>(width//delta*height//delta*3//5)):
            distance-=math.sqrt((Pos[0]-Body[len(Body)-1][0])**2+(Pos[1]-Body[len(Body)-1][1])**2)
        else:
            distance+=math.sqrt((Pos[0]-Food[0])**2+(Pos[1]-Food[1])**2)
    if (ExistPathFromHeadToFood==False):
        loss+=4000   
    if (IsGameOver==True):
        loss+=20000

    loss+=distance
    return loss


while True:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
            sys.exit()
        elif event.type == pygame.KEYDOWN:
            if event.key == pygame.K_RIGHT or event.key == pygame.K_d:
                changeto = 'RIGHT'
            if event.key == pygame.K_LEFT or event.key == pygame.K_a:
                changeto = 'LEFT'
            if event.key == pygame.K_UP or event.key == pygame.K_w:
                changeto = 'UP'
            if event.key == pygame.K_DOWN or event.key == pygame.K_s:
                changeto = 'DOWN'
            if event.key == pygame.K_ESCAPE:
                pygame.event.post(pygame.event.Event(pygame.QUIT))

#####################AI action########################################
    UpScore=NextState(snakePos,snakeBody,foodPos,direction,'UP',delta,width,height)
    DownScore=NextState(snakePos,snakeBody,foodPos,direction,'DOWN',delta,width,height)
    LeftScore=NextState(snakePos,snakeBody,foodPos,direction,'LEFT',delta,width,height)
    RightScore=NextState(snakePos,snakeBody,foodPos,direction,'RIGHT',delta,width,height)
    Score=[UpScore,DownScore,LeftScore,RightScore]
    ArgMin=np.argmin(Score)
    if (ArgMin==0):
        changeto='UP'
    elif (ArgMin==1):
        changeto='DOWN'
    elif (ArgMin==2):
        changeto='LEFT'
    else:
        changeto='RIGHT'
            



#####################AI Action########################################
    
    # Validate direction
    if changeto == 'RIGHT' and direction != 'LEFT':
        direction = changeto
    if changeto == 'LEFT' and direction != 'RIGHT':
        direction = changeto
    if changeto == 'UP' and direction != 'DOWN':
        direction = changeto
    if changeto == 'DOWN' and direction != 'UP':
        direction = changeto

    # Update snake position
    if direction == 'RIGHT':
        snakePos[0] += delta
    if direction == 'LEFT':
        snakePos[0] -= delta
    if direction == 'DOWN':
        snakePos[1] += delta
    if direction == 'UP':
        snakePos[1] -= delta

    # Snake body mechanism
    snakeBody.insert(0, list(snakePos))
    if snakePos == foodPos:
        foodSpawn = False
        score += 1
    else:
        snakeBody.pop()
    if foodSpawn == False:
#        foodPos = [random.randrange(1, width // 10) * delta, random.randrange(1, height // 10) * delta]
        foodPos=GenerateFood(snakeBody)
        if foodPos==[-10,-10]:
            time.sleep(4)
            pygame.quit()
            sys.exit()            
        foodSpawn = True


    playSurface.fill(white)
    for pos in snakeBody:
        pygame.draw.rect(playSurface, green, pygame.Rect(pos[0], pos[1], delta, delta))
    pygame.draw.rect(playSurface, brown, pygame.Rect(foodPos[0], foodPos[1], delta, delta))

    # Bounds
    if snakePos[0] >= width or snakePos[0] < 0:
        gameOver()
    if snakePos[1] >= height or snakePos[1] < 0:
        gameOver()

    # Self hit
    for block in snakeBody[1:]:
        if snakePos == block:
            gameOver()
    showScore()
    pygame.display.flip()
    fpsController.tick(20)
