
function printincomingImage(imageDataUrl, labelText) {
    const previewContent = document.getElementById("print-section");

    const htmlContent = `
        <html>
            <head>
               
                <style>
                   body, * {
                        font-family: Arial, sans-serif !important;
                        font-size: 16px !important;
                        background-color: white;
                        color: black;
                        margin: 3px;
                        padding: 0;
                    }

                    .dialog-title {
                      text-align: center;
                      margin: 0;
                      font-size: 14px !important;
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

                    .qr-container {
                        text-align: center;
                        margin-top: 20px;
                    }
.qr-container img {
    width: 75px !important;
    height: 75px !important;
    object-fit: contain;
}
                    @media print {
                        @page {
                            margin: 10mm;
                          
                        }
                    }
                </style>
            </head>
            <body>
              
                  <div class="mud-dialog">
                   <title class="dialog-title">INCOMING ROTOR INSPECTION</title>
                       <img id="qr-img" src="${imageDataUrl}" alt="QR Code" />

                    ${previewContent ? previewContent.innerHTML : ""}
                </div>

                <script>
                    window.onload = function() {
                        const img = document.getElementById('qr-img');
                        if (img.complete) {
                            window.print();
                            window.onafterprint = () => window.close();
                        } else {
                            img.onload = function () {
                                setTimeout(() => {
                                    window.print();
                                    window.onafterprint = () => window.close();
                                }, 300); // wait for rendering
                            };
                        }
                    }
                </script>
            </body>
        </html>
    `;


    const printWindow = window.open('', '_blank');



    printWindow.document.write(htmlContent);
    printWindow.document.close();

}



function printImage(imageData, partOrLoc, customer) {
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
        const qrCodeSize = canvasWidth * 0.85;
        const qrCodeX = (canvasWidth - qrCodeSize) / 2;
        const qrCodeY = (canvasHeight - qrCodeSize) / 2 - 10;

        // Draw the QR code image on the canvas
        context.drawImage(img, qrCodeX, qrCodeY, qrCodeSize, qrCodeSize);

        // Add the partOrLoc text below the QR code
        context.font = 'bold 16px Arial';
        context.textAlign = 'center';
        context.fillStyle = 'black';

        if (partOrLoc || customer) {
            const combinedText = `${partOrLoc || ""} ${customer || ""}`.trim();
            context.textAlign = "center";
            context.fillStyle = "black";
            context.font = "14px Arial";

            context.fillText(combinedText, canvasWidth / 2, qrCodeY + qrCodeSize + 20);
        }


        //if (partOrLoc, customer) {
        //    context.fillText(partOrLoc, canvasWidth / 2, qrCodeY + qrCodeSize + 20);
        //    context.fillText(customer, canvasWidth / 2, qrCodeY + qrCodeSize + 20);
        //}


        // Convert the canvas to an image for downloading
        const combinedImage = canvas.toDataURL('image/png');

        // Trigger download
        const link = document.createElement('a');
        link.href = combinedImage;
        link.download = 'QRCode.png';
        link.style.display = 'none';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

        // Open new window for print
        const win = window.open('', '', 'height=800,width=800');
        win.document.write('<html><head><title>Print QR Code</title>');
        win.document.write('<style>');
        win.document.write(`
        @media print {
            @page {
                margin: 0;
                size: 2.4in 2.4in;
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
                margin-top: 5px;
            }
        }
    `);
        win.document.write('</style></head><body>');
        win.document.write('<div class="print-container">');
        win.document.write(`<img src="${combinedImage}" alt="QR Code" />`);

        win.document.write('</div></body></html>');
        win.document.close();

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