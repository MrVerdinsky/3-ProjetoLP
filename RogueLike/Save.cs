using System;
using System.Collections.Generic;
using System.IO;

namespace RogueLike
{
    /// <summary>
    /// Creates and loads save files
    /// </summary>
    internal class Save
    {
        /// <summary>
        /// Auto-implemented property that represents levels number
        /// </summary>
        /// <value>Gets/Sets the value for the int Level</value>
        private int     Level           { get; set; }

        /// <summary>
        /// Auto-implemented property that represents the save file name
        /// </summary>
        /// <value>Gets/Sets the value for the string FileName</value>
        private string  FileName        { get; set; }

        /// <summary>
        /// Gets the necessary information to write a save file
        /// </summary>
        public void GetSave(int level, string intention)
        {
            Input input = new Input();
            Renderer print = new Renderer();
            Level   = level;
            if (intention == "quit")
                print.SaveBeforeLeave();
            else if(intention == "continue")
                print.GetSaveIntention();
            if (input.GetSaveIntention() == "y")
            {
                print.InsertFileName();
                FileName = input.InsertName();
                SaveFile();
            }
            else
                return;
        }


        /// <summary>
        /// Loads a save file
        /// </summary>
        public void LoadSave(Level level, string fileName)
        {
        
            string line;
            Renderer print = new Renderer(); 
            List<int> values_list = new List<int>();
            FileName = fileName;
            if (!(File.Exists($@"RogueLike\Saves\{FileName}")))
            {
                print.InvalidFileName();
                Environment.Exit(1);
            }
            StreamReader saveReader = new StreamReader(
            $@"RogueLike\Saves\{FileName}");
            using (saveReader)
            {
                while ((line = saveReader.ReadLine()) != null)  
                {
                    // 
                    values_list.Add(Convert.ToInt32(line.Split(": ")[1]));
                }
            }

            // Sets each atribute to its correspondent element on the
            // values_list
            level.LevelNum          = values_list[0];
            Player.HP               = values_list[1];
            Game.Seed               = values_list[2];
            Game.rows               = values_list[3];
            Game.columns            = values_list[4];
        
        }
   
        /// <summary>
        /// Writes the important values to save the game into the saving file
        /// </summary>
        private void SaveFile()
        {
            // Creates a Saves directory if it doesn't exist already
            Directory.CreateDirectory(@"RogueLike\Saves");
            StreamWriter save = new StreamWriter(
            $@"RogueLike\Saves\{FileName}.sav");
            using (save)
            {
                // Writes the necessary info to the file
                save.WriteLine($"Level: {Level}");
                save.WriteLine($"HP: {Player.HP}");
                save.WriteLine($"Seed: {Game.Seed}");
                save.WriteLine($"Rows: {Game.rows}");
                save.WriteLine($"Column: {Game.columns}");
            }
            save.Close();
        }
    }
}