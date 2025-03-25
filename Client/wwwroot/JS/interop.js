//function jsSaveAsFile(filename, byteBase64) {
//    var link = document.createElement('a');
//    link.download = filename;
//    link.href = "data:image/png;base64," + byteBase64;
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
//}

window.printQRCode = function () {
    window.print();
};



function printImage(imageData, partOrLoc) {
    // Define the canvas size in pixels (2.4 inches at 96 DPI)
    const dpi = 96;
    const canvasWidth = 2.4 * dpi; // 230.4 pixels
    const canvasHeight = canvasWidth; // Square canvas: 2.4x2.4 inches

    // Create a canvas element
    const canvas = document.createElement('canvas');
    const context = canvas.getContext('2d');

    canvas.width = canvasWidth;
    canvas.height = canvasHeight;

    // Load the image onto the canvas
    const img = new Image();
    img.src = imageData;

    img.onload = function () {
        // Calculate dimensions for the QR code area
        const qrCodeSize = canvasWidth * 0.85; // QR code takes 85% of the canvas width
        const qrCodeX = (canvasWidth - qrCodeSize) / 2; // Center QR code horizontally
        const qrCodeY = (canvasHeight - qrCodeSize) / 2 - 10; // Center vertically, with padding for text

        // Draw the QR code image on the canvas
        context.drawImage(img, qrCodeX, qrCodeY, qrCodeSize, qrCodeSize);

        // Add the partOrLoc text below the QR code
        if (partOrLoc) {
            context.font = 'bold 16px Arial'; // Adjust font size to fit within the canvas
            context.textAlign = 'center';
            context.fillStyle = 'black';
            context.fillText(partOrLoc, canvasWidth / 2, qrCodeY + qrCodeSize + 20); // Text below QR code
        }
        // Convert the canvas to an image for downloading
        const combinedImage = canvas.toDataURL('image/png');

        // Trigger download of the image
        const link = document.createElement('a');
        link.href = combinedImage;
        link.download = 'QRCode.png';
        link.style.display = 'none';
        document.body.appendChild(link);
        link.click(); // Trigger download
        document.body.removeChild(link); // Clean up

        // Open a new window for print preview
        const win = window.open('', '', 'height=800,width=800');
        win.document.write('<html><head><title>Print QR Code</title>');
        win.document.write('<style>');
        win.document.write(`
            @media print {
                @page {
                    margin: 0;
                    size: 2.4in 2.4in; /* Custom square paper size */
                }
                body, html {
                    margin: 0;
                    padding: 0;
                    width: 100%;
                    height: 100%;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    text-align: center;
                    background: none !important;
                }
                .print-container {
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    align-items: center;
                }
                img {
                    max-width: 100%;                   
                    object-fit: contain;
                }
                .text {
                    font-size: 14px;
                    font-weight: bold;
                    margin-top: 10px;
                }
            }
        `);
        win.document.write('</style></head><body>');
        win.document.write('<div class="print-container">');
        win.document.write(`<img src="${combinedImage}" alt="QR Code" />`);
        //if (partOrLoc) {
        //    win.document.write(`<div class="text">${partOrLoc}</div>`);
        //}
        win.document.write('</div></body></html>');
        win.document.close();

        // Delay printing to allow the window to fully load
        setTimeout(() => {
            win.print();
            win.close();
        }, 200);
    };

    // Handle errors if the image fails to load
    img.onerror = function () {
        console.error('Error loading image');
    };
}





function printTableContent() {
    const contentToPrint = document.getElementById("printableTable");

    const newWin = window.open("");
    newWin.document.write(`
        <html>
        <head>
        <title>Project Print List</title>
        <style>
         @page {
                margin: 2mm;
            }
                /* Include CSS for borders in print */
                table, th, td {
                    border: 1px solid black;
                    border-collapse: collapse;
                    padding: 8px;
                    text-align: left;
                    width: 100%; /* Ensure full width */
                }
                /* Make sure the body uses full width and height */
                body {
                    margin: 0; 
                    padding: 5mm 5mm; 
                    width: calc(100% - 10mm); /* Compensate for left & right margin */ 
                    height: 100%;
                }
                /* Remove all background images or colors */
                body, table, th, td {
                    background: none !important;
                }
                /* Reset list styles to prevent any unwanted symbols */
                ul, ol {
                    list-style-type: none;
                    margin: 0;
                    padding: 0;
                }
                li::before {
                    content: none;
                }
                /* Hide unwanted elements in print */
                @media print {
                    .mud-table-pagination, 
                    button, 
                    .mud-table-toolbar {
                        display: none !important;
                    }
                    /* Ensure no page breaks occur within the table */
                    .table {
                        page-break-inside: avoid;
                    }
                }
                /* Hide pseudo-elements to prevent unwanted symbols */
                *::before, *::after {
                    content: none !important;
                    display: none !important;
                }
</style>
</head>
<body>
            ${contentToPrint.outerHTML}
</body>
</html>
    `);

    newWin.document.close();
    newWin.focus();
    newWin.print();
    newWin.close();
}


async function downloadQRCodeWithText(imageData, text) {
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');

    // Load QR code image
    const qrImage = new Image();
    qrImage.src = imageData;

    qrImage.onload = function () {
        // Set canvas dimensions
        canvas.width = 300; // Example width
        canvas.height = 330; // Adjust height to minimize extra space

        // Draw QR code on canvas
        ctx.drawImage(qrImage, 50, 20, 200, 200);

        // Add text closer to the QR code
        ctx.font = '16px Arial';
        ctx.textAlign = 'center';
        ctx.fillStyle = 'black';
        ctx.fillText(text, canvas.width / 2, 240); // Adjust y-coordinate for text placement

        // Convert canvas content to image URL
        const combinedImage = canvas.toDataURL('image/png');

        // Create a link element to trigger the download
        const link = document.createElement('a');
        link.href = combinedImage;
        link.download = 'QRCode.png'; // File name for the downloaded image
        link.style.display = 'none'; // Hide the link element

        // Append link to the document, trigger the download, and remove the link
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };
}







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

