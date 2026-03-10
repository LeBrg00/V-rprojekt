using System.Net.Mail;
using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS(60);
int day = 0;
int playerLevel= 1;
int currency=0;
int gameState =0;
int selectAction=1;
Color blackHalfTransparent = new(0, 0, 0, 128);
static void optionboxes(List<Vector2> options,List<string> optionsText, int textSize, Color c){       
    for (int i = 0; i < options.Count; i++)
        {
            Raylib.DrawRectangleV(options[i],new Vector2(textSize*5,textSize*2),c);
            Raylib.DrawRectangleLines((int)options[i].X,(int)options[i].Y,textSize*5,textSize*2,Color.Black);
            Raylib.DrawText(optionsText[i],(int)options[i].X+10, (int)options[i].Y+15,textSize,Color.White);
        }
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
        optionboxes(option, optionsTex, 28, blackHalfTransparent);
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
        Raylib.EndDrawing();
    }
    //arena
    
    if (gameState == 1)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        optionboxes(
            [new Vector2(30,300),new Vector2(30,350),new Vector2(30,400),new Vector2(30,450) ],
            ["Attack","Skill","Items","Defend"],
            20,
            blackHalfTransparent
        );
        if (Raylib.IsKeyPressed(KeyboardKey.S))
        {
           selectAction++; 
        }
        if (Raylib.IsKeyPressed(KeyboardKey.W))
        {
           selectAction--; 
        }
        if(selectAction>4){selectAction=1;}
            //your turn 
                //simple attack
                //skills
                    //skill menu
                //defend
            // enemy turn
                //chooses enemytype       
        Raylib.EndDrawing();
    }
        //shop
            //shop menu
                // item descriptions
        //skill aqusition 

        //rest
            // restore Healt and change day

}
