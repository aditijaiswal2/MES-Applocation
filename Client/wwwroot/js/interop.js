
window.printDiv = function (divId) {
    const content = document.getElementById(divId);
    if (!content) {
        alert("Print content not found.");
        return;
    }

    const printWindow = window.open('', '_blank');

    // Collect stylesheets
    const stylesheets = Array.from(document.styleSheets)
        .filter(style => style.href)
        .map(style => `<link rel="stylesheet" href="${style.href}">`)
        .join("");

    const htmlContent = `
        <html>
            <head>
                <title>INCOMING ROTOR INSPECTION</title>
                ${stylesheets}
                <style>
                    body {
                        margin: 0;
                        padding: 0;
                        font-family: Arial, sans-serif;
                        background-color: white;
                        color: black;
                    }

                    .mud-dialog,
                    .mud-paper {
                        box-shadow: none !important;
                        border: 1px solid #000;
                        padding: 20px;
                        margin: 10px;
                        background-color: white;
                    }

                    .mud-dialog-title {
                        font-weight: bold;
                        font-size: 18px;
                        margin-bottom: 16px;
                        border-bottom: 1px solid #000;
                        padding-bottom: 8px;
                    }

                    .mud-grid {
                        display: grid !important;
                        grid-template-columns: repeat(3, 1fr) !important;
                        gap: 12px;
                    }

                    .mud-item {
                        padding: 4px;
                        box-sizing: border-box;
                    }

                    /* ✅ Uniform field container size */
                    .field-container {
                        border: 1px solid #000;
                        border-radius: 4px;
                        padding: 8px;
                        height: 100px; /* uniform height */
                        width: 100%; /* fills grid cell */
                        box-sizing: border-box;
                        display: flex;
                        flex-direction: column;
                        justify-content: center;
                        align-items: flex-start;
                        font-size: 14px;
                        overflow: hidden;
                    }

                    @media print {
                        body {
                            -webkit-print-color-adjust: exact;
                            print-color-adjust: exact;
                        }

                        .mud-grid,
                        .mud-item,
                        .field-container {
                            page-break-inside: avoid;
                        }

                        @page {
                            margin: 10mm;
                        }
                    }
                </style>
            </head>
            <body>
                <div class="mud-dialog mud-paper">
                    ${wrapFields(content.innerHTML)}
                </div>
            </body>
        </html>
    `;

    printWindow.document.open();
    printWindow.document.write(htmlContent);
    printWindow.document.close();

    printWindow.onload = () => {
        printWindow.focus();
        setTimeout(() => {
            printWindow.print();
            printWindow.onafterprint = () => printWindow.close();
        }, 500);
    };

    // Wrap each field in a fixed-size container
    function wrapFields(html) {
        const tempDiv = document.createElement("div");
        tempDiv.innerHTML = html;

        const targets = tempDiv.querySelectorAll(".mud-item, .mud-grid > div");
        targets.forEach(el => {
            const wrapper = document.createElement("div");
            wrapper.className = "field-container";
            wrapper.innerHTML = el.innerHTML;
            el.innerHTML = '';
            el.appendChild(wrapper);
        });

        return tempDiv.innerHTML;
    }
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