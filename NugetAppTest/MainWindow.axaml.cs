using Avalonia.Controls;
using System;

namespace NugetAppTest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        Console.WriteLine("[NugetAppTest] MainWindow loaded with VideoPlayerControl");
    }
}