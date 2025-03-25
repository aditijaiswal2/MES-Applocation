async function startVideo(src) {
    try {
        // Ensure the spinner exists before trying to show it
        const spinner = document.getElementById('loadingSpinner');
        if (spinner) {
            showLoadingSpinner();
        } else {
            console.warn('Spinner element not found. Skipping spinner display.');
        }

        // Check if camera access is supported
        if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia) {
            console.error('navigator.mediaDevices.getUserMedia is not supported on this browser.');
            alert('Camera access is not supported on this device or browser.');
            if (spinner) hideLoadingSpinner();
            return;
        }

        // Check camera permission status
        const permissionStatus = await navigator.permissions.query({ name: 'camera' });
        if (permissionStatus.state === 'denied') {
            console.error('Camera permission denied.');
            alert('Camera permission has been denied. Please enable camera access in your browser settings.');
            if (spinner) hideLoadingSpinner();
            return;
        }

        let videoConstraints = { width: { ideal: 1280 }, height: { ideal: 720 } };

        // Open the appropriate camera based on device type
        if (isMobileDevice()) {
            await openFrontAndBackCameras(src, videoConstraints);
        } else {
            await openLaptopCamera(src, videoConstraints);
        }
    } catch (error) {
        console.error('Error starting video:', error.message || error);
        alert(`Error accessing the camera: ${error.message || error}. Please check your camera settings.`);
        if (spinner) hideLoadingSpinner();
    }
}

function showLoadingSpinner() {
    const spinner = document.getElementById('loadingSpinner');
    if (spinner) {
        spinner.style.display = 'block';
    } else {
        console.warn('Spinner element not found.');
    }
}

function hideLoadingSpinner() {
    const spinner = document.getElementById('loadingSpinner');
    if (spinner) {
        spinner.style.display = 'none';
    } else {
        console.warn('Spinner element not found.');
    }
}

async function openLaptopCamera(src, videoConstraints) {
    const localMediaStream = await navigator.mediaDevices.getUserMedia({
        video: videoConstraints,
        audio: false
    });
    let video = document.getElementById(src);
    if (video) {
        video.srcObject = localMediaStream;
        video.onloadedmetadata = function () {
            video.play();
            hideLoadingSpinner();
        };
    } else {
        console.error('Video element with ID ' + src + ' not found.');
        throw new Error('Video element not found.');
        hideLoadingSpinner();
    }
}

async function openFrontAndBackCameras(src, videoConstraints) {
    // Open the front camera
    const frontMediaStream = await navigator.mediaDevices.getUserMedia({
        video: { ...videoConstraints, facingMode: "user" },
        audio: false
    });
    let frontVideo = document.getElementById(src);
    if (frontVideo) {
        frontVideo.srcObject = frontMediaStream;
        frontVideo.onloadedmetadata = function () {
            frontVideo.play();
        };
    } else {
        console.error('Front video element with ID ' + src + ' not found.');
        throw new Error('Front video element not found.');
    }

    // Open the back camera
    const backMediaStream = await navigator.mediaDevices.getUserMedia({
        video: { ...videoConstraints, facingMode: { exact: "environment" } },
        audio: false
    });
    let backVideo = document.getElementById('backCamera');
    if (backVideo) {
        backVideo.srcObject = backMediaStream;
        backVideo.onloadedmetadata = function () {
            backVideo.play();
        };
    } else {
        console.error('Back video element with ID backCamera not found.');
        throw new Error('Back video element not found.');
    }
}

async function getFrame(src, dest, dotnetHelper) {
    try {

        let video = document.getElementById(src);
        let canvas = document.getElementById(dest);

        if (video && canvas) {
            canvas.width = 600;
            canvas.height = 600;
            let ctx = canvas.getContext('2d');
            ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

            let dataUrl = canvas.toDataURL("image/png");
            const base64ImageSize = Math.round((dataUrl.length * 3) / 4);

            await dotnetHelper.invokeMethodAsync('ProcessImage', dataUrl, base64ImageSize);
        } else {
            console.error('Video or canvas element not found.');
            throw new Error('Video or canvas element not found.');
        }
    } catch (error) {
        console.error('Error capturing frame:', error);
        throw error;
    }
}

function stopVideo(src) {
    let video = document.getElementById(src);

    if (video) {
        let tracks = video.srcObject?.getTracks();
        if (tracks) {
            tracks.forEach(track => track.stop());
            video.srcObject = null;

        }
    } else {
        console.error('Video element with ID ' + src + ' not found.');
    }
}


function closeCamera() {
    let video = document.querySelector('video');
    if (video && video.srcObject) {
        let tracks = video.srcObject.getTracks();
        tracks.forEach(track => track.stop());
        video.srcObject = null;
    } else {
        console.error('No active video stream found to close.');
    }
}

function isMobileDevice() {
    return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
}