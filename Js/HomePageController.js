(function (homePageController, $, undefined) {
    homePageController.initialize = function (options) {
        this.playlist = options.playlist;
        this.numberOfPlaylistItems = options.numberOfPlaylistItems;

        this.homepageContainer = $(".homepage-container");
        this.refreshPlaylistButton = $(this.homepageContainer).find(".refresh-video-list-btn");
        this.playlistContainer = $(this.homepageContainer).find(".playlist-container");
        this.refreshPlaylistUrl = "/umbraco/surface/playlist/SaveAndReturnPlaylistItems?playlist=" + this.playlist;

        this.registerRefreshButtonClick();
    }

    homePageController.registerRefreshButtonClick = function() {
        this.refreshPlaylistButton.click(function () {
            homePageController.getYoutubePlaylistAndSave();
        });
    }

    homePageController.getYoutubePlaylistAndSave = function() {
        $.ajax({
            url: this.refreshPlaylistUrl,
            success: function(data) {
                if (console && console.log) {
                    console.log(data);
                }
            },
            error: function(data) {
                alert("Error Retrieving Refreshed Playlist!");
            }
        });
    }

})(window.homePageController = window.homePageController || {}, jQuery);