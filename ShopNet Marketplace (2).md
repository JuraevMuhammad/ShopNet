
# 🛒 ShopNet Marketplace — RESTful Web API бо ASP.NET Core

## 📘 Сенария

Ширкати "ShopNet" ният дорад, ки як RESTful Web API-ро барои идоракунии интернет-мағоза созад. API бояд маълумоти мизоҷон, фурӯшандаҳо, категорияҳо, маҳсулот ва фармоишҳоро идора кунад ва тавассути Swagger UI дастрас ва санҷида шавад.

Дар ин система:

- Мизоҷон (Customers): ҳар касе, ки мехоҳад маҳсулот бихарад, бояд сабт шавад (ном, насаб, email, телефон).
- Фурӯшандаҳо (Sellers): шахсоне, ки маҳсулотро ба мағоза мегузоранд, бо маълумоти шахсӣ ва номи мағоза.
- Категорияҳо (Categories): барои гурӯҳбандии маҳсулот (масалан, электроника, либос, китобҳо).
- Маҳсулот (Products): ҳар як маҳсулот дорои нарх, миқдор, фурӯшанда ва категория мебошад.
- Фармоишҳо (Orders): мизоҷ метавонад як ё якчанд маҳсулотро фармоиш диҳад.
- Тафсилоти фармоиш (Order Items): ҳар фармоиш метавонад якчанд маҳсулот бо миқдори муайян дошта бошад.

Вазифа — сохтани ASP.NET Core Web API бо ADO.NET (Npgsql) ва интегратсияи Swashbuckle (Swagger) барои ҳуҷҷатгузорӣ ва санҷиши API.

---

## 🧱 Схемаи базаи маълумот (PostgreSQL)

Схемаи база ба таври зерин боқӣ мемонад:

```sql
CREATE TABLE customers (
    id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone VARCHAR(20) UNIQUE
);

CREATE TABLE sellers (
    id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    shop_name VARCHAR(150) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    price NUMERIC(10,2) CHECK (price > 0),
    quantity INT CHECK (quantity >= 0),
    category_id INT REFERENCES categories(id) ON DELETE SET NULL,
    seller_id INT REFERENCES sellers(id) ON DELETE CASCADE
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES customers(id) ON DELETE CASCADE,
    order_date DATE NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE order_items (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES orders(id) ON DELETE CASCADE,
    product_id INT REFERENCES products(id) ON DELETE CASCADE,
    quantity INT CHECK (quantity > 0),
    price NUMERIC(10,2) CHECK (price > 0),
    UNIQUE(order_id, product_id)
);
```

---

## 🧩 Endpoint-ҳо (Controllers ва HTTP методҳо)

API бояд endpoint-ҳои зеринро дошта бошад (масалан: `/api/customers`, `/api/products`):

### 📌 Customers

| Method | Endpoint | Тавсиф |
|--------|----------|--------|
| GET | `/api/customers` | Рӯйхати ҳамаи мизоҷон |
| GET | `/api/customers/{id}` | Гирифтани як мизоҷ |
| POST | `/api/customers` | Иловаи мизоҷи нав |
| PUT | `/api/customers/{id}` | Тағйири маълумоти мизоҷ |
| DELETE | `/api/customers/{id}` | Ҳазфи мизоҷ |


### 📌 Sellers (Фурӯшандаҳо)

| Method | Endpoint | Тавсиф |
|--------|----------|--------|
| GET | `/api/sellers` | Рӯйхати ҳамаи фурӯшандаҳо |
| GET | `/api/sellers/{id}` | Гирифтани як фурӯшанда |
| POST | `/api/sellers` | Иловаи фурӯшандаи нав |
| PUT | `/api/sellers/{id}` | Тағйири маълумоти фурӯшанда |
| DELETE | `/api/sellers/{id}` | Ҳазфи фурӯшанда |

### 📌 Categories (Категорияҳо)

| Method | Endpoint | Тавсиф |
|--------|----------|--------|
| GET | `/api/categories` | Рӯйхати ҳамаи категорияҳо |
| GET | `/api/categories/{id}` | Гирифтани як категория |
| POST | `/api/categories` | Иловаи категорияи нав |
| PUT | `/api/categories/{id}` | Тағйири маълумоти категория |
| DELETE | `/api/categories/{id}` | Ҳазфи категория |

### 📌 Products (Маҳсулот)

| Method | Endpoint | Тавсиф |
|--------|----------|--------|
| GET | `/api/products` | Рӯйхати ҳамаи маҳсулот |
| GET | `/api/products/{id}` | Гирифтани як маҳсулот |
| POST | `/api/products` | Иловаи маҳсулоти нав |
| PUT | `/api/products/{id}` | Тағйири маълумоти маҳсулот |
| DELETE | `/api/products/{id}` | Ҳазфи маҳсулот |

### 📌 Orders (Фармоишҳо)

| Method | Endpoint | Тавсиф |
|--------|----------|--------|
| GET | `/api/orders/customer/{customerId}` | Фармоишҳои як мизоҷ |
| GET | `/api/orders/{orderId}` | Тафсилоти фармоиш |
| POST | `/api/orders` | Эҷоди фармоиши нав |
| POST | `/api/orders/{orderId}/products` | Иловаи маҳсулот ба фармоиш |
| DELETE | `/api/orders/{orderId}` | Ҳазфи фармоиш |


### 📌 Запросҳои иловагӣ (GET)

- `GET /api/products/search?name={q}` — Ҷустуҷӯи маҳсулот бо ном
- `GET /api/products?categoryId={id}` — Маҳсулот аз рӯи категория
- `GET /api/products?sellerId={id}` — Маҳсулот аз рӯи фурӯшанда
- `GET /api/products/lowstock` — Маҳсулоти кам (quantity < 5)
- `GET /api/products/topselling` — 5 маҳсулоти бештар фурӯхташуда

---

## 📝 Дастурҳо ва талаботҳо

- Забон ва технологияҳо: ASP.NET Core Web API (.NET 8 ё навтар) + ADO.NET + Npgsql (бидуни Entity Framework)
- NuGet пакетҳо: `Npgsql`, `Swashbuckle.AspNetCore`
- Пайвастшавӣ ба база: `ConnectionString`
- Дастрасӣ ба база: Ҳамаи дархостҳо бояд бо `NpgsqlConnection`, `NpgsqlCommand` ва параметрҳо (`@param`) иҷро шаванд.
- Амният: Ҳамаи параметрҳо бояд параметрӣ бошанд барои пешгирӣ аз SQL Injection.
- Swagger: Дар `Program.cs` Swagger-ро фаъол кунед то `/swagger` дастрас бошад;



## 🎯 Тести API

Пас аз оғоз кардани проект, ба суроғаи `https://localhost:{port}/swagger` ворид шавед ва ҳамаи endpoint-ҳоро санҷед.

---

## Дедлайн

То соати 05.01.2026 12:00 бояд тамоми супоришҳо пурра ҳал шуда, линкҳои GitHub-ро ирсол кунанд.
Аз пас аз соати 12:00 дигар ҳеҷ commit ё тағйирот ба проект қабул карда намешавад.

