using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationTests
{
    public abstract class Multiplatform
    {
        public abstract string GetPOSIXpath();

        public abstract string GetWindowsPath();

        public string GetPlatformPath()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            string path = String.Empty;
            switch (osInfo.Platform)
            {
                case PlatformID.MacOSX:
                    {
                        path = GetPOSIXpath();
                        break;
                    }
                case PlatformID.Unix:
                    {
                        path = GetPOSIXpath();
                        break; 
                    }
                default:
                    {
                        path = GetWindowsPath();
                        break;
                    }
            }
            return path;
        }
    }

    public class NotFoundPath : Multiplatform
    {
        public override string GetPOSIXpath()
        {
            return "/asdflksafasdfasdf/data.txt";
        }
        public override string GetWindowsPath()
        {
            return @"D:\asdafdsfsfasfasd\data.txt";
        }
    }

    public class EasyPath : Multiplatform
    {
        public override string GetPOSIXpath()
        {
            return "/tmp/data.txt";
        }
        public override string GetWindowsPath()
        {
            return @"D:\data.txt";
        }
    }



}
