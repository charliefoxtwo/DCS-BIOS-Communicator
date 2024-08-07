﻿using System.Collections.Generic;

// ReSharper disable UnusedMember.Global

namespace DcsBios.Communicator.Configuration;

// ReSharper disable once ClassNeverInstantiated.Global
public class BiosControl
{
    public string Category { get; set; } = null!;

    // TODO: enumify
    public string ControlType { get; set; } = null!;
    public string Identifier { get; set; } = null!;
    public List<BiosInput> Inputs { get; set; } = null!;

    public List<BiosOutput> Outputs { get; set; } = null!;
}
