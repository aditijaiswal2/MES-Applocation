function isMobileDevice() {
    return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
}

function checkCameraPermission() {
    return new Promise((resolve, reject) => {
        navigator.permissions.query({ name: 'camera' })
            .then(permissionStatus => {
                if (permissionStatus.state === 'prompt') {
                    resolve('prompt');
                } else if (permissionStatus.state === 'granted') {
                    resolve('granted');
                } else {
                    reject('denied');
                }
            })
            .catch(error => {
                console.error('Error checking camera permission:', error);
                reject(error);
            });
    });
}

window.downloadImageFromBase64 = (base64Data, filename) => {
    const link = document.createElement('a');
    link.href = base64Data;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};


