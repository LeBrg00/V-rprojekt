
using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS(60);
int day = 1;
int currency=0;
int gameState =0;
bool playerturn=false;
bool enemyselected=false;
int selectAction1=1;
int selectAction2=1;
int skill=0;
int maxPlayerHealth=100;
int playerHealth = maxPlayerHealth;
int magicPoint=100;
int maxMagicPoint=100;
int enemylevel;
int enemyMaxHealt=0;
int enemyHealt=0;
int damage;
int baseAttackdamage=5;
List<string> skills=["","","","",""] ;
Color blackHalfTransparent = new(0, 0, 0, 128);
static void optionBoxes(List<Vector2> options,List<string> optionsText, int textSize, Color c){ // draws black boxes with text inside      
    for (int i = 0; i < options.Count; i++)
        {
            Raylib.DrawRectangleV(options[i],new Vector2(textSize*5,textSize*2),c);
            Raylib.DrawRectangleLines((int)options[i].X,(int)options[i].Y,textSize*5,textSize*2,Color.Black);
            Raylib.DrawText(optionsText[i],(int)options[i].X+10, (int)options[i].Y+15,textSize,Color.White);
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
{
    Raylib.DrawRectangleV(position,new Vector2(measurementName.Length*textsize/4*3,textsize+4),boxColor);
    Raylib.DrawText(measurementName,(int)position.X+4,(int)position.Y+2,textsize,Color.White);
    string measurementLengt= measurement.ToString();
    Raylib.DrawRectangleV(new Vector2((int)position.X+measurementName.Length*textsize/4*3,(int)position.Y),new Vector2(measurementLengt.Length*textsize/4*3,textsize+4),boxColor);
    Raylib.DrawText(measurementLengt,4+(int)position.X+measurementName.Length*textsize/4*3,(int)position.Y+2,textsize,Color.White);
    Raylib.DrawRectangleV(new Vector2((int)position.X+measurementName.Length*textsize/4*3+measurementLengt.Length*textsize/4*3,(int)position.Y),new Vector2(textsize*5,textsize+4),boxColor);
    float percentage=measurement/maxMeasurement;
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
        }
        if (Raylib.IsKeyPressed(KeyboardKey.A))
        {
            gameState=2;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.S))
        {
            gameState=3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            gameState=4;
        }
        bar(playerHealth,maxPlayerHealth,"HP",16,new Vector2(650,30),blackHalfTransparent,Color.Red);
        bar(magicPoint,maxMagicPoint,"MP",16,new Vector2(650,60),blackHalfTransparent,Color.Blue);
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
            [new Vector2(140,250),new Vector2(140,290),new Vector2(140,330),new Vector2(140,370), new Vector2(140,410) ],
            skills,
            18,
            blackHalfTransparent
        );
        selectAction2=select(
        selectAction2,5,0,
        new Vector2(140,210),40,36
        );
        if(Raylib.IsKeyPressed(KeyboardKey.A)){skill=0;}
        }
    }
    if (playerturn==false)
        {
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
            enemyselected=false;
            playerturn=false;
            gameState=0;
        }
                    //skill menu
                //defend
            // enemy turn
                //chooses enemytype       
        Raylib.EndDrawing();
    }
    if (gameState == 4)
    {
        playerHealth=maxPlayerHealth;
        day++;
        gameState=0;
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