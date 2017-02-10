#region

using System.Data.Entity.Migrations;

#endregion

namespace RealtimeKanbanBoard.Models.Migrations
{
    public class initalschema : DbMigration
    {
        /// <summary>
        ///     Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Lists",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(),
                    Board_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .Index(t => t.Board_Id);

            CreateTable(
                "dbo.Files",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(),
                    Path = c.String(),
                    Size = c.Long(),
                    Board_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .Index(t => t.Board_Id);

            CreateTable(
                "dbo.Tasks",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(),
                    Order = c.Int(false),
                    List_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
        }

        /// <summary>
        ///     Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "Board_Id", "dbo.Boards");
            DropForeignKey("dbo.Files", "Board_Id", "dbo.Boards");
            DropForeignKey("dbo.Tasks", "List_Id", "dbo.Lists");
            DropIndex("dbo.Files", new[] {"Board_Id"});
            DropIndex("dbo.Lists", new[] {"Board_Id"});
            DropIndex("dbo.Tasks", new[] {"List_Id"});
            DropTable("dbo.Tasks");
            DropTable("dbo.Lists");
            DropTable("dbo.Files");
            DropTable("dbo.Boards");
        }
    }
}