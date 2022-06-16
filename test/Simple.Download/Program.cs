using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

string servicename = "Simple.Authorization";
string replacename = "Demo";
string newpath = "\\virtual\\" + replacename;
string servicepath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + servicename;
string newservicename = $"Web.{replacename}.API";
string zippath = Directory.GetCurrentDirectory() + "\\virtual";
string[] services = new string[] { "Application", "Caching", "Domain", "Entity" };
bool multilayer = true;

if (!Directory.Exists(newpath))
{
    Directory.CreateDirectory(newpath);
}
if (multilayer)
{
    MultilDirector(servicepath);
}
else
{
    Director(servicepath);
}
return;
ZipFile.CreateFromDirectory(zippath, $".\\virtual\\{replacename}.zip");
//单层递归文件夹
void Director(string dir)
{
    DirectoryInfo d = new DirectoryInfo(dir);
    DirectoryInfo[] directs = d.GetDirectories();
    foreach (FileInfo file in d.GetFiles())
    {
        WriteFile(dir.Replace("wwwroot", "virtual").Replace(servicename, replacename), file);
    }
    foreach (DirectoryInfo dd in directs)
    {
        Director(dd.FullName);
    }
}


//多层项目生成
void MultilDirector(string dir)
{
    DirectoryInfo d = new DirectoryInfo(dir);
    DirectoryInfo[] directs = d.GetDirectories();
    string newservicepath = dir.Replace("wwwroot", "virtual").Replace(servicename, replacename);
    FileInfo csproj = d.GetFiles().FirstOrDefault(c => c.Name.Contains(servicename));
    CreateSln();
    foreach (DirectoryInfo dd in directs)
    {
        switch (dd.Name)
        {
            case "Application":
            case "Caching":
            case "Domain":
            case "Entity":
            case "Job":
                {
                    CreateCsproj($"{replacename}.{dd.Name}", csproj.OpenText().ReadToEnd());
                }
                break;
            default:
                {
                    CreateCsproj($"Web.{replacename}.API", "");
                }
                break;
        }
    }
    WriteMultil(dir);
}

void WriteMultil(string dir)
{
    DirectoryInfo d = new DirectoryInfo(dir);
    DirectoryInfo[] directs = d.GetDirectories();
    string newservicepath = dir.Replace("wwwroot", "virtual")
         .Replace(servicename, replacename)
         .Replace("Application", $"{replacename}.Application")
         .Replace("Caching", $"{replacename}.Caching")
         .Replace("Domain", $"{replacename}.Domain")
         .Replace("Entity", $"{replacename}.Entity")
         .Replace("Controllers", $"\\Web.{replacename}.API\\Controllers");
    Regex regex = new Regex("Application|Caching|Domain|Entity|Controllers");
    if (!regex.IsMatch(dir))
    {
        newservicepath += $"\\Web.{replacename}.API";
    }
    foreach (FileInfo file in d.GetFiles())
    {
        WriteFile(newservicepath, file);
    }
    foreach (DirectoryInfo dd in directs)
    {
        WriteMultil(dd.FullName);
    }
}
//写入文件
void WriteFile(string path, FileInfo file)
{
    if (!Directory.Exists(path))
    {
        Directory.CreateDirectory(path);
    }
    string filename = file.Name;
    if (filename.Contains(servicename))
    {
        filename = filename.Replace(servicename, replacename);
    }
    if (multilayer && file.Name == $"{servicename}.csproj")
        return;
    StreamReader sr = file.OpenText();
    //创建并写入
    FileStream stream = new FileStream(path + "\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    string content = sr.ReadToEnd().Replace(servicename, replacename);
    sw.Write(content);
    sw.Close();
    stream.Close();
}
//创建csproj文件
void CreateCsproj(string service, string content)
{
    string newservicepath = $"{Directory.GetCurrentDirectory()}\\virtual\\{replacename}\\{service}";
    if (!Directory.Exists(newservicepath))
    {
        Directory.CreateDirectory(newservicepath);
    }
    FileStream stream = new FileStream(newservicepath + $"\\{service}.csproj", FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    sw.Write(content);
    sw.Close();
    stream.Close();
}
void CreateSln()
{
    string slnpath = $"{Directory.GetCurrentDirectory()}\\virtual\\{replacename}";
    if (!Directory.Exists(slnpath))
    {
        Directory.CreateDirectory(slnpath);
    }
    FileInfo file = new FileInfo("sln.txt");
    FileStream stream = new FileStream(slnpath + $"\\{replacename}.sln", FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    sw.Write(file.OpenText().ReadToEnd().Replace(servicename, replacename));
    sw.Close();
    stream.Close();
}
