using FluentMigrator;

namespace MetricsManager.DAL.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");


            Delete.Table("dotnetmetrics");


            Delete.Table("hddmetrics");


            Delete.Table("networkmetrics");


            Delete.Table("rammetrics");

            Delete.Table("agents");
        }

        public override void Up()
        {
            Create.Table("cpumetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsDouble()
                .WithColumn("agentid").AsInt32();


            Create.Table("dotnetmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentid").AsInt32(); 


            Create.Table("hddmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentid").AsInt32(); 


            Create.Table("networkmetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentid").AsInt32(); 


            Create.Table("rammetrics")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64()
                .WithColumn("agentid").AsInt32();


            Create.Table("agents")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("agenturl").AsString()
                .WithColumn("agentid").AsInt32();
        }
    }
}
