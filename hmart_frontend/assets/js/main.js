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

  //#region Wishlist
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

  //#region CartList
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

  // Modals start

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

  $(document).on("click", ".add-to-cart", function (e) {
    e.preventDefault();
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
  });

  $(document).on("click", ".wishlist", function (e) {
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
  });

  $(document).on("click", ".quickview", function (e) {
    e.preventDefault();
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
  });
  // Modal end

  // Modal slider start

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
  // Modal slider end

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

  // Shop range start
  $("#slider-range").slider({
    range: true,
    min: 0,
    max: 2000,
    values: [400, 1600],
    slide: function (event, ui) {
      $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
    },
  });
  $("#amount").val(
    "$" +
      $("#slider-range").slider("values", 0) +
      " - $" +
      $("#slider-range").slider("values", 1)
  );
  // Shop range end

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

  // Count Product start
  $(document).on("click", ".operator", function (e) {
    e.preventDefault();
    let o = $(this);
    let i = o.parent().find("input").val();
    if ("+" === o.text()) var n = parseFloat(i) + 1;
    else if (i > 1) n = parseFloat(i) - 1;
    else n = 1;
    o.parent().find("input").val(n);
  });
  // Count Product start

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

  // Selection Checkbox start
  $(document).on("change", ".checkbox input", function (e) {
    let inputChanged = $(this)[0];
    $(".checkbox input").each(function (e) {
      if ($(this)[0] == inputChanged) {
        if ($(this)[0].checked == true) {
          $(this)[0].checked = true;
        } else {
          $(this)[0].checked = false;
        }
      } else {
        $(this)[0].checked = false;
      }
    });
  });
  // Selection Checkbox end

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
});
