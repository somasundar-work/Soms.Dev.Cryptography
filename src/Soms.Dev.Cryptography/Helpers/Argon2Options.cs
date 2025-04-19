using System;

namespace Soms.Dev.Cryptography;

public class Argon2Options
{
    public int Iterations { get; set; } = 4; // Default iterations
    public int MemorySize { get; set; } = 65536; // Default momory size
    public int HashSize { get; set; } = 32; // Default hash size
    public int DegreeOfParallelism { get; set; } = 2; // Default Degree Of Parallelism algorithm
}
