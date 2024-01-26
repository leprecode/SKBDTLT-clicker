mergeInto(LibraryManager.library, {

    GetLanguage: function () {
        var lang = ysdk.environment.i18n.lang;
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        return buffer;

    },

    ShowFullScreenAD: function () {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function (wasShown) {
                    myGameInstance.SendMessage('=====YANDEX=====', 'OnFullscreenADVEndOrError');
                },
                onError: function (error) {
                    myGameInstance.SendMessage('=====YANDEX=====', 'OnFullscreenADVEndOrError');
                }
            }
        });
    },

    GetPlatformInfo: function () {
        var ua = navigator.userAgent;
        var platform;

         if (ua.toLowerCase().includes("win")) {
            console.log("GET IN JSLIB: Windows");
             platform = "Windows";
        } else if (ua.toLowerCase().includes("mac")) {
            console.log("GET IN JSLIB: Mac");
             platform = "Mac";
        } else if (ua.toLowerCase().includes("linux i686") || ua.toLowerCase().includes("linux x86_64")) {
            console.log("GET IN JSLIB: Linux");     
             platform = "Linux";
        } else if (ua.toLowerCase().includes("iphone") || ua.toLowerCase().includes("ipad") || ua.toLowerCase().includes("ipod")) {
            console.log("GET IN JSLIB: iOS");     
             platform = "iOS";
        } else if (ua.toLowerCase().includes("android") || ua.toLowerCase().includes("linux armv7l") || ua.toLowerCase().includes("linux i686")) {
            console.log("GET IN JSLIB: Android");     
            platform = "Android";
        } else {
            console.log("GET IN JSLIB: Unknown Platform");     
             platform = "Unknown Platform";
        }

        var bufferSize = lengthBytesUTF8(platform) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(platform, buffer, bufferSize);
        return buffer;
    },


    ShowFullScreenADInFirstTime: function () {
        YaGames.init()
            .then(function (ysdk) {
                ysdk.adv.showFullscreenAdv({
                    callbacks: {
                        onClose: function (wasShown) {
                            myGameInstance.SendMessage('=====YANDEX=====', 'OnFullscreenADVEndOrError');
                        },
                        onError: function (error) {
                            myGameInstance.SendMessage('=====YANDEX=====', 'OnFullscreenADVEndOrError');
                        }
                    }
                });
            });
    },
});

