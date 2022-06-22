using Simple.Core.Extensions;
using System.IO.Compression;
using System.Text.RegularExpressions;

string SERVICENAME = "Simple.Authorization";
string REPLACENAME = args.Get("-name");
if (string.IsNullOrWhiteSpace(REPLACENAME))
{
    Console.WriteLine("请输入项目名称");
    return;
}
string newpath = "\\virtual\\" + REPLACENAME;
string servicepath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + SERVICENAME;
string zippath = Directory.GetCurrentDirectory() + "\\virtual";
bool multilayer = args.Get("-multilayer", true);
if (Directory.Exists(newpath))
{
    Directory.Delete(newpath);
}
else
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
ZipFile.CreateFromDirectory(zippath, $".\\virtual\\{REPLACENAME}.zip");
//单层递归文件夹
void Director(string dir)
{
    DirectoryInfo d = new DirectoryInfo(dir);
    DirectoryInfo[] directs = d.GetDirectories();
    foreach (FileInfo file in d.GetFiles())
    {
        WriteFile(dir.Replace("wwwroot", "virtual").Replace(SERVICENAME, REPLACENAME), file);
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
    CreateSln();
    foreach (DirectoryInfo dd in directs)
    {
        switch (dd.Name)
        {
            case "Application":
            case "Consumer":
            case "Entity":
            case "Job":
                {
                    CreateCsproj(dd.Name);
                }
                break;
            default:
                {
                    CreateCsproj("WebAPI");
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
         .Replace(SERVICENAME, REPLACENAME)
         .Replace("Application", $"{REPLACENAME}.Application")
         .Replace("Entity", $"{REPLACENAME}.Entity")
         .Replace("Job", $"{REPLACENAME}.Entity")
         .Replace("Consumer", $"{REPLACENAME}.Entity")
         .Replace("Controllers", $"\\Web.{REPLACENAME}.API\\Controllers");
    Regex regex = new Regex("Application|Entity|Controllers|Job|Consumer");
    if (!regex.IsMatch(dir))
    {
        newservicepath += $"\\Web.{REPLACENAME}.API";
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
    if (filename.Contains(SERVICENAME))
    {
        filename = filename.Replace(SERVICENAME, REPLACENAME);
    }
    if (multilayer && file.Name == $"{SERVICENAME}.csproj")
        return;
    StreamReader sr = file.OpenText();
    FileStream stream = new FileStream(path + "\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    string content = sr.ReadToEnd().Replace(SERVICENAME, REPLACENAME);
    sw.Write(content);
    sw.Close();
    stream.Close();
}
//创建csproj文件
void CreateCsproj(string service)
{
    string servicename = $"{REPLACENAME}.{service}";
    if (service == "WebAPI")
    {
        servicename = $"Web.{REPLACENAME}.API";
    }
    string newservicepath = $"{Directory.GetCurrentDirectory()}\\virtual\\{REPLACENAME}\\{servicename}";
    if (!Directory.Exists(newservicepath))
    {
        Directory.CreateDirectory(newservicepath);
    }
    FileInfo file = new FileInfo($"static/{service.ToLower()}.txt");
    FileStream stream = new FileStream(newservicepath + $"\\{servicename}.csproj", FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    sw.Write(file.OpenText().ReadToEnd().Replace(SERVICENAME, REPLACENAME));
    sw.Close();
    stream.Close();
}
//创建sln文件
void CreateSln()
{
    string slnpath = $"{Directory.GetCurrentDirectory()}\\virtual\\{REPLACENAME}";
    if (!Directory.Exists(slnpath))
    {
        Directory.CreateDirectory(slnpath);
    }
    FileInfo file = new FileInfo("static/sln.txt");
    FileStream stream = new FileStream(slnpath + $"\\{REPLACENAME}.sln", FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter sw = new StreamWriter(stream);
    string project = Guid.NewGuid().ToString().ToUpper();
    string application = Guid.NewGuid().ToString().ToUpper();
    string entity = Guid.NewGuid().ToString().ToUpper();
    string web = Guid.NewGuid().ToString().ToUpper();
    string solu = Guid.NewGuid().ToString().ToUpper();
    sw.Write(file.OpenText().ReadToEnd()
                 .Replace(SERVICENAME, REPLACENAME)
                 .Replace("Web.Authorization.API", $"Web.{REPLACENAME}.API")
                 .Replace("$PROJECT", project)
                 .Replace("$APPLICATION", application)
                 .Replace("$ENTITY", entity)
                 .Replace("$WEB", web)
                 .Replace("$SOLU", solu));
    sw.Close();
    stream.Close();
}
