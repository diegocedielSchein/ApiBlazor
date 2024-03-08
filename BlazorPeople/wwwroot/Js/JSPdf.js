
    window.descargarpdf = function (data, fileName, contentType) {
        var blob = new Blob([data], {type: contentType });

    if (window.navigator.msSaveBlob) {
        window.navigator.msSaveBlob(blob, fileName);
        } else {
            var link = document.createElement("a");

    if (link.download !== undefined) {
                var url = URL.createObjectURL(blob);
    link.setAttribute("href", url);
    link.setAttribute("download", fileName);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
            }
        }
    }

