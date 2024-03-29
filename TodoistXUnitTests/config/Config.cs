﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TodoistXUnitTests.config
{
    public class Config
    {
        private IConfigurationRoot _configuration;
        public string CurrentProjectDirectory { get; }
        public string CurrentSolutionDirectory { get; }

        public Config()
        {
            string workingDirectory = Environment.CurrentDirectory;
            CurrentProjectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            CurrentSolutionDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile(CurrentProjectDirectory + "/config/appSettings.json")
                .Build();
        }

        public string GetBaseUrl()
        {
            return _configuration.GetSection("baseUrl").Value;
        }

        public string GetBrowserType()
        {
            return _configuration.GetSection("browserType").Value;
        }

        public string GetReportFilename()
        {
            return CurrentSolutionDirectory + @"\reports\testResults.html";
        }

        public string GetUsername()
        {
            return _configuration.GetSection("username").Value;
        }

        public string GetPassword()
        {
            return _configuration.GetSection("password").Value;
        }
    }
}
