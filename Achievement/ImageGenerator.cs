/*
MIT License

Copyright (c) 2017 Chris Johnston

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Changes made by Matthew Martin (2022)
*/
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
namespace AchievementAPI
{
    public class ImageGenerator
    {
        public int GenerationCounter { get; private set; } = 0;

        public string GenerateImagePath(string imageID)
        {
            // generate path in format
            // %path%/123456789.png
            string path = Path.Combine(Path.GetTempPath(), $"achievement{imageID}.png");
            return Environment.ExpandEnvironmentVariables(path);
        }

        public string GenerateImage(AchievementRequest request)
        {
            // first determine background image path
            
            AchievementType type = request.achievementType;
            string typeString = Enum.GetName(typeof(AchievementType), type);
            string backgroundImagePath = Path.Combine("Resources/Templates", typeString + ".png");

            // passing the relative path was breaking it, so now just going to pass it the file stream instead
            string path = Path.Combine(Directory.GetCurrentDirectory(), backgroundImagePath);
            string finalImagePath = GenerateImagePath(request.UUID.ToString());
            using (var backgroundStream = new FileStream(path, FileMode.Open))
            {
                using (MagickImage image = new MagickImage(backgroundImagePath))
                {
                    MagickImage headerLayer = new MagickImage(MagickColor.FromRgba(0, 0, 0, 0), image.Width, image.Height);
                    headerLayer.Settings.Font = Path.Combine("Resources/Fonts", "SEGOEUI.TTF");
                    headerLayer.Settings.FontPointsize = 36;
                    headerLayer.Settings.TextGravity = Gravity.Southwest;
                    headerLayer.Settings.FillColor = MagickColor.FromRgb(255, 255, 255);
                    if (type == AchievementType.XboxOne)
                    {
                        var s = $"{request.gamerScore} - {request.achievementName}";
                        headerLayer.Annotate(s, new MagickGeometry(225, 30, 700, 80), Gravity.West);
                    }
                    else if (type == AchievementType.XboxOneRare)
                    {
                        headerLayer.Annotate($"{request.gamerScore} - {request.achievementName}", new MagickGeometry(195, 55, 400, 70), Gravity.West);
                    }
                    else if(type == AchievementType.Xbox360)
                    {
                        headerLayer.Annotate($"{request.gamerScore}G - {request.achievementName}", new MagickGeometry(175, 55, 400, 70), Gravity.West);
                    }
                    if(type == AchievementType.XboxOneRare)
                    {
                        Random r = new Random();
                        int rarePercent = r.Next(1, 5);
                        headerLayer.Annotate($"Rare achievement unlocked - {rarePercent}%", new MagickGeometry(155, 5, 400, 70), Gravity.West);
                    }
                    else if(type == AchievementType.Xbox360)
                    {
                        headerLayer.Annotate($"Achievement unlocked", new MagickGeometry(175, 5, 400, 70), Gravity.West);
                    }
                    image.Composite(headerLayer, CompositeOperator.Over);
                    image.Write(finalImagePath);
                }
            }
            GenerationCounter++;
            return finalImagePath;
        }

        /// <summary>
        /// Deletes an image
        /// </summary>
        /// <param name="achievementID"></param>
        public void DeleteImage(string achievementID)
        {
            File.Delete(GenerateImagePath(achievementID));
        }
    }
}
