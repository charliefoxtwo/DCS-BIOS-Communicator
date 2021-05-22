
# DCS BIOS Communicator

[![Nuget](https://img.shields.io/nuget/v/DcsBios.Communicator?style=flat-square)](https://www.nuget.org/packages/DcsBios.Communicator/)
[![.NET 5 CI build](https://github.com/charliefoxtwo/DCS-BIOS-Communicator/actions/workflows/ci-build.yml/badge.svg?branch=develop)](https://github.com/charliefoxtwo/DCS-BIOS-Communicator/actions/workflows/ci-build.yml)
[![GitHub](https://img.shields.io/github/license/charliefoxtwo/DCS-BIOS-Communicator?style=flat-square)](LICENSE)
[![Discord](https://img.shields.io/discord/840762843917582347?style=flat-square)](https://discord.gg/rWAF3AdsKT)

DCS BIOS Communicator is a .net 5 library for interacting with DCS-BIOS. It parses the raw data bytes provided by DCS-BIOS and will trigger actions when it receives data.

<img src="https://raw.githubusercontent.com/charliefoxtwo/DCS-BIOS-Communicator/main/DcsBiosCommunicator/resources/airplane.png" alt="DCS BIOS Communicator logo - a vector outline of an airplane" width="150" />

## Features

- Parses data from DCS-BIOS
- Handles pesky UTF-8 symbols
- Might buy you an ice cream sandwich if you're lucky
- Cross platform


## Usage/Examples

```c#
// create a new UDP client for talking to DCS-BIOS
var client = new BiosUdpClient(IPAddress.Parse("239.255.50.10"), 7778, 5010, logger);
client.OpenConnection();

// create your own translator class which implements IBiosTranslator
var biosListener = new BiosListener(client, translator, logger);

// register the json configuration files
var configLocation = "%userprofile%/Saved Games/DCS.openbeta/Scripts/DCS-BIOS/doc/json/";
foreach (var config in await AircraftBiosConfiguration.AllConfigurations(configLocation))
{
    biosListener.RegisterConfiguration(config);
}

// start the listener
biosListener.Start();

```


## Roadmap

- Unit tests


## Acknowledgements

- [Package icon](https://www.flaticon.com/authors/good-ware)
- [readme tools](https://readme.so)
- [badges](https://shields.io)
