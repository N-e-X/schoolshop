// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var d = document,
    itemBox = d.querySelectorAll('.item_box'), // блок каждого товара
    cartCont = d.getElementById('cart_content'); // блок вывода данных корзины
openCart();

// Функция кроссбраузерной установка обработчика событий
function addEvent(elem, type, handler) {
    if (elem.addEventListener) {
        elem.addEventListener(type, handler, false);
    } else {
        elem.attachEvent('on' + type, function () { handler.call(elem); });
    }
    return false;
}
// Получаем данные из LocalStorage
function getCartData() {
    return JSON.parse(localStorage.getItem('cart'));
}
// Записываем данные в LocalStorage
function setCartData(o) {
    localStorage.setItem('cart', JSON.stringify(o));
    return false;
}
// Добавляем товар в корзину
function addToCart(e) {
    this.disabled = true; // блокируем кнопку на время операции с корзиной
    var cartData = getCartData() || {}, // получаем данные корзины или создаём новый объект, если данных еще нет
        parentBox = this.parentNode, // родительский элемент кнопки "Добавить в корзину"
        itemId = this.getAttribute('data-id'), // ID товара
        itemName = parentBox.querySelector('td span.item_name').innerHTML, // название товара
        itemPrice =
            Number.parseFloat(parentBox.querySelector('td span.item_price').innerHTML.replace(',', '.')), // стоимость товара

        itemCount = Number.parseInt(parentBox.querySelector('td label.item_count input').value), // кол-во товара
        itemCost = 0;
    if (itemCount <= 0)
        return false;
    if (cartData.hasOwnProperty(itemId)) { // если такой товар уже в корзине, то добавляем +1 к его количеству
        cartData[itemId][2] += itemCount;
    }
    else { // если товара в корзине еще нет, то добавляем в объект
        cartData[itemId] = [itemName, itemPrice, itemCount, itemCost];
    }
    cartData[itemId][3] = cartData[itemId][2] * itemPrice;
    if (!setCartData(cartData)) { // Обновляем данные в LocalStorage
        this.disabled = false; // разблокируем кнопку после обновления LS
    }

    var options = {
        title: itemName + '(' + itemCount + ')' + " added to the cart"
    }
    // Инициализация tooltip
    $('[data-toggle="tooltip"]').tooltip(options);

    $('[data-toggle="tooltip"]').tooltip('show');
    setTimeout(function () { $('[data-toggle="tooltip"]').tooltip('destroy'); }, 2000);

    return false;
}
// Устанавливаем обработчик события на каждую кнопку "Добавить в корзину"
for (var i = 0; i < itemBox.length; i++) {
    addEvent(itemBox[i].querySelector('.add_item'), 'click', addToCart);
}
// Открываем корзину со списком добавленных товаров
function openCart() {
    var cartData = getCartData(), // вытаскиваем все данные корзины
        totalItems = '';
    // если что-то в корзине уже есть, начинаем формировать данные для вывода
    if (cartData !== null) {
        totalItems = '<table class="table"><tr><th>Product name</th><th>Price</th><th>Quantity</th><th>Cost</th></tr>';
        for (var items in cartData) {
            totalItems += '<tr>';
            for (var i = 0; i < cartData[items].length; i++) {
                totalItems += '<td>' + cartData[items][i] + '</td>';
            }
            totalItems += '</tr>';
        }
        totalItems += '</table>';
        cartCont.innerHTML = totalItems;
    } else {
        // если в корзине пусто, то сигнализируем об этом
        cartCont.innerHTML = 'The cart is empty!';
    }
    return false;
}

/* Очистить корзину */
addEvent(d.getElementById('clear_cart'), 'click', function (e) {
    localStorage.removeItem('cart');
    cartCont.innerHTML = 'The cart has been cleaned.';
});

/* Выполнить заказ */

addEvent(d.getElementById('order'), 'click', function (e) {
    if (getCartData() !== null)
    {
        sendCartData();
    }
    else
    {
        alert('Cart is empty!');
    }
});


// Отправить данные корзины на сервер
function sendCartData() {

    var xhr = new XMLHttpRequest();

    var body = getCartDataJson(); // но нужно сделать нормальный JSON 

    xhr.open('POST', '/orders/add', true);
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');

    xhr.onreadystatechange = function () {
        if (this.readyState !== 4) return;

        // по окончании запроса доступны:
        // status, statusText
        // responseText, responseXML (при content-type: text/xml)

        if (this.status != 200) {
            // обработать ошибку
            alert('Error: ' + (this.status ? this.statusText : 'request failed'));
            return;
        }

        alert('Order has been done!');
    };

    // Отсылаем объект в формате JSON и с Content-Type application/json
    // Сервер должен уметь такой Content-Type принимать и раскодировать
    xhr.send(body);

    localStorage.removeItem('cart');
    openCart();
}

function getCartDataJson() {

    var cartData = getCartData();
    var cartDataJson = [];

    for (var id in cartData) {
        if (cartData.hasOwnProperty(id)) {
            cartDataJson.push(
                {
                    "ProductId": Number.parseInt(id),
                    "ProductName": cartData[id][0],
                    "ProductPrice": Number.parseFloat(cartData[id][1]),
                    "ProductCount": Number.parseInt(cartData[id][2])
                });
        }
    }
    return JSON.stringify(cartDataJson);
}