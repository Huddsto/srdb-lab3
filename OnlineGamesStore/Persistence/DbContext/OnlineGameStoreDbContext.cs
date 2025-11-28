using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Models;
using OnlineGamesStore.Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGamesStore.Persistence.DbContext
{
    public class OnlineGamesStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public OnlineGamesStoreDbContext(DbContextOptions<OnlineGamesStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Developers> Developers { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseLog> PurchaseLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnlineGamesStoreDbContext).Assembly);
        }

        public void EnsureStoredProceduresAndFunctions()
        {
            Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'usp_insert_developer')
                EXEC('
                CREATE PROCEDURE usp_insert_developer
                    @new_name NVARCHAR(100),
                    @new_country NVARCHAR(50),
                    @new_website NVARCHAR(255)
                AS
                BEGIN
                    INSERT INTO Developers (Name, Country, Website)
                    VALUES (@new_name, @new_country, @new_website);
                END');");

            Database.ExecuteSqlRaw(@"
            IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usr_ins_sale]'))
                DROP PROCEDURE [dbo].[usr_ins_sale];

            EXEC('
            CREATE PROCEDURE usr_ins_sale
                @GameId INT,
                @UserId INT,
                @Quantity INT,
                @Price MONEY
            AS
            BEGIN
                SET NOCOUNT ON;

                IF NOT EXISTS (SELECT 1 FROM Games WHERE Id = @GameId)
                BEGIN
                    RAISERROR (''Game with #%d does not exist'', 16, 1, @GameId);
                    RETURN;
                END;

                IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
                BEGIN
                    RAISERROR (''User with #%d does not exist'', 16, 1, @UserId);
                    RETURN;
                END;

                INSERT INTO Purchases(GameId, UserId, Quantity, TotalPrice, PurchaseDate)
                VALUES (@GameId, @UserId, @Quantity, @Price, GETDATE());
            END');");

            Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Games_Less_Than_Avg')
            EXEC('
            CREATE FUNCTION dbo.Games_Less_Than_Avg()
            RETURNS INT
            AS
            BEGIN
                DECLARE @RowCount INT;
                SET @RowCount = (
                    SELECT COUNT(*)
                    FROM Games
                    WHERE Price < (SELECT AVG(Price) FROM Games)
                );
                RETURN @RowCount;
            END');");

            Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'Count_Games_By_Price')
            EXEC('
            CREATE FUNCTION dbo.Count_Games_By_Price (@new_price MONEY)
            RETURNS INT
            AS
            BEGIN
                DECLARE @temp_count INT;
                SET @temp_count = (
                    SELECT COUNT(*)
                    FROM Games
                    WHERE Price < @new_price
                );
                RETURN @temp_count;
            END');");

            Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetMaxOrderByGameName')
            EXEC('
            CREATE FUNCTION GetMaxOrderByGameName(@GameName NVARCHAR(100))
            RETURNS @Result TABLE
            (
                Message NVARCHAR(200),
                Назва_гри NVARCHAR(100),
                Код_покупки INT,
                Дата_покупки DATETIME,
                Кількість INT,
                Покупець NVARCHAR(100),
                Розробник NVARCHAR(100)
            )
            AS
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM Games WHERE Name = @GameName)
                BEGIN
                    INSERT INTO @Result (Message)
                    VALUES (N''Відсутні замовлення товару ""'' + @GameName + N''"" (гра не знайдена)'');
                    RETURN;
                END;

                IF NOT EXISTS (
                    SELECT 1
                    FROM Purchases pr
                    JOIN Games g ON pr.GameId = g.Id
                    WHERE g.Name = @GameName
                )
                BEGIN
                    INSERT INTO @Result (Message)
                    VALUES (N''Відсутні замовлення товару ""'' + @GameName + N''""'');
                    RETURN;
                END;

                INSERT INTO @Result (Назва_гри, Код_покупки, Дата_покупки, Кількість, Покупець, Розробник)
                SELECT TOP 1
                    g.Name,
                    pr.Id,
                    pr.PurchaseDate,
                    pr.Quantity,
                    u.Name + '' '' + u.Surname,
                    d.Name
                FROM Games g
                JOIN Purchases pr ON g.Id = pr.GameId
                JOIN Users u ON pr.UserId = u.Id
                LEFT JOIN Developers d ON g.DeveloperId = d.Id
                WHERE g.Name = @GameName
                ORDER BY pr.Quantity DESC, pr.TotalPrice DESC;

                RETURN;
            END');");

            Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetMostExpensiveGameByDate')
            EXEC('
            CREATE FUNCTION GetMostExpensiveGameByDate(@PurchaseDate DATE)
            RETURNS @Result TABLE
            (
                Message NVARCHAR(200),
                Назва_гри NVARCHAR(100),
                Код_покупки INT,
                Ціна DECIMAL(10,2),
                Кількість INT,
                Загальна_сума DECIMAL(10,2),
                Покупець NVARCHAR(100),
                Розробник NVARCHAR(100)
            )
            AS
            BEGIN
                IF NOT EXISTS (
                    SELECT 1
                    FROM Purchases p
                    WHERE CAST(p.PurchaseDate AS DATE) = @PurchaseDate
                )
                BEGIN
                    INSERT INTO @Result (Message)
                    VALUES (N''Товар не був реалізований у ""'' + CONVERT(NVARCHAR(10), @PurchaseDate, 104) + N''""'');
                    RETURN;
                END;

                INSERT INTO @Result (Назва_гри, Код_покупки, Ціна, Кількість, Загальна_сума, Покупець, Розробник)
                SELECT TOP 1
                    g.Name,
                    p.Id,
                    g.Price,
                    p.Quantity,
                    p.TotalPrice,
                    u.Name + '' '' + u.Surname,
                    d.Name
                FROM Purchases p
                JOIN Games g ON p.GameId = g.Id
                JOIN Users u ON p.UserId = u.Id
                LEFT JOIN Developers d ON g.DeveloperId = d.Id
                WHERE CAST(p.PurchaseDate AS DATE) = @PurchaseDate
                ORDER BY g.Price DESC;

                RETURN;
            END');");
        }
    }
}
