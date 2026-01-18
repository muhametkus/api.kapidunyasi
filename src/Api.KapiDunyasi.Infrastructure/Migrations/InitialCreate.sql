CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE blog_posts (
    id uuid NOT NULL,
    title_tr text NOT NULL,
    slug text NOT NULL,
    excerpt_tr text,
    tags_tr text[] NOT NULL,
    published_at timestamp with time zone,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_blog_posts" PRIMARY KEY (id)
);

CREATE TABLE categories (
    id uuid NOT NULL,
    name_tr text NOT NULL,
    slug text NOT NULL,
    description_tr text,
    parent_id uuid,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_categories" PRIMARY KEY (id),
    CONSTRAINT "FK_categories_categories_parent_id" FOREIGN KEY (parent_id) REFERENCES categories (id) ON DELETE RESTRICT
);

CREATE TABLE contact_messages (
    id uuid NOT NULL,
    name text NOT NULL,
    email text NOT NULL,
    message text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_contact_messages" PRIMARY KEY (id)
);

CREATE TABLE faqs (
    id uuid NOT NULL,
    q_tr text NOT NULL,
    a_tr text NOT NULL,
    sort_order integer NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_faqs" PRIMARY KEY (id)
);

CREATE TABLE orders (
    id uuid NOT NULL,
    order_no text NOT NULL,
    status text NOT NULL,
    form_name text NOT NULL,
    phone text NOT NULL,
    email text NOT NULL,
    address text NOT NULL,
    invoice_type text NOT NULL,
    payment text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_orders" PRIMARY KEY (id)
);

CREATE TABLE policy_contents (
    key text NOT NULL,
    title_tr text NOT NULL,
    body_tr text NOT NULL,
    CONSTRAINT "PK_policy_contents" PRIMARY KEY (key)
);

CREATE TABLE products (
    id uuid NOT NULL,
    name_tr text NOT NULL,
    slug text NOT NULL,
    series_tr text,
    category_id uuid NOT NULL,
    tags_tr text[] NOT NULL,
    badges_tr text[] NOT NULL,
    price_type text NOT NULL,
    price_value numeric(12,2),
    price_min numeric(12,2),
    price_max numeric(12,2),
    stock_status text NOT NULL,
    stock_count integer NOT NULL,
    code text,
    images text[] NOT NULL,
    specs_material_tr text,
    specs_surface_tr text,
    specs_thickness_options text[] NOT NULL,
    specs_frame_options text[] NOT NULL,
    specs_sizes text[] NOT NULL,
    specs_color_options_tr text[] NOT NULL,
    specs_opening_directions_tr text[] NOT NULL,
    fire_resistant boolean NOT NULL DEFAULT FALSE,
    warranty_tr text,
    shipping_tr text,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_products" PRIMARY KEY (id)
);

CREATE TABLE "references" (
    id uuid NOT NULL,
    project_name_tr text NOT NULL,
    city_tr text,
    year integer,
    type_tr text,
    description_tr text,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_references" PRIMARY KEY (id)
);

CREATE TABLE showrooms (
    id uuid NOT NULL,
    name_tr text NOT NULL,
    slug text NOT NULL,
    phone text,
    address_tr text,
    map_embed_url text,
    working_hours_tr text,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_showrooms" PRIMARY KEY (id)
);

CREATE TABLE users (
    id uuid NOT NULL,
    name text NOT NULL,
    email text NOT NULL,
    password_hash text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_users" PRIMARY KEY (id)
);

CREATE TABLE blog_content_sections (
    id uuid NOT NULL,
    heading_tr text NOT NULL,
    body_tr text NOT NULL,
    blog_post_id uuid NOT NULL,
    CONSTRAINT "PK_blog_content_sections" PRIMARY KEY (id),
    CONSTRAINT "FK_blog_content_sections_blog_posts_blog_post_id" FOREIGN KEY (blog_post_id) REFERENCES blog_posts (id) ON DELETE CASCADE
);

CREATE TABLE order_items (
    id uuid NOT NULL,
    product_id uuid NOT NULL,
    name_tr text NOT NULL,
    price numeric(12,2) NOT NULL,
    image text,
    qty integer NOT NULL,
    variant_size text,
    variant_frame text,
    variant_direction text,
    variant_color text,
    variant_thickness text,
    order_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone,
    CONSTRAINT "PK_order_items" PRIMARY KEY (id),
    CONSTRAINT "FK_order_items_orders_order_id" FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
);

CREATE INDEX "IX_blog_content_sections_blog_post_id" ON blog_content_sections (blog_post_id);

CREATE INDEX "IX_blog_posts_published_at" ON blog_posts (published_at);

CREATE UNIQUE INDEX "IX_blog_posts_slug" ON blog_posts (slug);

CREATE INDEX "IX_blog_posts_tags_tr" ON blog_posts USING gin (tags_tr);

CREATE INDEX "IX_categories_parent_id" ON categories (parent_id);

CREATE UNIQUE INDEX "IX_categories_slug" ON categories (slug);

CREATE INDEX "IX_contact_messages_email" ON contact_messages (email);

CREATE INDEX "IX_faqs_sort_order" ON faqs (sort_order);

CREATE INDEX "IX_order_items_order_id" ON order_items (order_id);

CREATE INDEX "IX_order_items_product_id" ON order_items (product_id);

CREATE INDEX "IX_orders_created_at" ON orders (created_at);

CREATE UNIQUE INDEX "IX_orders_order_no" ON orders (order_no);

CREATE INDEX "IX_orders_status" ON orders (status);

CREATE UNIQUE INDEX "IX_policy_contents_key" ON policy_contents (key);

CREATE INDEX "IX_products_badges_tr" ON products USING gin (badges_tr);

CREATE INDEX "IX_products_category_id" ON products (category_id);

CREATE INDEX "IX_products_fire_resistant" ON products (fire_resistant);

CREATE INDEX "IX_products_price_min_price_max" ON products (price_min, price_max);

CREATE INDEX "IX_products_series_tr" ON products (series_tr);

CREATE UNIQUE INDEX "IX_products_slug" ON products (slug);

CREATE INDEX "IX_products_stock_status" ON products (stock_status);

CREATE INDEX "IX_products_tags_tr" ON products USING gin (tags_tr);

CREATE INDEX "IX_references_city_tr" ON "references" (city_tr);

CREATE INDEX "IX_references_year" ON "references" (year);

CREATE UNIQUE INDEX "IX_showrooms_slug" ON showrooms (slug);

CREATE UNIQUE INDEX "IX_users_email" ON users (email);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260117091922_InitialCreate', '10.0.1');

COMMIT;

