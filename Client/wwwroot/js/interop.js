window.printDiv = function (divId) {
    const content = document.getElementById(divId);
    if (!content) {
        alert("Print content not found.");
        return;
    }

    const printWindow = window.open('', '_blank');

    // Clone and write full HTML with corrected layout styles
    printWindow.document.write(`
        <html>
            <head>
                <title>INCOMING ROTOR INSPECTION</title>
                <link rel="stylesheet" href="_content/MudBlazor/MudBlazor.min.css" />
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        padding: 20px;
                        margin: 0;
                    }

                    /* Force grid layout for printing */
                    .mud-grid {
                        display: grid !important;
                        grid-template-columns: repeat(6, 1fr) !important;
                        gap: 16px;
                    }

                    .mud-item {
                        width: 100% !important;
                        margin: 0 !important;
                        padding: 4px;
                        box-sizing: border-box;
                    }

                    .mud-stack {
                        display: flex;
                        flex-direction: column;
                        gap: 2px;
                    }

                    @media print {
                        body {
                            margin: 0;
                        }

                        .mud-grid, .mud-item {
                            page-break-inside: avoid;
                        }
                    }
                </style>
            </head>
            <body>
                <div>${content.innerHTML}</div>
            </body>
        </html> 
    `);

    printWindow.document.close();

    printWindow.onload = function () {
        printWindow.focus();
        printWindow.print();
        printWindow.onafterprint = function () {
            printWindow.close();
        };
    };
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