
using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS(60);
int day = 1; //Gets larger when rested and increases enemys healt and damage
int currency=0; // used to buy and upgrade skills and items
int gameState =0; // determines if in hub, battle, skill menu, shop, resting or have lost
bool playerturn=false; //determines if its the players turn
bool enemyselected=false; // determines if the enemys healt and base damage have been determined.
int selectAction1=1;// determines what action is highlighted in battle.
int selectAction2=1;// determines what skill is selected.
int skill=0; // determines if you have opened the skill menu in battle.
int maxPlayerHealth=100; // the players max healt
int playerHealth = maxPlayerHealth; // Players current healt
int magicPoint=100; // Used for skill
int maxMagicPoint=100; // maximum amount of magic points.
int enemylevel=1; // determines enemies base attack and health.
int enemyMaxHealt=0;//maximum health of a enemy
int enemyHealt=0; // current healt of a enemy
int damage; 
int baseAttackdamage=5;
int defence=1;
List<string> skills=["","","","",""];
List<string> skillDescription=["Deals a large amount of damage"];
Color blackHalfTransparent = new(0, 0, 0, 128);
static void optionBoxes(List<Vector2> options,List<string> optionsText, int textSize, Color c){ // draws black boxes with text inside      
    for (int i = 0; i < options.Count; i++)
        {
            Raylib.DrawRectangleV(options[i],new Vector2(textSize*5,textSize*2),c);
            Raylib.DrawRectangleLines((int)options[i].X,(int)options[i].Y,textSize*5,textSize*2,Color.Black);
            Raylib.DrawText(optionsText[i],(int)options[i].X+10, (int)options[i].Y+textSize/2,textSize,Color.White);
        }
}
static int select(int action, int maxAction,int active,Vector2 position, int distance,int size)//apply´s a yellow overlay on the edge of a box and outputs a int
    {
        if (Raylib.IsKeyPressed(KeyboardKey.S)&&active==0)
        {
           action++; 
        }
        if (Raylib.IsKeyPressed(KeyboardKey.W)&&active==0)
        {
           action--; 
        }
        if(action>maxAction){action=1;}
        if(action<1){action=maxAction;}
        Raylib.DrawRectangleLines((int)position.X,distance*action+(int)position.Y,size*5/2,size,Color.Yellow);
        return action;
    }
