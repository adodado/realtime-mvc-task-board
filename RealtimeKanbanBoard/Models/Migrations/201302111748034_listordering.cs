using System.Data.Entity.Migrations;

namespace RealtimeKanbanBoard.Migrations
{
    public partial class listordering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lists", "Order", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Lists", "Order");
        }
    }
}