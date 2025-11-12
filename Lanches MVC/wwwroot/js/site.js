$(document).ready(function () {

    $("[data-mask='phone']").inputmask({
        mask: ["(99) 9999-9999", "(99) 99999-9999"],
        keepStatic: true
    });

    $("[data-mask='cep']").inputmask("99999-999");
});
