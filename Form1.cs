using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMaria
{
   
    public class CWorld
    {
        public Rectangle rcDst, rcSrc;
        public Bitmap img;
    }
    public class CActorHero
    {
        public int x, y;
        public Bitmap img;
    }
    public class CActorDoomsdayEnemy
    {
        public int x, y;
        public Bitmap img;
    }
    public class CActorSonicPunch
    {
        public int x, y;
        public Bitmap img;
    }
    public class CActorEnemyPunch
    {
        public int x, y;
        public Bitmap img;
    }
    public class CLadder
    {
        public int x, y;
        public Bitmap img;
    }
    public class CPlatform
    {
        public int x, y;
        public Bitmap img;
    }
    public partial class Form1 : Form
    {
        List<CWorld> LWorld = new List<CWorld>();
        List<CActorHero> LHero = new List<CActorHero>();
        List<CActorDoomsdayEnemy> LDoomsday = new List<CActorDoomsdayEnemy>();
        List<CActorSonicPunch> LSonicPunch = new List<CActorSonicPunch>();
        List<CActorEnemyPunch> LEnemyPunch = new List<CActorEnemyPunch>(); 
        List<CLadder> LLadder = new List<CLadder>();
        List<CPlatform> LPlatform = new List<CPlatform>();


        Timer tt = new Timer();
        int cttick = 0;
        Bitmap off;
        public int ijump = 0;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            tt.Tick += Tt_Tick;
            tt.Interval = 1;
            tt.Start();
        }
        public int flagintro = 0;
        public int herolocy;
        public int ihoverr = 0;
        public int ihoverl = 0;
        public int flaghoverr = 0;
        public int flaghoverl = 0;
        public int flagdoomsdayishere = 0;
        public int flaghoverleft = 0;
        public int flaghoverright = 0;
        public int flagwalkright = 0;
        public int iwalkright = 0;
        public int flagheroisonright = 0;
        public int flagheroisonleft = 0;
        public int moveenemyleft = 0;
        public int moveenemyright = 0;
        public int ienemywalkleft = 0;
        public int ienemywalkright = 0;
        public int flagplaypunch = 0;
        public int flagplaypunchenemy = 0;
        private void Tt_Tick(object sender, EventArgs e)
        {
            RemoveDeadEnemy();
            CheckIsHit();
            CheckDeathDoomsday();
            if (iintro <= 37)
            {
                HeroIntro();
            }
            if (cttick> 37&&cttick%2==0)
            {
                if(flaghoverright==1)
                {
                    HoverAll();
                    flaghoverr = 1;
                }
                if (flaghoverleft == 1)
                {
                    HoverAll();
                    flaghoverr = 1;
                }
            }
            if (cttick > 37 && cttick % 2 != 0)
            {

                HoverAll();
                flaghoverr = 0;

            }
            if (cttick == 41)
            {
                LHero[0].y = LHero[0].y - 5;
                herolocy = LHero[0].y+5;
                
            }
            if (flagjumpright == 1)
            {
                if(ctjump<=3)
                {
                    ScrollRight();
                    JumpHeroRight();
                    ctjump++;
                }
                if (ctjump == 3)
                {
                    flagjumpright = 0;
                    ctjump = 0;
                    LHero[0].y = herolocy;
                }
            }
            if (flagjumpleft == 1)
            {
                if (ctjump <= 3)
                {
                    JumpHeroLeft();
                    ScrollLeft();
                    ctjump++;
                }
                if (ctjump == 3)
                {
                    flagjumpleft = 0;
                    ctjump = 0;
                    LHero[0].y = herolocy;
                }
            }
            if (flagdie2 == 0)
            {
                if (ctdie <= 8)
                {
                    HeroDieAnimation();
                    flagdie2 = 0;
                    ctdie++;
                }
            }
            if (flagdoomsday2 == 0)
            {
                if (ctdiedoomsday <= 8)
                {
                    DoomsdayDieAnimation();
                    flagdoomsday2 = 0;
                    ctdiedoomsday++;
                }
            }

            if (cttick%2==0)
            {
                if (ctdoomsday < 1)
                {
                    CreateDoomsdayEnemy();
                    flagdoomsdayishere = 1;
                }
                
            }
            if(flagdoomsdayishere==1)
            {
                EnemyFollowHero();
            }
            if(flagsonicpunch==1)
            {
                if (isonicpunch<16|| isonicpunch2<16)
                {
                    HeroSonicPunchAnimationAll();
                    isonicpunch2++;
                    isonicpunch++;
                }

                //if (flagplaypunch==1)
                //{
                //    if(ctpunch==1)
                //    {
                //        MoveSonicPunch();
                //        ctpunch++;
                //    }
                //    ctpunch = 0;

                //}
                CreateSonicPunch();
                
            }
            if (LSonicPunch.Count > 0)
            {
                MoveSonicPunch();
            }
            if(flagcreateladder==1)
            {
                CreateLadder();
            }
            //if(flaggravityon==1)
            //{
            //    Fall();
            //}
            MoveElevator();
           

            cttick++;
            DrawDubb(this.CreateGraphics());
        }
        public int ctpunch = 0;
        public int ctdoomsday = 0;
        public int ctjump = 0;
        public int ctdie = 0;
        public int ctdiedoomsday = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Right:
                    flaghoverleft = 0;
                    flaghoverright = 1;
                    MoveHeroRight();
                    ScrollRight();
                    break;
                case Keys.Left:
                    flaghoverright = 0;
                    flaghoverleft = 1;
                    ScrollLeft();
                    MoveHeroLeft();
                    break;
                case Keys.K:
                    flagjumpright = 1;
                    break;
                case Keys.J:
                    flagjumpleft = 1;
                    break;
                case Keys. P:
                    flagsonicpunch=1;
                    break;
                case Keys.Up:

                    CheckLadderRadius();
                    break;
                case Keys.Down:
                    CheckLadderRadius();
                    break;
                case Keys.L:
                    CreatePlatform();
                    platformlocx += 300;
                    break;
            }
            DrawDubb(this.CreateGraphics());
        }
        void ScrollRight()
        {
            LWorld[0].rcSrc.X += 20;
            if (LWorld[0].rcSrc.X >= LWorld[0].img.Width)
            {
                LWorld[0].rcSrc.X = 0;
            }
        }
        void ScrollLeft()
        {
            LWorld[0].rcSrc.X -= 20;
            if (LWorld[0].rcSrc.X >= LWorld[0].img.Width)
            {
                LWorld[0].rcSrc.X = 0;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        void CheckGravity()
        {
            if(LPlatform.Count>0)
            {
                if (LHero[0].x > LPlatform[0].x + LPlatform[0].img.Width)
                {
                    flaggravityon = 1;
                    Fall();
                }
            }
            
        }
        void Fall()
        {
            if(flaggravityon==1)
            {
                if (LHero[0].y <= herolocy-1)
                LHero[0].y =herolocy -2;
            }
            
        }
        public int ctup = 0;
        public int ctdown = 0;
        void MoveElevator()
        { 
            if(LPlatform.Count>0)
            {
                if (LHero[0].y + LHero[0].img.Height < LPlatform[0].y)
                {
                    flagonelevator = 1;
                    if (ctup <= 50)
                    {
                        LPlatform[0].y -= 5;
                        ctup++;

                    }
                    if (ctup == 50)
                    {
                        ctup = 0;
                    }
                    if (ctdown <= 50)
                    {
                        LPlatform[0].y += 5;
                        ctdown++;

                    }
                    if (ctdown == 50)
                    {
                        ctdown = 0;
                    }
                }

            }


        }
        public int flagonelevator = 0;
        public int flaggravityon=0;
        void CreateWorld()
        {
            CWorld pnn = new CWorld();
            pnn.img = new Bitmap("Background.bmp");
            pnn.rcSrc = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height+450);
            pnn.rcDst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            LWorld.Add(pnn);
        }
        public int hx = 50;
        public int hy = 740;
        void CreateHero()
        {
            CActorHero pnn = new CActorHero();
             pnn.img = new Bitmap("SupermanIntro (2).png");
             pnn.x = hx;
             pnn.y = hy;
             pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
             LHero.Add(pnn);    
        }
        public int doomsdaylocxs, doomsdaylocxe, doomsdaylocys, doomsdaylocye;
        void CreateDoomsdayEnemy()
        {
            doomsdaylocxs = hx+400;
            doomsdaylocxs = hy;
            Random RR = new Random();
            CActorDoomsdayEnemy pnn = new CActorDoomsdayEnemy();
            pnn.img = new Bitmap("DoomsdayWalkLeft (0).png");
            pnn.x = RR.Next(doomsdaylocxs,ClientSize.Width-70) ;
            pnn.y = hy;
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            LDoomsday.Add(pnn);
            ctdoomsday++;
        }
        public int punchlocx;
        public int punchlocy;
        void CreateSonicPunch()
        {
            CActorSonicPunch pnn = new CActorSonicPunch();
            pnn.img = new Bitmap("SonicPunch (0).png");
            pnn.x = punchlocx;
            pnn.y = punchlocy+15;
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            LSonicPunch.Add(pnn);
            flagplaypunch = 1;
            //MoveSonicPunch();
        }
        void CreateLadder()
        {
            CLadder pnn = new CLadder();
            pnn.img = new Bitmap("Ladder.jpeg");
            pnn.x = ClientSize.Width/3;
            pnn.y = ClientSize.Height/2+205;
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            LLadder.Add(pnn);
            
            //MoveSonicPunch();
        }
        public int flagupladder = 0;
        public int flagdownladder = 0;
        void MoveOnLadder()
        {
            if(flagupladder==1)
            {
                // flagdownladder = 0;
                //LHero[0].y -= 10;
                MoveOnLadderUp();
            }
            if (flagdownladder == 1)
            {
                MoveOnLadderDown();
               // flagupladder = 0;
                //LHero[0].y += 10;
            }

        }
        void MoveOnLadderUp()
        {
            
                // flagdownladder = 0;
                LHero[0].y -= 10;
            
        }
        void MoveOnLadderDown()
        {
            
                // flagupladder = 0;
                LHero[0].y += 10;
            
        }

        void CheckLadderRadius()
        {
            if (LHero[0].x >=LLadder[0].x &&
                            LHero[0].x <=LLadder[0].x+LLadder[0].img.Width&& LHero[0].y >= LLadder[0].y-70)
            {
                flagupladder = 1;
                MoveOnLadderUp();
                flagdownladder = 0;

            }
            if (LHero[0].x >= LLadder[0].x &&
                            LHero[0].x < LLadder[0].x + LLadder[0].img.Width&& LHero[0].y <LLadder[0].y-200)
            {
                flagdownladder =1 ;
                MoveOnLadderDown();
                flagupladder = 0;
            }

            ////LHero[0].y < +LLadder[0].y + LLadder[0].img.Height &&
            ////LHero[0].y + LHero[0].img.Height >= LLadder[0].y)
            //{
            //    MoveOnLadder();
            //}
        }
        void CreateSonicEnemy()
        {
            CActorEnemyPunch pnn = new CActorEnemyPunch();
            pnn.img = new Bitmap("SonicPunch (0).png");
            pnn.x = LDoomsday[0].x;
            pnn.y = punchlocy + 25;
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
            LEnemyPunch.Add(pnn);
            flagplaypunchenemy = 1;
            //MoveSonicPunch();
        }
        public int platformlocx, platformlocy, platformwidth;
        void CreatePlatform()
        {
            
            CPlatform pnn = new CPlatform();
            platformlocx= LLadder[0].x + LLadder[0].img.Width;
            platformlocy= LHero[0].y+ LHero[0].img.Height+5;
            
            pnn.img = new Bitmap("Platform.png");
            pnn.x = platformlocx;
            pnn.y = platformlocy;
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));

            
            LPlatform.Add(pnn);
            
        }
        void MoveSonicPunch()
        {
            for (int i = 0; i < LSonicPunch.Count; i++)
            {
                CheckIsHit();
                if(flaghoverright==1)
                {
                    LSonicPunch[i].x += 5;
                }
                if (flaghoverleft == 1)
                {
                    LSonicPunch[i].x -= 5;
                }
                if (LSonicPunch[i].x + LSonicPunch[i].img.Width >= ClientSize.Width - 20 || LSonicPunch[i].x <= ClientSize.Width - ClientSize.Width + 20)
                {
                    //LSonicPunch.RemoveAt(i);
                }

            }
            
        }
        void MoveEnemyPunch()
        {
            for (int i = 0; i < LSonicPunch.Count; i++)
            {
                CheckIsHit();
                if (flaghoverright == 1)
                {
                    LEnemyPunch[i].x += 30;
                }
                if (flaghoverleft == 1)
                {
                    LEnemyPunch[i].x -= 30;
                }


            }

        }
        public int iintro = 0;
        void HeroIntro()
        {
            if(flagintro==0)
            {
                LHero[0].img = new Bitmap("SupermanIntro (" + iintro + ").png");
                LHero[0].x = LHero[0].x + 2;
                LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
                iintro++;
                if(iintro==37)
                {
                    flagintro = 1;
                }
            }
        }
        void HoverAll()
        {
            if (flaghoverleft == 1)
            {
                HeroHoverLeft();
            }
            if (flaghoverright == 1)
            {
                HeroHoverRight();
            }
        }
        void DoomsdayMoveLeft()
        {
            CheckDeathDoomsday();
            if (ienemywalkleft == 5)
            {
                ienemywalkleft = 0;
            }
            if (LDoomsday.Count > 0)
            {
                LDoomsday[0].img = new Bitmap("DoomsdayWalkLeft (" + ienemywalkleft + ").png");
                LDoomsday[0].img.MakeTransparent(LDoomsday[0].img.GetPixel(0, 0));
                LDoomsday[0].x -= 5;
                ienemywalkleft++;
            }
                

            
        }
        void DoomsdayMoveRight()
        {
            CheckDeathDoomsday();
            if (ienemywalkright == 5)
            {
                ienemywalkright = 0;
            }
            if(LDoomsday.Count>0)
            {
                LDoomsday[0].img = new Bitmap("DoomsdayWalkRight (" + ienemywalkright + ").png");
                LDoomsday[0].img.MakeTransparent(LDoomsday[0].img.GetPixel(0, 0));
                LDoomsday[0].x += 5;
                ienemywalkright++;
            }
            
        }
        

        void HeroHoverRight()
        {
            if (flaghoverr == 0)
            {
                LHero[0].img = new Bitmap("SupermanHover (0).png");
            }
            if (flaghoverr == 1)
            {
                 LHero[0].img = new Bitmap("SupermanHover (1).png");
            }
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            ihoverr++;
        }
        void HeroHoverLeft()
        {
            if (flaghoverr == 0)
            {
                LHero[0].img = new Bitmap("SupermanHoverLeft (0).png");
            }
            if (flaghoverr == 1)
            {
                LHero[0].img = new Bitmap("SupermanHoverLeft (1).png");
            }
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            ihoverr++;
        }

        public int flagmoveright = 0;
        public int imoveright = 0;
        void MoveHeroRight()
        {
            flaghoverright = 1;
            flaghoverleft = 0;
            if (flagmoveright == 0)
            {
                LHero[0].img = new Bitmap("SupermanMoveRight (0).png");
                LHero[0].x += 10;
                flagmoveright = 1;
                CheckGravity();
                CheckLadderRadius();
                CheckDeathDoomsday();
                DoomsdayMoveLeft();
            }
            if (flagmoveright == 1)
            {
                LHero[0].img = new Bitmap("SupermanMoveRight (1).png");
                LHero[0].x += 10;
                flagmoveright = 0;
                CheckGravity();
                CheckLadderRadius();
                DoomsdayMoveLeft();
                CheckDeathDoomsday();
            }
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            imoveright++;
        }
            
        
        public int flagmoveleft = 0;
        public int imoveleft = 0;
        void MoveHeroLeft()
        {
            flaghoverleft = 1;
            flaghoverright = 0;
            if (flagmoveleft==0)
            {
                if (flagmoveleft == 0)
                {
                    LHero[0].img = new Bitmap("SupermanMoveLeft (0).png");
                    flagmoveleft = 1;
                    if(LDoomsday.Count>0)
                    {
                        LDoomsday[0].x += 10;
                    }
                    
                    LHero[0].x -= 10;
                    DoomsdayMoveRight();
                    CheckDeathDoomsday();
                }
                if (flagmoveleft == 1)
                {
                    LHero[0].img = new Bitmap("SupermanMoveLeft (1).png");
                    if (LDoomsday.Count > 0)
                    {
                        LDoomsday[0].x += 10;
                    }   
                    LHero[0].x -= 10;
                    flagmoveleft = 0;
                    DoomsdayMoveRight();
                    CheckDeathDoomsday();
                }
            }
           
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            imoveleft++;
        }

        void EnemyFollowHero()
        {
            if(LDoomsday.Count>0)
            {
                if (LHero[0].x + LHero[0].img.Width < LDoomsday[0].x)
                {
                    flagheroisonleft = 1;
                    if (flagheroisonleft == 1)
                    {
                        moveenemyleft = 1;
                        flagheroisonright = 0;
                        if (moveenemyleft == 1)
                        {
                            if (cttick % 2 == 0)
                            {
                                DoomsdayMoveLeft();
                            }

                        }

                    }
                    CheckDeathDoomsday();
                }
                if (LHero[0].x > LDoomsday[0].x + LDoomsday[0].img.Width)
                {
                    flagheroisonright = 1;
                    if (flagheroisonright == 1)
                    {
                        moveenemyright = 1;
                        flagheroisonleft = 0;
                        if (moveenemyright == 1)
                        {
                            DoomsdayMoveRight();
                        }

                    }
                    CheckDeathDoomsday();
                }
            }
           
        }
        void WalkHeroRight()
        {
            LHero[0].img = new Bitmap("SupermanWalkRight (" + iwalkright + ").png");
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            iwalkright++;
            LDoomsday[0].x -= 10;
            if (iwalkright==4)
            {
                iwalkright = 0;
            }
        }
        public int flagjumpright = 0;
        void CheckDeathDoomsday()
        {
            for (int i = 0; i < LDoomsday.Count; i++)
            {
                if (LHero[0].x <= LDoomsday[i].x + LDoomsday[i].img.Width &&
                            LHero[0].x + LHero[0].img.Width >= LDoomsday[i].x &&
                            LHero[0].y < +LDoomsday[i].y + LDoomsday[i].img.Height &&
                            LHero[0].y + LHero[0].img.Height >= LDoomsday[i].y)
                {
                    flagdie = 1;
                    if(flagdie==1)
                    {
                        HeroDieAnimation();
                    }
                }
            }
        }
        public int ienemydie = 0;
        public int  flagdoomsdaydie, flagdoomsday2;
        void CheckIsHit()
        {
            if(LDoomsday.Count>0)
            {
                for (int i = 0; i < LSonicPunch.Count; i++)
                {
                    if (LSonicPunch[i].x <= LDoomsday[0].x + LDoomsday[0].img.Width &&
                                LSonicPunch[i].x + LSonicPunch[i].img.Width >= LDoomsday[0].x &&
                                LSonicPunch[i].y < +LDoomsday[0].y + LDoomsday[0].img.Height &&
                                LSonicPunch[i].y + LSonicPunch[i].img.Height >= LDoomsday[0].y)
                    {
                        flagdoomsdaydie = 1;
                        if (LSonicPunch[i].x + LSonicPunch[i].img.Width >= LDoomsday[0].x - 5 || LSonicPunch[i].x <= LDoomsday[0].x + LDoomsday[0].img.Width+5)
                        {
                            LSonicPunch.RemoveAt(i);
                            if (flagdoomsdaydie == 1)
                            {
                                DoomsdayDieAnimation();
                                
                                
                            }
                        }
                        
                    }
                }
            }
            for (int i = 0; i < LEnemyPunch.Count; i++)
            {
                if (LEnemyPunch[i].x <= LHero[0].x + LHero[0].img.Width &&
                            LEnemyPunch[i].x + LEnemyPunch[i].img.Width >= LHero[0].x &&
                            LEnemyPunch[i].y < +LHero[0].y + LHero[0].img.Height &&
                            LEnemyPunch[i].y + LEnemyPunch[i].img.Height >= LHero[0].y)
                {
                    flagdoomsdaydie = 1;
                    if (flagdoomsdaydie == 1)
                    {
                        CheckDeathDoomsday();
                    }
                }
            }

        }
        void DoomsdayDieAnimation()
        {

            if (flagdoomsdaydie == 1)
            {
                if (ienemydie == 3)
                {
                    ienemydie = 0;
                    flagdeaddoomsdayremove = 1;
                }
                flagdoomsday2 = 0;
                if(LDoomsday.Count>0)
                {
                    LDoomsday[0].img = new Bitmap("DoomsdayDieLeft (" + ienemydie + ").png");
                    LDoomsday[0].img.MakeTransparent(LDoomsday[0].img.GetPixel(0, 0));
                    flagdeaddoomsdayremove = 1;
                    flagcreateladder = 1;
                    ienemydie++;
                    
                }
                
            }
            
        }
        public int flagcreateladder = 0;
        void RemoveDeadEnemy()
        {
            if(flagdeaddoomsdayremove==1)
            {
                LDoomsday.RemoveAt(0);
                flagdeaddoomsdayremove = 0;
            }
            
        }
        public int flagdeaddoomsdayremove = 0;
        public int idie = 0;
        public int flagdie = 0;
        public int flagdie2 = 1;

        void HeroDieAnimation()
        {
            
            if(flagdie==1)
            {
                if (idie == 9)
                {
                    idie = 0;
                }
                flagdie2 = 0;
                LHero[0].img = new Bitmap("SupermanDie (" + idie + ").png");
                
            }
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
            idie++;
        }
        public int flagsonicpunch=0;
        public int isonicpunch = 0;
        public int isonicpunch2 = 0;
        public int sonicpunchon = 0;
        void HeroSonicPunchAnimationRight()
        {
            sonicpunchon = 1;
            punchlocx = LHero[0].x+LHero[0].img.Width;
            punchlocy = LHero[0].y-10;
            if (isonicpunch == 15)
            {
                isonicpunch = 0;
                flagsonicpunch = 0;
            }
            if (flagsonicpunch==1)
            {
                
                LHero[0].img = new Bitmap("SupermanSonicPunchRight (" + isonicpunch + ").png");
                LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
                
            }
            
        }
        void HeroSonicPunchAnimationLeft()
        {
            sonicpunchon = 1;
            punchlocx = LHero[0].x - LHero[0].img.Width;
            punchlocy = LHero[0].y - 10;
            if (isonicpunch2 >= 15)
            {
                isonicpunch2 = 0;
                flagsonicpunch = 0;
            }
            else
            {

                LHero[0].img = new Bitmap("SupermanSonicPunchLeft("+ isonicpunch2 + ").png");
                LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));

            }

        }
        void HeroSonicPunchAnimationAll()
        {
            if(flaghoverright==1)
            {
                HeroSonicPunchAnimationRight();
            }
            if (flaghoverleft == 1)
            {
                HeroSonicPunchAnimationLeft();
            }
        }
        void JumpHeroRight()
        {
            
                LHero[0].img = new Bitmap("SupermanJumpRight (" + ijump + ").png");
                if(ctjump==0)
                {
                     LHero[0].x  += 100;
                     LHero[0].y -= 130;

                     CheckDeathDoomsday();
                }
                if(ctjump==1)
                {
                     LHero[0].x  += 100;
                     LHero[0].y -= 130;
                     CheckDeathDoomsday();
                }
                if(ctjump==2)
                {
                     LHero[0].x  += 100;
                     LHero[0].y += 130;
                     CheckDeathDoomsday();
                }
                if(ctjump==3)
                {
                     LHero[0].x += 100;
                     LHero[0].y += 130;
                     flagjumpright = 0;
                     CheckDeathDoomsday();
                }
                for (int i = 0; i < LDoomsday.Count; i++)
                {
                   LDoomsday[i].x -= 10;
                }
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));
                
            
        }
        public int flagjumpleft = 0;
        void JumpHeroLeft()
        {
            LHero[0].img = new Bitmap("SupermanJumpLeft (" + ijump + ").png");
            if (ctjump == 0)
            {
                LHero[0].x -= 100;
                LHero[0].y -= 130;
                CheckDeathDoomsday();
            }
            if (ctjump == 1)
            {
                LHero[0].x -= 100;
                LHero[0].y -= 130;
                CheckDeathDoomsday();
            }
            if (ctjump == 2)
            {
                LHero[0].x -= 100;
                LHero[0].y += 130;
                CheckDeathDoomsday();
            }
            if (ctjump == 3)
            {
                LHero[0].x -= 100;
                LHero[0].y += 130;
                flagjumpleft = 0;
                CheckDeathDoomsday();
            }
            LDoomsday[0].x += 10;
            LHero[0].img.MakeTransparent(LHero[0].img.GetPixel(0, 0));


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            CreateWorld();
            CreateHero();
            CreateLadder();

        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            for (int i = 0; i < LWorld.Count; i++)
            {
                if (LWorld[i].rcSrc.X + ClientSize.Width > LWorld[i].img.Width)
                {
                    //1
                    int cxRem = LWorld[i].img.Width - LWorld[i].rcSrc.X;
                    Rectangle Dst1 = new Rectangle(0, 0, cxRem, ClientSize.Height);
                    Rectangle Src1 = new Rectangle(LWorld[i].rcSrc.X, 0, cxRem, ClientSize.Height);
                    g.DrawImage(LWorld[i].img, Dst1, Src1, GraphicsUnit.Pixel);
                    //2
                    int cxRem2 = ClientSize.Width - cxRem;
                    Rectangle src2 = new Rectangle(0, 0, cxRem2, ClientSize.Height);
                    Rectangle Dst2 = new Rectangle(cxRem, 0, cxRem2, ClientSize.Height);
                    g.DrawImage(LWorld[i].img, Dst2, src2, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(LWorld[i].img, LWorld[i].rcDst, LWorld[i].rcSrc, GraphicsUnit.Pixel);
                }
            }
            for (int i = 0; i < LHero.Count; i++)
            {
                g.DrawImage(LHero[i].img, LHero[i].x, LHero[i].y);
            }
            for (int i = 0; i < LDoomsday.Count; i++)
            {
                g.DrawImage(LDoomsday[i].img, LDoomsday[i].x, LDoomsday[i].y);
            }
            for (int i = 0; i < LSonicPunch.Count; i++)
            {
                g.DrawImage(LSonicPunch[i].img, LSonicPunch[i].x, LSonicPunch[i].y);
            }
            for (int i = 0; i < LLadder.Count; i++)
            {
                g.DrawImage(LLadder[i].img, LLadder[i].x, LLadder[i].y);
            }
            for (int i = 0; i < LPlatform.Count; i++)
            {
                g.DrawImage(LPlatform[i].img, LPlatform[i].x, LPlatform[i].y);
            }
        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

    }
}
