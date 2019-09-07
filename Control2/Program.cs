using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control2
{
    class BaseClass
    {
        public string Size { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public virtual void Parse(string input)
        {
            string[] splitFileInfo = input.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Size = splitFileInfo[1];
            Name = splitFileInfo[0];
            var ext = splitFileInfo[0].Split('.');
            Extension = ext[1];
        }
        public virtual void Print()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"       Extension: {Extension}");
            Console.WriteLine($"       Size: {Size}");
        }
       

        class OutputText : BaseClass

        {
            public string Content { get; set; }
            public override void Parse(string input)
            {
                base.Parse(input);
                string[] inputSP = input.Split(';');
                Content = inputSP[1];
            }
            public override void Print()
            {
                base.Print();
                Console.WriteLine($"       Content: {Content}");
            }
        }

        class OutputImages : BaseClass

        {
            public string Resolution { get; set; }
            public override void Parse(string input)
            {
                base.Parse(input);
                string[] inputSP = input.Split(';');
                Resolution = inputSP[1];
            }
            public override void Print()
            {
                base.Print();
                Console.WriteLine($"       Resolution: {Resolution}");
            }
        }

        class OutputMovies : OutputImages

        {
            public string Length { get; set; }
            public override void Parse(string input)
            {
                base.Parse(input);
                string[] inputSP = input.Split(';');
                Length = inputSP[2];
            }
            public override void Print()
            {
                base.Print();
                Console.WriteLine($"       Length: {Length}");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                string a = @"Text:file.txt(6B); Some string content
            Image:img.bmp(19MB); 1920x1080
            Text:data.txt(12B); Another string
            Text:data1.txt(7B); Yet another string
            Movie:logan.2017.mkv(19GB); 1920x1080; 2h12m";
                ;
                
                List<OutputText> outputTexts = new List<OutputText>();
                List<OutputImages> outputImages = new List<OutputImages>();
                List<OutputMovies> outputMovies = new List<OutputMovies>();
                string[] files = a.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < files.Length; i++)
                {
                    string[] split = files[i].Split(':');
                    string fileType = split[0].ToLower().Trim(' ');
                    switch (fileType)
                    {
                        case "text":
                            OutputText text = new OutputText();
                            text.Parse(split[1]);
                            outputTexts.Add(text);
                            break;
                        case "image":
                            OutputImages image = new OutputImages();
                            image.Parse(split[1]);
                            outputImages.Add(image);
                            break;
                        case "movie":
                            OutputMovies movies = new OutputMovies();
                            movies.Parse(split[1]);
                            outputMovies.Add(movies);
                            break;
                    }
                }
                
                foreach(var p in outputTexts)
                {
                    p.Print();
                }
                foreach (var p in outputImages)
                {
                    p.Print();
                }
                foreach (var p in outputMovies)
                {
                    p.Print();
                }

                Console.ReadLine();
            }
        }
    }
}
