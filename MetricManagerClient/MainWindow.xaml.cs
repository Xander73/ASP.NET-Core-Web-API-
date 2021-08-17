using System.Windows;
using MetricManagerClient.Agent;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using MetricsManagerClient.Client;
using System.Net.Http;
using System.Linq;
using MetricsManagerClient.Responses;

namespace MetricsManagerClient
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Metrics { Cpu, DotNet, Hdd, Network, Ram };

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
            HDDChart.Heading.Text = "HDD Metrics";
            RamChart.Heading.Text = "RAM Metrics";
            DotNetChart.Heading.Text = ".NET Metrics";
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });


        public void Button_Cpu(object sender, RoutedEventArgs e)
        {
            CpuChart.Heading.Text = "CPU Metrics";
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


        private void Button_DotNet(object sender, RoutedEventArgs e)
        {
            DotNetChart.Heading.Text = ".NET Metrics";
            DotNetChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetDotNetMetrics(
                          lastTime[(int)Metrics.DotNet], 
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                          orderby metric.Time descending
                          select metric).Take<DotNetMetricDto>(10).ToList();

            foreach (var item in metrics)
            {
                DotNetChart.ColumnSeriesValues[0].Values.Add((double)item.Value / 10_000); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.DotNet] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }


        private void Button_Hdd(object sender, RoutedEventArgs e)
        {
            HDDChart.Heading.Text = "HDD Metrics";
            HDDChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetHddMetrics(
                          lastTime[(int)Metrics.Hdd], 
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                          orderby metric.Time descending
                          select metric).Take<HddMetricDto>(10).ToList();

            foreach (var item in metrics)
            {
                HDDChart.ColumnSeriesValues[0].Values.Add((double)item.Value / 10_000); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Hdd] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }


        private void Button_Network(object sender, RoutedEventArgs e)
        {
            CpuChart.Heading.Text = "Network Metrics";
            CpuChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetNetworkMetrics(
                          lastTime[(int)Metrics.Network], 
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                          orderby metric.Time descending
                          select metric).Take<NetworkMetricDto>(10).ToList();

            foreach (var item in metrics)
            {
                CpuChart.ColumnSeriesValues[0].Values.Add((double)item.Value + 50); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Network] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }


        private void Button_Ram(object sender, RoutedEventArgs e)
        {
            RamChart.Heading.Text = "RAM Metrics";
            RamChart.ColumnSeriesValues[0].Values.Clear();
            var metrics = (from metric in AgentConnect
                          .GetRamMetrics(
                          lastTime[(int)Metrics.Ram],
                          new MetricsAgentClient(new HttpClient()))
                          .Metrics
                           orderby metric.Time descending
                           select metric).Take<RamMetricDto>(10).ToList();

            foreach (var item in metrics)
            {
                RamChart.ColumnSeriesValues[0].Values.Add((double)item.Value / 100); //приводим к красивому виду на графике
            }

            lastTime[(int)Metrics.Ram] = metrics.Count > 0 ? metrics[metrics.Count - 1].Value : 0;
        }
    }
}




