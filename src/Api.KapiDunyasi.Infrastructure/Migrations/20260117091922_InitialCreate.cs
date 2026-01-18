using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.KapiDunyasi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog_posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    excerpt_tr = table.Column<string>(type: "text", nullable: true),
                    tags_tr = table.Column<string[]>(type: "text[]", nullable: false),
                    published_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contact_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "faqs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    q_tr = table.Column<string>(type: "text", nullable: false),
                    a_tr = table.Column<string>(type: "text", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faqs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_no = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    form_name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    invoice_type = table.Column<string>(type: "text", nullable: false),
                    payment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "policy_contents",
                columns: table => new
                {
                    key = table.Column<string>(type: "text", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    body_tr = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policy_contents", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    series_tr = table.Column<string>(type: "text", nullable: true),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_tr = table.Column<string[]>(type: "text[]", nullable: false),
                    badges_tr = table.Column<string[]>(type: "text[]", nullable: false),
                    price_type = table.Column<string>(type: "text", nullable: false),
                    price_value = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    price_min = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    price_max = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    stock_status = table.Column<string>(type: "text", nullable: false),
                    stock_count = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    images = table.Column<string[]>(type: "text[]", nullable: false),
                    specs_material_tr = table.Column<string>(type: "text", nullable: true),
                    specs_surface_tr = table.Column<string>(type: "text", nullable: true),
                    specs_thickness_options = table.Column<string[]>(type: "text[]", nullable: false),
                    specs_frame_options = table.Column<string[]>(type: "text[]", nullable: false),
                    specs_sizes = table.Column<string[]>(type: "text[]", nullable: false),
                    specs_color_options_tr = table.Column<string[]>(type: "text[]", nullable: false),
                    specs_opening_directions_tr = table.Column<string[]>(type: "text[]", nullable: false),
                    fire_resistant = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    warranty_tr = table.Column<string>(type: "text", nullable: true),
                    shipping_tr = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "references",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_name_tr = table.Column<string>(type: "text", nullable: false),
                    city_tr = table.Column<string>(type: "text", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: true),
                    type_tr = table.Column<string>(type: "text", nullable: true),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_references", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "showrooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    address_tr = table.Column<string>(type: "text", nullable: true),
                    map_embed_url = table.Column<string>(type: "text", nullable: true),
                    working_hours_tr = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showrooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_content_sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    heading_tr = table.Column<string>(type: "text", nullable: false),
                    body_tr = table.Column<string>(type: "text", nullable: false),
                    blog_post_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_content_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_blog_content_sections_blog_posts_blog_post_id",
                        column: x => x.blog_post_id,
                        principalTable: "blog_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    qty = table.Column<int>(type: "integer", nullable: false),
                    variant_size = table.Column<string>(type: "text", nullable: true),
                    variant_frame = table.Column<string>(type: "text", nullable: true),
                    variant_direction = table.Column<string>(type: "text", nullable: true),
                    variant_color = table.Column<string>(type: "text", nullable: true),
                    variant_thickness = table.Column<string>(type: "text", nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_content_sections_blog_post_id",
                table: "blog_content_sections",
                column: "blog_post_id");

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_published_at",
                table: "blog_posts",
                column: "published_at");

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_slug",
                table: "blog_posts",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_tags_tr",
                table: "blog_posts",
                column: "tags_tr")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_slug",
                table: "categories",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_messages_email",
                table: "contact_messages",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_faqs_sort_order",
                table: "faqs",
                column: "sort_order");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_created_at",
                table: "orders",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_orders_order_no",
                table: "orders",
                column: "order_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_status",
                table: "orders",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_policy_contents_key",
                table: "policy_contents",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_badges_tr",
                table: "products",
                column: "badges_tr")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_fire_resistant",
                table: "products",
                column: "fire_resistant");

            migrationBuilder.CreateIndex(
                name: "IX_products_price_min_price_max",
                table: "products",
                columns: new[] { "price_min", "price_max" });

            migrationBuilder.CreateIndex(
                name: "IX_products_series_tr",
                table: "products",
                column: "series_tr");

            migrationBuilder.CreateIndex(
                name: "IX_products_slug",
                table: "products",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_stock_status",
                table: "products",
                column: "stock_status");

            migrationBuilder.CreateIndex(
                name: "IX_products_tags_tr",
                table: "products",
                column: "tags_tr")
                .Annotation("Npgsql:IndexMethod", "gin");

            migrationBuilder.CreateIndex(
                name: "IX_references_city_tr",
                table: "references",
                column: "city_tr");

            migrationBuilder.CreateIndex(
                name: "IX_references_year",
                table: "references",
                column: "year");

            migrationBuilder.CreateIndex(
                name: "IX_showrooms_slug",
                table: "showrooms",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_content_sections");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "contact_messages");

            migrationBuilder.DropTable(
                name: "faqs");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "policy_contents");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "references");

            migrationBuilder.DropTable(
                name: "showrooms");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "blog_posts");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
