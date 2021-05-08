using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DcsBios.Communicator.Configuration
{
    /// <summary>
    /// A nested dictionary class containing all bios commands and their properties for a specific aircraft
    /// </summary>
    public class AircraftBiosConfiguration : Dictionary<string, BiosCategory>
    {
        public string AircraftName { get; private set; } = null!;

        /// <summary>
        /// Parses all json files within the provided directories into <see cref="AircraftBiosConfiguration"/>s (basically
        /// just nested dictionaries)
        /// </summary>
        /// <param name="configLocations">Directories containing json configuration files outlining all available DCS-BIOS inputs/outputs</param>
        /// <returns>Parsed aircraft configurations</returns>
        public static async Task<AircraftBiosConfiguration[]> AllConfigurations(
            params string[] configLocations)
        {
            var configTasks = configLocations
                .SelectMany(GetAllJsonFilesBelowDirectory)
                .Select(BuildFromConfiguration);

            return await Task.WhenAll(configTasks);
        }

        /// <summary>
        /// Creates a configuration from a single json file
        /// </summary>
        /// <param name="configFile">File to create a configuration from</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<AircraftBiosConfiguration> BuildFromConfiguration(FileInfo configFile)
        {
            var fileData = await File.ReadAllTextAsync(configFile.FullName);
            var dcsConfiguration = JsonConvert.DeserializeObject<AircraftBiosConfiguration>(fileData, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                }
            });

            if (dcsConfiguration is null) throw new Exception($"Unable to parse config file {configFile.FullName}");

            dcsConfiguration.AircraftName = Path.GetFileNameWithoutExtension(configFile.FullName);

            return dcsConfiguration;
        }

        private static IEnumerable<FileInfo> GetAllJsonFilesBelowDirectory(string directory)
        {
            var finalPath = FullOrRelativePath(Environment.ExpandEnvironmentVariables(directory));
            return Directory.Exists(finalPath)
                ? new DirectoryInfo(finalPath).EnumerateFiles("*.json", SearchOption.AllDirectories)
                : Enumerable.Empty<FileInfo>();
        }

        private static string FullOrRelativePath(string path)
        {
            return Path.IsPathFullyQualified(path) ? path : Path.Combine(Environment.CurrentDirectory, path);
        }
    }
}