static void bar(int measurement,int maxMeasurement,string measurementName,int textsize, Vector2 position,Color boxColor, Color barColor)// draws a box with a 
{//Draws a bar with a name and the value to the left. The bar has a set length multiplied by the measurement divided by the maxeasurement
    Raylib.DrawRectangleV(position,new Vector2(measurementName.Length*textsize/4*3,textsize+4),boxColor);
    Raylib.DrawText(measurementName,(int)position.X+4,(int)position.Y+2,textsize,Color.White);
    string measurementLengt= measurement.ToString();
    Raylib.DrawRectangleV(new Vector2((int)position.X+measurementName.Length*textsize/4*3,(int)position.Y),new Vector2(measurementLengt.Length*textsize/4*3,textsize+4),boxColor);
    Raylib.DrawText(measurementLengt,4+(int)position.X+measurementName.Length*textsize/4*3,(int)position.Y+2,textsize,Color.White);
    Raylib.DrawRectangleV(new Vector2((int)position.X+measurementName.Length*textsize/4*3+measurementLengt.Length*textsize/4*3,(int)position.Y),new Vector2(textsize*5,textsize+4),boxColor);
    float a=measurement;
    float b=maxMeasurement;
    float percentage=a/b;
    Raylib.DrawRectangleV(new Vector2((int)position.X+measurementName.Length*textsize/4*3+measurementLengt.Length*textsize/4*3,(int)position.Y+2),new Vector2(percentage*textsize*5,textsize-2),barColor);//The bar itself
    Raylib.DrawRectangleLines((int)position.X,(int)position.Y,measurementName.Length*textsize/4*3+measurementLengt.Length*textsize/4*3+textsize*5,textsize+4,Color.Black);
}
while(!Raylib.WindowShouldClose())
{
    // menu
    if (gameState == 0)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        List<Vector2> option = [new Vector2(340,100),new Vector2(100,270),new Vector2(560,270),new Vector2(340,440),];
        List<string> optionsTex= ["Arena(W)", "skills(A)", "shop(D)", "Rest(S)"];
        optionBoxes(option, optionsTex, 28, blackHalfTransparent);
        if (Raylib.IsKeyPressed(KeyboardKey.W))
        {
            gameState=1;
            selectAction1=1;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.A))
        {
            gameState=2;
            selectAction1=1;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.S))
        {
            gameState=4;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            gameState=3;
        }
        bar(playerHealth,maxPlayerHealth,"HP",16,new Vector2(650,30),blackHalfTransparent,Color.Red);
        bar(magicPoint,maxMagicPoint,"MP",16,new Vector2(650,60),blackHalfTransparent,Color.Blue);
        optionBoxes([new Vector2(710,90)],["Souls: "+currency],16,blackHalfTransparent);
        Raylib.EndDrawing();
    }
    //arena
        //your turn 
    if (gameState == 1)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        bar(playerHealth, maxPlayerHealth, "HP", 40, new Vector2(30,500), blackHalfTransparent, Color.Red);
        bar(magicPoint, maxMagicPoint, "MP", 40, new Vector2(30,550), blackHalfTransparent, Color.SkyBlue);
        if(enemyselected==true){
        bar(enemyHealt, enemyMaxHealt, "HP", 30, new Vector2(550,20), blackHalfTransparent, Color.Red);}
        if (playerturn==true){
        optionBoxes(
            [new Vector2(30,300),new Vector2(30,350),new Vector2(30,400),new Vector2(30,450) ],
            ["Attack","Skill","Items","Defend"],
            20,
            blackHalfTransparent
        );
        selectAction1=select(
        selectAction1,4,skill,
        new Vector2(30,250),50,40
        );
        // Raylib.DrawRectangleLines(30,50*select+250,100,40,Color.Yellow);
        //simple attack
        if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            if(selectAction1==1){skill=1;}
            if(selectAction1==2){skill=2;}
            if(selectAction1==3){skill=3;}
            if(selectAction1==4){skill=4;}
        }
        //skills
        if (skill == 1)
            {
            damage=baseAttackdamage*(1+Random.Shared.Next(3));
            enemyHealt=enemyHealt-damage;
            playerturn=false;
            }
        if (skill == 2)
        {
            optionBoxes(
            [new Vector2(140,250),new Vector2(140,290),new Vector2(140,330),new Vector2(140,370), new Vector2(140,410)],
            skills,
            18,
            blackHalfTransparent
        );
        selectAction2=select(
        selectAction2,5,0,
        new Vector2(140,210),40,36
        );
        if(Raylib.IsKeyPressed(KeyboardKey.A)){skill=0;}
                if (Raylib.IsKeyPressed(KeyboardKey.D))
                {
                    
                }
        }

    }
    if (playerturn==false)
        {
            if (enemyselected == true)
            {
                playerHealth=playerHealth-enemyattack(enemylevel,defence);
            }
            if (enemyselected == false){
                enemylevel=enemyselect(day);
                enemyMaxHealt=50+enemylevel*(2+Random.Shared.Next(5));
                enemyHealt=enemyMaxHealt;
                enemyselected=true;
            }
            skill=0;
            playerturn=true;
        }
        if (enemyHealt <= 0)
        {
            currency= currency+(enemylevel*(50+Random.Shared.Next(50)));
            enemyselected=false;
            playerturn=false;
            gameState=0;
        }
        if (playerHealth <= 0)
        {
            gameState=5;
        }       
        Raylib.EndDrawing();
    }
    if (gameState == 2)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        if(Raylib.IsKeyPressed(KeyboardKey.A)){gameState=0;}
        optionBoxes([new Vector2(170,50),new Vector2(170,100),new Vector2(170,150),new Vector2(170,200),new Vector2(170,250)],["skill 1","skill 2","skill 3","skill 4","skill 5" ],20, blackHalfTransparent);
        selectAction1=select(selectAction1,5,0,new Vector2(170,0),50,40);
        //List<string> skillDescription=["Deals a large amount of damage"];
        Raylib.DrawRectangleV(new Vector2(320,50),new Vector2(300,400),blackHalfTransparent);
        Raylib.DrawRectangleLines(320,50,300,400,Color.Black);
        Raylib.DrawText(skillDescription[1],330,60,28,Color.White);
        Raylib.EndDrawing();
    }
    if (gameState == 4)
    {
        playerHealth=maxPlayerHealth;
        day++;
        gameState=0;
    }
    if (gameState == 5)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        Raylib.DrawText("You lose",250,260,80,Color.Black);
        Raylib.EndDrawing();
    }
        //shop
            //shop menu
                // item descriptions
        //skill aqusition 

        //rest
            // restore Healt and change day

}
static int enemyselect(int day)
{
    return day+Random.Shared.Next(3);
}
static int enemyattack(int level, int defend)
{
    return level*Random.Shared.Next(5)+5/defend;
}
static int enemyattacked(int level, int damage, int debuff)
{
    return damage+debuff-(level/Random.Shared.Next(4));
}