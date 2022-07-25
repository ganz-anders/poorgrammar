StreamWriter sw = new StreamWriter("TestFile.txt");
string myString="TestTest";
sw.WriteLine(myString);
sw.Close();

/*FileStream sb = new FileStream("MyFile.txt", FileMode.OpenOrCreate);
char[] b = {'a','b','c','d','e','f','g','h','i','j','k','l','m'};
StreamWriter sw = new StreamWriter("MyFile.txt");
sw.Write(b, 3, 8);
sw.Close();
*/
/*
// Get the directories currently on the C drive.
DirectoryInfo[] cDirs = new DirectoryInfo(@"c:\").GetDirectories();

// Write each directory name to a file.
using (StreamWriter sw = new StreamWriter("CDriveDirs.txt"))
{
    foreach (DirectoryInfo dir in cDirs)
    {
        sw.WriteLine(dir.Name);
    }
}*/