
//Tiny slider

// if(document.getElementsByClassName('client-review-slider').length > 0) {
//     var slider = tns({
//         container: '.client-review-slider',
//         items: 1,
//         controls: false,
//         mouseDrag: true,
//         loop: true,
//         rewind: true,
//         autoplay: true,
//         autoplayButtonOutput: false,
//         autoplayTimeout: 3000,
//         navPosition: "bottom",
//         speed: 400,
//         gutter: 16,
//         autoplayDirection:"backward"
//     });
// };

// if (document.getElementsByClassName('slider-range-four').length > 0) {
//     var slider = tns({
//         container: '.slider-range-four',
//         items: 4,
//         controls: false,
//         mouseDrag: true,
//         loop: true,
//         rewind: true,
//         autoplay: true,
//         autoplayButtonOutput: false,
//         autoplayTimeout: 3000,
//         navPosition: "bottom",
//         speed: 400,
//         gutter: 24,
//         responsive: {
//             992: {
//                 items: 4
//             },

//             767: {
//                 items: 2
//             },


//             320: {
//                 items: 1
//             },
//         },
//     });
// };


// if (document.getElementsByClassName('slider-range-three').length > 0) {
//     var slider = tns({
//         useLocalStorage: true,
//         container: '.slider-range-three',
//         items: 3,
//         controls: false,
//         mouseDrag: true,
//         loop: true,
//         rewind: true,
//         autoplay: true,
//         autoplayButtonOutput: false,
//         autoplayTimeout: 3000,
//         navPosition: "bottom",
//         speed: 400,
//         gutter: 24,
//         responsive: {
//             992: {
//                 items: 3
//             },

//             767: {
//                 items: 2
//             },

//             320: {
//                 items: 1
//             },
//         },
//     });
// };







$(document).ready(function () {

    $('.client-review-slider').owlCarousel({
        rtl: true,
        margin: 10,
        nav: false,
        items: 1,
        controls: false,
        mouseDrag: true,
        loop: true,
        rewind: true,
        autoplay: true,
        autoplayButtonOutput: false,
        autoplayTimeout: 3000,
        navPosition: "bottom",
        speed: 400,
        gutter: 16,
        autoplayDirection: "backward"
    })


    $('.slider-range-four').owlCarousel({
        rtl: true,
        items: 4,
        margin:25,
        controls: false,
        mouseDrag: true,
        loop: true,
        rewind: true,
        autoplay: true,
        autoplayButtonOutput: false,
        autoplayTimeout: 3000,
        navPosition: "bottom",
        speed: 400,
        gutter: 24,
        responsive: {
            992: {
                items: 4
            },

            767: {
                items: 2
            },


            320: {
                items: 1
            },
        },
    })

    $('.slider-range-three').owlCarousel({
        rtl: true,
        items: 3,
        margin:25,
        controls: false,
        mouseDrag: true,

        loop: true,
        rewind: true,
        autoplay: true,
        autoplayButtonOutput: false,
        autoplayTimeout: 3000,
        navPosition: "bottom",
        speed: 400,
        gutter: 24,
        responsive: {
            992: {
                items: 3
            },

            767: {
                items: 2
            },

            320: {
                items: 1
            },
        },
    })


});


