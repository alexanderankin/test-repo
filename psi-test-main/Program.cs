using System;
using System.Diagnostics;
using System.IO;

string exe = "psi-test-child\\bin\\Debug\\net9.0\\psi-test-child.exe";

var psi = new ProcessStartInfo
{
    FileName = exe,
    UseShellExecute = false,
    CreateNoWindow = true,
};

if (OperatingSystem.IsWindows())
    psi.WindowStyle = ProcessWindowStyle.Hidden;

var proc = Process.Start(psi)!;
Console.WriteLine($"Launched child process with PID {proc.Id}");
Console.WriteLine("Parent exiting.");