Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        decrementLink: "",
        removeFromCartLink: ""
    },

    init: function(properties) {
        $.extend(Cart._properties, properties);
        Cart.initEvents();
    },

    initEvents: function() {
        $(".add-to-cart").click(Cart.addToCart);
        //$(".cart_quantity_up").click(Cart.incrementItem);
        //$(".cart_quantity_down").click(Cart.decrementItem);
        //$(".cart_quantity_delete").click(Cart.removeItem);
    },

    addToCart: function(e) {
        e.preventDefault();

        const button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function(response) {
                console.log(response.message);

                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function() { console.log("addToCart fail"); });
    },

    showToolTip: function(button) {
        button.tooltip({ title: "Добавлено в корзину" }).tooltip("show");
        setTimeout(function() {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function() {
        $.get(Cart._properties.getCartViewLink)
            .done(function(cartHtml) {
                $("#cart-container").html(cartHtml);
            })
            .fail(function() { console.log("refreshCartView fail"); });
    }
}