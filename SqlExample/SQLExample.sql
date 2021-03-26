/*
 Одному продукту может соответствовать много категорий, в одной категории может быть много продуктов. 
 Напишите SQL запрос для выбора всех пар «Имя продукта – Имя категории». 
 Если у продукта нет категорий, то его имя все равно должно выводиться.
*/

SELECT
    products.name as product_name, categories.name as category_name
FROM
     products
RIGHT JOIN relation_categories_to_products rctp ON products.id = rctp.id_product
LEFT JOIN categories ON rctp.id_category = categories.id