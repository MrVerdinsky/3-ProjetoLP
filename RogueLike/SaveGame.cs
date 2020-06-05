/* using System;
using System.IO;
namespace RogueLike
{
    public class SaveGame
    {
        private string FileName     {get; set;}
        Level CurrentLevel;
        Map[,] Map;
        
        public SaveGame(Level  level, Map[,] map)
        {
            CurrentLevel = level;
            Map = map;
        }

        public void CreateSafeFile()
        {
            //const char space    = ' ';
            int i = 0;
            int j = 0;
           
            FileName = Console.ReadLine();
            StreamWriter safeF = new StreamWriter(
                    $@"RogueLike\{FileName}.sav");
            if(File.Exists( $@"RogueLike\{FileName}.sav"))
            {
                
                using (safeF)
                {
                    safeF.WriteLine("PUDIM");
                    safeF.WriteLine(CurrentLevel);
                    safeF.WriteLine(CurrentLevel.ColumnNum);
                    safeF.WriteLine(Map[CurrentLevel.player.Position.Row,
                    CurrentLevel.player.Position.Column]);
                    
                    foreach(Enemy enemy in CurrentLevel.Enemies)
                    {
                        i++;
                        safeF.WriteLine(Map[CurrentLevel.Enemies[i].Position.Row,
                    CurrentLevel.Enemies[i].Position.Column]);
                    }
                    foreach(PowerUp powerUp in CurrentLevel.PowerUps)
                    {
                        j++;
                        safeF.WriteLine(Map[CurrentLevel.PowerUps[j].Position.Row,
                    CurrentLevel.PowerUps[j].Position.Column]);
                    }
                    for (int k = 0; k <CurrentLevel.RowNum; k++)
                    {
                        for (int x = 0; x <CurrentLevel.ColumnNum; x++)
                        {
                            if(Map[k,x].Position.HasWall)
                            {
                                safeF.WriteLine(Map[x,k]);
                            }
                        }
                    }
                }
            }
           

            else
            {
                safeF.WriteLine("XABLAU");
            }    
        }
    }
} */