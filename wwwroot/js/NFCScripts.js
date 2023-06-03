var ChromeSamples = {
    log: function () {
        var line = Array.prototype.slice.call(arguments).map(function (argument) {
            return typeof argument === 'string' ? argument : JSON.stringify(argument);
        }).join(' ');

        document.querySelector('#log').textContent += line + '\n';
    },

    clearLog: function () {
        document.querySelector('#log').textContent = '';
    },

    setStatus: function (status) {
        document.querySelector('#status').textContent = status;
    },

    setContent: function (newContent) {
        var content = document.querySelector('#content');
        while (content.hasChildNodes()) {
            content.removeChild(content.lastChild);
        }
        content.appendChild(newContent);
    }
};

if (/Chrome\/(\d+\.\d+.\d+.\d+)/.test(navigator.userAgent)) {
    // Let's log a warning if the sample is not supposed to execute on this
    // version of Chrome.
    if (89 > parseInt(RegExp.$1)) {
        ChromeSamples.setStatus('Warning! Keep in mind this sample has been tested with Chrome ' + 89 + '.');
    }
}

log = ChromeSamples.log;
const NFCPresent = ("NDEFReader" in window);

if (!NFCPresent)//(!("NDEFReader" in window))
    ChromeSamples.setStatus("Зчитувач NFC відсутній. Використайте Chrome на Android пристрої.");

async function WriteNFC(SectionId, Id) {
    const vData = {
        SectionId: SectionId,
        Id: Id
    };
    const vEncoder = new TextEncoder();
    const vJsonRecord = {
        recordType: "mime",
        mediaType: "application/json",
        data: vEncoder.encode(JSON.stringify(vData))
    };
    const ndef = new NDEFReader();
    await ndef.write({ records: [vJsonRecord] });
}