(function (homePageController, $, undefined) {
    homePageController.initialize = function (options) {
        this.playlist = options.playlist;
        this.playlistItemCount = options.playlistItemCount;
        this.spinnerHtml = options.spinnerHtml;

        this.homepageContainer = $(".homepage-container");
        this.refreshPlaylistButton = $(this.homepageContainer).find(".refresh-video-list-btn");
        this.playlistContainer = $(this.homepageContainer).find(".playlist-container");
        this.refreshPlaylistUrl = "/umbraco/surface/playlist/SaveAndReturnPlaylistItems?playlist=" + this.playlist;

        this.registerRefreshButtonClick();

        if (this.playlistItemCount === '0') {
            homePageController.getYoutubePlaylistAndSave();
        }
    }

    homePageController.registerRefreshButtonClick = function() {
        this.refreshPlaylistButton.click(function () {
            $(homePageController.playlistContainer).html(homePageController.spinnerHtml);
            homePageController.getYoutubePlaylistAndSave();
        });
    }

    homePageController.getYoutubePlaylistAndSave = function() {
        $.ajax({
            url: homePageController.refreshPlaylistUrl,
            success: function(data) {
                $(homePageController.playlistContainer).html(data);
            },
            error: function(data) {
                alert("Error Retrieving Refreshed Playlist!");
            }
        });
    }

})(window.homePageController = window.homePageController || {}, jQuery);