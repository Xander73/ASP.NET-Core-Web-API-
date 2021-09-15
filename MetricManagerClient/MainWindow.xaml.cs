using System.Windows;
using MetricManagerClient.Agent;
using System.Collections.Generic;
using MetricsManagerClient.Client;
using System.Net.Http;
using System.Linq;
using MetricsManagerClient.Responses;
using System.Runtime.InteropServices;
using System.IO;

namespace MetricsManagerClient
{
    public enum Metrics { Cpu, DotNet, Hdd, Network, Ram };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<double> lastTime = new List<double> { 0d, 0d, 0d, 0d, 0d};

        public List<double> LastTime 
        { 
            get => lastTime;
        }

        AgentConnect _agent;

        public MainWindow()
        {
            InitializeComponent();
            _agent = new AgentConnect(new string[0]);
            CpuChart.Heading.Text = "CPU Load";
            CpuChart.AxisY.Text = "\nLoaded, %";
            HDDChart.Heading.Text = "HDD Occupied";
            HDDChart.AxisY.Text = "\nMemory occupied, %";
            RamChart.Heading.Text = "RAM Occupied";
            RamChart.AxisY.Text = "\nMemory occupied, %";
            DotNetChart.Heading.Text = ".NET Allocated Memory";
            DotNetChart.AxisY.Text = "\nAllocated Bytes, %";

            CpuChart.update.Click += Button_Cpu;
            HDDChart.update.Click += Button_Hdd;
            RamChart.update.Click += Button_Ram;
            DotNetChart.update.Click += Button_DotNet;
        }

        public void Button_Cpu(object sender, RoutedEventArgs e)
        {
            CpuChart.ColumnSeriesValues[0].Values.Clear();

            var metrics = (from metric in AgentConnect
                          .GetCpuMetrics(
                          lastTime[(int)Metrics.Cpu], 
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics 
                          orderby metric.Time descending
                          select metric).Take<CpuMetricDto>(10).ToList();

            foreach (var item in metrics)
            {
                CpuChart.ColumnSeriesValues[0].Values.Add((double)item.Value); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Cpu] = metrics.Count >= 0 ? metrics[metrics.Count - 1].Value : 0;
        }


        public void Button_DotNet(object sender, RoutedEventArgs e)
        {            
            DotNetChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetDotNetMetrics(
                          lastTime[(int)Metrics.DotNet],
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                          orderby metric.Time descending
                          select metric).Take<DotNetMetricDto>(10).ToList();

            MEMORYSTATUSEX memEx = new MEMORYSTATUSEX();
            GlobalMemoryStatusEx(memEx);

            var ram = memEx.ullTotalPhys/1000;

            foreach (var item in metrics)
            {
                DotNetChart.ColumnSeriesValues[0].Values.Add((double)item.Value * 100 / ram); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.DotNet] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }


        public void Button_Hdd(object sender, RoutedEventArgs e)
        {
            HDDChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetHddMetrics(
                          lastTime[(int)Metrics.Hdd], 
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                          orderby metric.Time descending
                          select metric).Take<HddMetricDto>(10).ToList();

            DriveInfo driveInfo = new DriveInfo("C");
            var totalSize = driveInfo.TotalSize/1000_000;

            foreach (var item in metrics)
            {
                HDDChart.ColumnSeriesValues[0].Values.Add((double)item.Value * 100/ totalSize); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Hdd] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }

        
        public void Button_Ram(object sender, RoutedEventArgs e)
        {
            RamChart.ColumnSeriesValues[0].Values.Clear();

            var metrics = (from metric in AgentConnect
                          .GetRamMetrics(
                          lastTime[(int)Metrics.Ram],
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                           orderby metric.Time descending
                           select metric).Take<RamMetricDto>(10).ToList();


            MEMORYSTATUSEX memEx = new MEMORYSTATUSEX();
            GlobalMemoryStatusEx(memEx);

            var ram = memEx.ullTotalPhys/1000_000;

            foreach (var item in metrics)
            {
                RamChart.ColumnSeriesValues[0].Values.Add((double)item.Value * 100/ ram); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Ram] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }
        

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        [StructLayout(LayoutKind.Sequential)]
        public class MEMORYSTATUSEX
        {
            /// <summary>
            /// Size of the structure, in bytes. You must set this member before calling GlobalMemoryStatusEx. 
            /// </summary>
            public uint dwLength;

            /// <summary>
            /// Number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use). 
            /// </summary>
            public uint dwMemoryLoad;

            /// <summary>
            /// Total size of physical memory, in bytes.
            /// </summary>
            public ulong ullTotalPhys;

            /// <summary>
            /// Size of physical memory available, in bytes. 
            /// </summary>
            public ulong ullAvailPhys;

            /// <summary>
            /// Size of the committed memory limit, in bytes. This is physical memory plus the size of the page file, minus a small overhead. 
            /// </summary>
            public ulong ullTotalPageFile;

            /// <summary>
            /// Size of available memory to commit, in bytes. The limit is ullTotalPageFile. 
            /// </summary>
            public ulong ullAvailPageFile;

            /// <summary>
            /// Total size of the user mode portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullTotalVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the user mode portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullAvailVirtual;

            /// <summary>
            /// Size of unreserved and uncommitted memory in the extended portion of the virtual address space of the calling process, in bytes. 
            /// </summary>
            public ulong ullAvailExtendedVirtual;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:MEMORYSTATUSEX"/> class.
            /// </summary>
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }
    }
}




