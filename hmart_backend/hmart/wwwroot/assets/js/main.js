// Loading start
$(window).on("load", function () {
  $("#loading").hide();
});
// Loading end

$(document).ready(function () {
  //#region Sticky fixed
  if ($(window).scrollTop() >= 250) {
    if ($(window).width() < 993) {
      $("#sticky-header-bottom").addClass("fixed animate__fadeInDown");
    } else if ($(window).width() >= 993) {
      $("#header-nav-menu").addClass("fixed animate__fadeInDown");
    }
  } else {
    $("#sticky-header-bottom").removeClass("fixed animate__fadeInDown");
    $("#header-nav-menu").removeClass("fixed animate__fadeInDown");
  }

  $(window).scroll(function () {
    if ($(window).scrollTop() >= 250) {
      if ($(window).width() < 993) {
        $("#sticky-header-bottom").addClass("fixed animate__fadeInDown");
      } else if ($(window).width() >= 993) {
        $("#header-nav-menu").addClass("fixed animate__fadeInDown");
      }
    } else {
      $("#sticky-header-bottom").removeClass("fixed animate__fadeInDown");
      $("#header-nav-menu").removeClass("fixed animate__fadeInDown");
    }
  });
  //#endregion

  //#region Canvas

  //#region Wishlist canvas
  $(document).on("click", "#wishlist-button", function (e) {
    e.preventDefault();
    let scrollSize = window.innerWidth - document.documentElement.clientWidth;
    $("body").css("margin-right", scrollSize.toString() + "px");
    $("body").addClass("canvas-opening");
    $("#shadow-layout").removeClass("d-none");
    setTimeout(() => {
      $("#shadow-layout").addClass("show");
      $("#wishlist .list-wrapper").addClass("canvas-opening");
    }, 300);
  });

  $(document).on(
    "click",
    "#wishlist .canvas-close, #shadow-layout",
    function (e) {
      e.preventDefault();
      $("#wishlist .list-wrapper").removeClass("canvas-opening");
      $("#shadow-layout").removeClass("show");
      setTimeout(() => {
        $("#shadow-layout").addClass("d-none");
        $("body").css("margin-right", "0");
        $("body").removeClass("canvas-opening");
      }, 600);
    }
  );
  //#endregion

  //#region CartList canvas
  $(document).on("click", "#cartlist-button", function (e) {
    e.preventDefault();
    let scrollSize = window.innerWidth - document.documentElement.clientWidth;
    $("body").css("margin-right", scrollSize.toString() + "px");
    $("body").addClass("canvas-opening");
    $("#shadow-layout").removeClass("d-none");
    setTimeout(() => {
      $("#shadow-layout").addClass("show");
      $("#cart .list-wrapper").addClass("canvas-opening");
    }, 300);
  });

  $(document).on("click", "#cart .canvas-close, #shadow-layout", function (e) {
    e.preventDefault();
    $("#cart .list-wrapper").removeClass("canvas-opening");
    $("#shadow-layout").removeClass("show");
    setTimeout(() => {
      $("#shadow-layout").addClass("d-none");
      $("body").css("margin-right", "0");
      $("body").removeClass("canvas-opening");
    }, 600);
  });
  //#endregion

  //#region Mobile-menu
  $(document).on("click", "#menu-button", function (e) {
    e.preventDefault();
    let scrollSize = window.innerWidth - document.documentElement.clientWidth;
    $("body").addClass("canvas-opening");
    $("body").css("margin-right", scrollSize.toString() + "px");
    $("#shadow-layout").removeClass("d-none");
    setTimeout(() => {
      $("#shadow-layout").addClass("show");
      $("#mobile-menu .menu").addClass("canvas-opening");
      $("#mobile-menu .canvas-close").addClass("move");
    }, 300);
  });

  $(document).on(
    "click",
    "#mobile-menu .canvas-close, #shadow-layout",
    function (e) {
      e.preventDefault();
      $("#mobile-menu .canvas-close").removeClass("move");
      $("#mobile-menu .menu").removeClass("canvas-opening");
      $("#shadow-layout").removeClass("show");
      setTimeout(() => {
        $("#shadow-layout").addClass("d-none");
        $("body").css("margin-right", "0");
        $("body").removeClass("canvas-opening");
      }, 600);
    }
  );
  //#endregion

  //#endregion

  // Main Slider start
  var mainSlider = new Swiper(".main-slider", {
    direction: "vertical",
    speed: 2000,
    autoplay: {
      delay: 7000,
      disableOnInteraction: !1,
    },
    loop: !0,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    simulateTouch: !1,
  });
  // Main Slider end

  //#region Scroll Up Button
  if ($(window).scrollTop() >= 350) {
    $("#scrollUp").removeClass("d-none");
    setTimeout(() => {
      $("#scrollUp").addClass("show");
    }, 100);
  } else {
    $("#scrollUp").removeClass("show");
    setTimeout(() => {
      $("#scrollUp").addClass("d-none");
    }, 100);
  }

  $(window).scroll(function () {
    if ($(window).scrollTop() >= 350) {
      $("#scrollUp").removeClass("d-none");
      setTimeout(() => {
        $("#scrollUp").addClass("show");
      }, 100);
    } else {
      $("#scrollUp").removeClass("show");
      setTimeout(() => {
        $("#scrollUp").addClass("d-none");
      }, 100);
    }
  });

  $(document).on("click", "#scrollUp", function (e) {
    e.preventDefault();
    $("html, body").animate({ scrollTop: 0 }, 1000);
  });
  //#endregion

  // Bg_image Setting start
  if ($("[data-bg-image]")) {
    $("[data-bg-image]").each(function (e) {
      $(this).css(
        "background-image",
        "url(" + $(this).attr("data-bg-image") + ")"
      );
    });
  }
  // Bg_image Setting end

  // Countdown Time start
    $("[data-countdown]").each(function () {
    $(this).countdown($(this).attr("data-countdown"), function (event) {
      $(this).html(
        event.strftime(
          '<span class="cdown day"><span class="cdown-1">%d</span><p>Days</p></span> <span class="cdown hour"><span class="cdown-1">%-H</span><p>Hrs</p></span> <span class="cdown minutes"><span class="cdown-1">%M</span> <p>Min</p></span> <span class="cdown second"><span class="cdown-1"> %S</span> <p>Sec</p></span>'
        )
      );
    });
  });
  // Countdown Time end

  // Testimonial Slider start
  var swiper = new Swiper(".testimonial-slider", {
    slidesPerView: 2,
    spaceBetween: 30,
    speed: 1500,
    loop: !0,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    breakpoints: {
      0: { slidesPerView: 1 },
      478: { slidesPerView: 1 },
      576: { slidesPerView: 1 },
      768: { slidesPerView: 2 },
      992: { slidesPerView: 2 },
      1200: { slidesPerView: 2 },
    },
  });
  // Testimonial Slider end

  // Partners start
  var swiper = new Swiper(".partners-slider", {
    slidesPerView: 4,
    speed: 1500,
    loop: !0,
    autoplay: { delay: 2e3, disableOnInteraction: !1 },
    breakpoints: {
      0: { slidesPerView: 1 },
      480: { slidesPerView: 2 },
      768: { slidesPerView: 2 },
      992: { slidesPerView: 3 },
      1200: { slidesPerView: 4 },
    },
  });
  // Partners end

  //#region Modals Close Btn

  $(document).on("click", ".modal", function (e) {
    let target = $(e.target);
    if (target.hasClass("modal") || target.hasClass("modal-wrapper")) {
      $("body").css("margin-right", "0");
      $("#header-nav-menu .nav-menu-list").css("margin-right", "0");
      $("body").removeClass("canvas-opening");
      $("#shadow-layout").removeClass("show");
      $(".modal").removeClass("show");
      setTimeout(() => {
        $("#shadow-layout").addClass("d-none");
        $(".modal").removeClass("fade");
      }, 600);
    }
  });

  $(document).on("click", ".btn-close", function (e) {
    e.preventDefault();
    $("body").css("margin-right", "0");
    $("#header-nav-menu .nav-menu-list").css("margin-right", "0");
    $("body").removeClass("canvas-opening");
    $("#shadow-layout").removeClass("show");
    $(".modal").removeClass("show");
    setTimeout(() => {
      $("#shadow-layout").addClass("d-none");
      $(".modal").removeClass("fade");
    }, 600);
  });

  //$(document).on("click", ".wishlist", function (e) {
  //  e.preventDefault();
  //  let scrollSize = window.innerWidth - document.documentElement.clientWidth;
  //  $("#shadow-layout").removeClass("d-none");
  //  $("#wishlist-modal").addClass("fade");
  //  setTimeout(() => {
  //    $("#shadow-layout").addClass("show");
  //    $("#wishlist-modal").addClass("show");
  //    $("body").addClass("canvas-opening");
  //    $("body").css("margin-right", scrollSize.toString() + "px");
  //    $("#header-nav-menu .nav-menu-list").css(
  //      "margin-right",
  //      scrollSize.toString() + "px"
  //    );
  //  }, 300);
  //});

  //#endregion

  //#region Show QuickView (Detail) modal
    $(document).on("click", ".quickview", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");
        let id = url.substring(url.lastIndexOf("/")+1);

        async function ResponseHtml() {
            const response = await fetch(url)
                .then(resp=> {
                    if (!resp.ok) {
                        let url = window.location.href + 'details?id=' + id + '2&&code=404&&name=Product';
                        window.location.href = url;
                    }
                    return resp.text();
                })
                .then(data => {
                    $("#quickview-modal .modal-content").html(data);
                }
                );
        }

        ResponseHtml();

        setTimeout(() => {
            var modalSmallSlider = new Swiper(".modal-view-small-slider", {
                spaceBetween: 10,
                slidesPerView: 3,
                freeMode: !0,
                watchSlidesVisibility: !0,
                watchSlidesProgress: !0,
                navigation: {
                    nextEl: ".swiper-button-next",
                    prevEl: ".swiper-button-prev",
                },
            });

            var modalBigSlider = new Swiper(".modal-view-big-slider", {
                spaceBetween: 0,
                loop: !0,
                slidesPerView: 1,
                centerMood: !0,
                thumbs: { swiper: modalSmallSlider },
            });

            setTimeout(() => {
                let scrollSize = window.innerWidth - document.documentElement.clientWidth;
                $("#shadow-layout").removeClass("d-none");
                $("#quickview-modal").addClass("fade");
                setTimeout(() => {
                    $("#shadow-layout").addClass("show");
                    $("#quickview-modal").addClass("show");
                    $("body").addClass("canvas-opening");
                    $("body").css("margin-right", scrollSize.toString() + "px");
                    $("#header-nav-menu .nav-menu-list").css(
                        "margin-right",
                        scrollSize.toString() + "px"
                    );
                }, 300);
            }, 10);

        }, 300);
       

       
    });

    //#endregion 

  //#region AddToBasket
    $(document).on("click", ".add-to-cart", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");
        let imgSrc = $(this).attr("data-img");
        let name = $(this).attr("data-name");
        let id = url.substring(url.lastIndexOf("/") + 1);
        if ($(this).hasClass("detail_cart")) {
            let count = $("#count_for_basket").val();
            url = url + "?count=" + count;
            console.log(url);
        }

        async function ResponseHtml() {
            const response = await fetch(url)
                .then(resp => {
                    if (!resp.ok) {
                        let url = window.location.href + 'details?id=' + id + '&&code=404&&name=Product';
                        window.location.href = url;
                    } else if (resp.status == 204) {
                        return 204;
                    } else if (resp.status == 201) {
                        return 201;
                    }
                    return resp.text();
                })
                .then(data => {
                    if (data == 204) {
                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;

                        $("#out-of-product #modal_message_text").text("Out of stock!");

                        $("#shadow-layout").removeClass("d-none");
                        $("#out-of-product").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#out-of-product").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);

                    } else if (data == 201) {
                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;

                        $("#out-of-product #modal_message_text").text("There are not so many products left in stock!");

                        $("#shadow-layout").removeClass("d-none");
                        $("#out-of-product").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#out-of-product").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);
                    }
                    else {

                        $(".basket-partial").html(data);

                        $("#cart-modal .modal-product img").attr("src", imgSrc);
                        $("#cart-modal .modal-product a").text(name);

                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;
                        $("#shadow-layout").removeClass("d-none");
                        $("#cart-modal").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#cart-modal").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);
                    }
                }
                );
        }

        ResponseHtml();

    });

    //#endregion 

  //#region RemoveFromBasket
    $(document).on("click", ".remove-from-basket", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");
        let id = url.substring(url.lastIndexOf("/") + 1);

        async function ResponseHtml() {
            const response = await fetch(url)
                .then(resp => {
                    if (!resp.ok) {
                        let url = window.location.href + 'details?id=' + id + '&&code=404&&name=Product';
                        window.location.href = url;
                    }
                    return resp.text();
                })
                .then(data => {
                        $(".basket-partial").html(data);
                }
                );
        }

        ResponseHtml();

    });

    //#endregion

  //#region AddToWishList
    $(document).on("click", ".wishlist", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        let imgSrc = $(this).attr("data-img");
        let name = $(this).attr("data-name");
        let id = url.substring(url.lastIndexOf("/") + 1);

        async function ResponseHtml() {
            const response = await fetch(url)
                .then(resp => {
                    if (!resp.ok) {
                        console.log("OK-deyil");
                        let url = window.location.href + 'details?id=' + id + '&&code=404&&name=Product';
                        window.location.href = url;
                    } else if (resp.status == 204) {
                        return 204;
                    } else if (resp.status == 201) {
                        return 201;
                    }
                    return resp.text();
                })
                .then(data => {

                    if (data == 204) {
                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;

                        $("#out-of-product #modal_message_text").text("Out of stock!");

                        $("#shadow-layout").removeClass("d-none");
                        $("#out-of-product").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#out-of-product").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);
                    } else if (data == 201) {

                        $("#wishlist-modal .modal-product img").attr("src", imgSrc);
                        $("#wishlist-modal .modal-product a").text(name);
                        $("#modal_message_text").text("This product has been added!");
                        $("#wishlist-modal .modal-message .check").css({
                            "background-color": "#f2c935",
                            "border-color": "#f2c935"
                        });
                        $("#modal_message_text").css("color","#d8a42f");

                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;
                        $("#shadow-layout").removeClass("d-none");
                        $("#wishlist-modal").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#wishlist-modal").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);
                    } else {

                        $(".wishList-partial").html(data);

                        $("#wishlist-modal .modal-product img").attr("src", imgSrc);
                        $("#wishlist-modal .modal-product a").text(name);
                        $("#modal_message_text").text("Added to Wishlist successfully!");
                        $("#wishlist-modal .modal-message .check").css({
                            "background-color": "#30e568",
                            "border-color": "#30e568"
                        });
                        $("#modal_message_text").css("color", "#29ac52");

                        e.preventDefault();
                        let scrollSize = window.innerWidth - document.documentElement.clientWidth;
                        $("#shadow-layout").removeClass("d-none");
                        $("#wishlist-modal").addClass("fade");
                        setTimeout(() => {
                            $("#shadow-layout").addClass("show");
                            $("#wishlist-modal").addClass("show");
                            $("body").addClass("canvas-opening");
                            $("body").css("margin-right", scrollSize.toString() + "px");
                            $("#header-nav-menu .nav-menu-list").css(
                                "margin-right",
                                scrollSize.toString() + "px"
                            );
                        }, 300);
                    }
                }
                );
        }

        ResponseHtml();

    });

    //#endregion 

  //#region RemoveFromWishList
    $(document).on("click", ".remove-from-wishList", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");
        let id = "";
        if (url.indexOf("?") > -1) {
            id = url.substring(url.lastIndexOf("/") + 1, url.indexOf("?"))
        } else {
            id = url.substring(url.lastIndexOf("/") + 1);
        }

        async function ResponseHtml() {
            const response = await fetch(url)
                .then(resp => {
                    if (!resp.ok) {
                        let url = window.location.href + 'details?id=' + id + '&&code=404&&name=Product';
                        window.location.href = url;
                    } 
                    return resp.text();
                })
                .then(data => {
                        $(".wishList-partial").html(data);
                }
                );
        }

        ResponseHtml();

    });

    //#endregion

  // Featured Main Product responsived start

  if ($(".single-feature-content")[0]) {
    if ($(".single-feature-content")[0].scrollHeight < 750) {
      $(".single-feature-content").addClass("resposive-height");
    } else {
      $(".single-feature-content").removeClass("resposive-height");
    }
  }

  // Featured Main Product responsived end

  // Modals end

  //Dropdown Sort start
  $(document).on("click", ".header-action-btn", function (e) {
    e.preventDefault();
    console.log($(this));
    $(".dropdown-menu").toggleClass("show");
  });
  $(document).on("click", function (e) {
    let target = $(e.target);
    if (
      !target.hasClass("header-action-btn") &&
      !target.hasClass("icon-angle-down")
    ) {
      $(".dropdown-menu").removeClass("show");
    }
    // console.log("doc clik");
  });
  //Dropdown Sort end

  // Shop product list style change start

  $(document).on("click", ".btn-tab-style", function (e) {
    e.preventDefault();
    $(".btn-tab-style").removeClass("active");
    $(this).addClass("active");
    if ($(this).hasClass("shop-grid")) {
      $("#shop-list").removeClass("active");
      $("#shop-list").removeClass("show");
      $("#shop-grid").addClass("active");
      $("#shop-grid").addClass("show");
    } else {
      $("#shop-grid").removeClass("active");
      $("#shop-grid").removeClass("show");
      $("#shop-list").addClass("active");
      $("#shop-list").addClass("show");
    }
  });

  // Shop product list style change end

  // Single Product Sliders start
  var smallImages = new Swiper(".product-images-slider-thumbs", {
    spaceBetween: 18,
    slidesPerView: 3,
    freeMode: !0,
    watchSlidesVisibility: !0,
    watchSlidesProgress: !0,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });

  var bigImages = new Swiper(".product-images-slider", {
    spaceBetween: 0,
    slidesPerView: 1,
    effect: "fade",
    fadeEffect: { crossFade: !0 },
    thumbs: { swiper: smallImages },
  });
  // Single Product Sliders end

  // Related products start
  var swiper = new Swiper(".related-products-slider", {
    slidesPerView: 4,
    spaceBetween: 30,
    speed: 1500,
    loop: !0,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    breakpoints: {
      0: { slidesPerView: 1 },
      480: { slidesPerView: 2 },
      768: { slidesPerView: 2 },
      992: { slidesPerView: 3 },
      1200: { slidesPerView: 4 },
    },
  });
  // Related products end

  // Count Add To Cart Product start
  $(document).on("click", ".operator", function (e) {
    e.preventDefault();
    let o = $(this);
    let i = o.parent().find("input").val();
    if ("+" === o.text()) var n = parseFloat(i) + 1;
    else if (i > 1) n = parseFloat(i) - 1;
    else n = 1;
    o.parent().find("input").val(n);
  });
  // Count Add To Cart Product start

  // Product Info Tabs start
  $(document).on("click", '[data-type="tab"]', function (e) {
    e.preventDefault();
    $('[data-type="tab"]').removeClass("active");
    $(this).addClass("active");
    let dataTarget = $(this).attr("data-target");
    $(".tab-content .tab-pane").removeClass("active");
    $(dataTarget).addClass("active");
  });
  // Product Info Tabs end

  // Account tags start

  $(document).on("click", "[data-bs-toggle='tab']", function (e) {
    e.preventDefault();
    $("[data-bs-toggle='tab']").removeClass("active");
    $(".tab-pane").removeClass("show");
    $(".tab-pane").removeClass("active");

    $(this).addClass("active");
    $($(this).attr("href")).addClass("active");
    $($(this).attr("href")).addClass("show");
  });

  // Account tags end

  // FileInput Choosing Names Show start

  $(document).on("change", "#imageFile", function (e) {
    if ($(this).val()) {
      let fileName = $(this)[0].files.item(0).name;
      if (fileName.length > 25) {
        fileName = fileName.substring(0, 22) + "...";
      }

      $("#imageFileName").text(fileName);
    }
  });

  // FileInput Choosing Names Show end

  // Toast start 
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if ($("#toast-message").length) {
        if ($("#toast-message").attr("data-succeded") == "true") {
            toastr["success"]($("#toast-message").attr("data-text"))
        }
        else {
            toastr["error"]($("#toast-message").attr("data-text"))
        }
    }
  // Toast end
});
