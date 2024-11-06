using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addalltables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "framework");

            migrationBuilder.EnsureSchema(
                name: "movie_data");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                schema: "movie_data",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    release_year = table.Column<int>(type: "integer", nullable: true),
                    runtime_minutes = table.Column<int>(type: "integer", nullable: true),
                    plot = table.Column<string>(type: "text", nullable: true),
                    is_adult = table.Column<bool>(type: "boolean", nullable: false),
                    poster_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    box_office = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    production = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.movie_id);
                });

            migrationBuilder.CreateTable(
                name: "name_ratings",
                schema: "framework",
                columns: table => new
                {
                    person_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    weighted_rating = table.Column<decimal>(type: "numeric", nullable: false),
                    total_votes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_name_ratings", x => x.person_id);
                });

            migrationBuilder.CreateTable(
                name: "people",
                schema: "movie_data",
                columns: table => new
                {
                    person_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birth_year = table.Column<int>(type: "integer", nullable: true),
                    death_year = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.person_id);
                });

            migrationBuilder.CreateTable(
                name: "professions",
                schema: "movie_data",
                columns: table => new
                {
                    profession_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profession_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professions", x => x.profession_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "framework",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_genres", x => new { x.movie_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_movie_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalSchema: "movie_data",
                        principalTable: "genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movie_data",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_sources",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    source_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    source_url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_sources", x => new { x.movie_id, x.source_name });
                    table.ForeignKey(
                        name: "FK_movie_sources_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movie_data",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    averagerating = table.Column<decimal>(type: "numeric(5,1)", nullable: false),
                    numvotes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.movie_id);
                    table.ForeignKey(
                        name: "FK_ratings_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movie_data",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_cast",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    person_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    character = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ordering = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_cast", x => new { x.movie_id, x.person_id });
                    table.ForeignKey(
                        name: "FK_movie_cast_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movie_data",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_cast_people_person_id",
                        column: x => x.person_id,
                        principalSchema: "movie_data",
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_crew",
                schema: "movie_data",
                columns: table => new
                {
                    movie_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    person_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_crew", x => new { x.movie_id, x.person_id, x.Role });
                    table.ForeignKey(
                        name: "FK_movie_crew_movies_movie_id",
                        column: x => x.movie_id,
                        principalSchema: "movie_data",
                        principalTable: "movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_crew_people_person_id",
                        column: x => x.person_id,
                        principalSchema: "movie_data",
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_professions",
                schema: "movie_data",
                columns: table => new
                {
                    person_id = table.Column<string>(type: "character varying(15)", nullable: false),
                    profession_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_professions", x => new { x.person_id, x.profession_id });
                    table.ForeignKey(
                        name: "FK_person_professions_people_person_id",
                        column: x => x.person_id,
                        principalSchema: "movie_data",
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_professions_professions_profession_id",
                        column: x => x.profession_id,
                        principalSchema: "movie_data",
                        principalTable: "professions",
                        principalColumn: "profession_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "framework",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "framework",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "framework",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "framework",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookmarks",
                schema: "framework",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookmarks", x => new { x.user_id, x.movie_id });
                    table.ForeignKey(
                        name: "FK_bookmarks_users_user_id1",
                        column: x => x.user_id1,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "notes",
                schema: "framework",
                columns: table => new
                {
                    note_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    movie_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    actor_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.note_id);
                    table.ForeignKey(
                        name: "FK_notes_users_user_id1",
                        column: x => x.user_id1,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "search_history",
                schema: "framework",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    search_query = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    search_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    user_id1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_search_history", x => new { x.user_id, x.search_query, x.created_at });
                    table.ForeignKey(
                        name: "FK_search_history_users_user_id1",
                        column: x => x.user_id1,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_ratings",
                schema: "framework",
                columns: table => new
                {
                    ratings_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    MovieId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    rating = table.Column<decimal>(type: "numeric", nullable: false),
                    review = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_ratings", x => new { x.ratings_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_user_ratings_users_user_id1",
                        column: x => x.user_id1,
                        principalSchema: "framework",
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "framework",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "framework",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "framework",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "framework",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "framework",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_bookmarks_user_id1",
                schema: "framework",
                table: "bookmarks",
                column: "user_id1");

            migrationBuilder.CreateIndex(
                name: "IX_movie_cast_person_id",
                schema: "movie_data",
                table: "movie_cast",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_crew_person_id",
                schema: "movie_data",
                table: "movie_crew",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_genre_id",
                schema: "movie_data",
                table: "movie_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_notes_user_id1",
                schema: "framework",
                table: "notes",
                column: "user_id1");

            migrationBuilder.CreateIndex(
                name: "IX_person_professions_profession_id",
                schema: "movie_data",
                table: "person_professions",
                column: "profession_id");

            migrationBuilder.CreateIndex(
                name: "IX_search_history_user_id1",
                schema: "framework",
                table: "search_history",
                column: "user_id1");

            migrationBuilder.CreateIndex(
                name: "IX_user_ratings_user_id1",
                schema: "framework",
                table: "user_ratings",
                column: "user_id1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "framework",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "framework",
                table: "users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "bookmarks",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "movie_cast",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "movie_crew",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "movie_genres",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "movie_sources",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "name_ratings",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "notes",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "person_professions",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "ratings",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "search_history",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "user_ratings",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "genres",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "people",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "professions",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "movies",
                schema: "movie_data");

            migrationBuilder.DropTable(
                name: "users",
                schema: "framework");
        }
    }
}
