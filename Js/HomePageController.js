(function (homePageController, $, undefined) {
    homePageController.initialize = function (options) {
        this.playlist = options.playlist;
        console.log(this.playlist);
    }

    homePageController.someFunctionThatUsesUrl = function () {
        $.ajax({
            url: this.someUrl
        });
    }
})(window.homePageController = window.homePageController || {}, jQuery);