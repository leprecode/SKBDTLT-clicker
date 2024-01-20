mergeInto(LibraryManager.library, {
	
	GetLanguage: function () {
		var lang = ysdk.enviroment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = malloc(bufferSize);
		stringToUTF8(lang,buffer,bufferSize);
		return buffer;
	},
	

});