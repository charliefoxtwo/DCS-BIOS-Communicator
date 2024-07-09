
# DCS BIOS Communicator

[![Nuget](https://img.shields.io/nuget/v/DcsBios.Communicator?style=for-the-badge)](https://www.nuget.org/packages/DcsBios.Communicator/)
[![.NET 8 CI build](https://img.shields.io/github/actions/workflow/status/charliefoxtwo/DCS-BIOS-Communicator/ci-build.yml?style=for-the-badge)](https://github.com/charliefoxtwo/DCS-BIOS-Communicator/actions/workflows/ci-build.yml)
[![GitHub](https://img.shields.io/github/license/charliefoxtwo/DCS-BIOS-Communicator?style=for-the-badge)](LICENSE)
[![Discord](https://img.shields.io/discord/840762843917582347?style=for-the-badge)](https://discord.gg/rWAF3AdsKT)

<br />
<div align="center">
  <a href="https://github.com/charliefoxtwo/DCS-BIOS-Communicator">
    <img src="https://raw.githubusercontent.com/charliefoxtwo/DCS-BIOS-Communicator/main/DcsBiosCommunicator/resources/airplane.png" alt="DCS BIOS Communicator logo - a vector outline of an airplane" width="80" height="80">
  </a>

<h3 align="center">DCS-BIOS Communicator</h3>

  <p align="center">
    a .NET library for communicating with DCS-BIOS
    <br />
  </p>
</div>

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#features">Features</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li><a href="#benchmarks">Benchmarks</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

DCS BIOS Communicator is a .net 8 library for interacting with DCS-BIOS. It parses the raw data bytes provided by DCS-BIOS and will trigger actions when it receives data.

## Features

- Parses data from DCS-BIOS
- Handles pesky UTF-8 symbols
- Might buy you an ice cream sandwich if you're lucky
- Cross-platform

## Usage

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

## Benchmarks
Benchmarks are strictly for parsing data received from the UDP server and do not include network time or UDP receive time.
```
BenchmarkDotNet v0.13.12, Windows 11
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.206

Job=.NET 8.0  Runtime=.NET 8.0
```
| Method     | Mean      | Error     | StdDev    | Ratio |
|----------- |----------:|----------:|----------:|------:|
| TestString | 30.043 ns | 0.1793 ns | 0.1677 ns |  1.00 |
|            |           |           |           |       |
| TestInt    |  1.290 ns | 0.0114 ns | 0.0106 ns |  1.00 |

## Roadmap

- Unit tests

## Acknowledgements

- [Package icon](https://www.flaticon.com/authors/good-ware)
- [readme tools](https://readme.so)
- [badges](https://shields.io)
