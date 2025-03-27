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

