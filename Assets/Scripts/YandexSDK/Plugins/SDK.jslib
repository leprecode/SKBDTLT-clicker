mergeInto(LibraryManager.library, {

    GetLanguage: function () {
        YaGames.init()
            .then(function (ysdk) {
                var lang = ysdk.environment.i18n.lang;
                var bufferSize = lengthBytesUTF8(lang) + 1;
                var buffer = _malloc(bufferSize);
                stringToUTF8(lang, buffer, bufferSize);
                return buffer;
            });
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

