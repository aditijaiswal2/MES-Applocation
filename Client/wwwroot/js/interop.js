function printForm() {
    // Open a new window for printing
    var printWindow = window.open('', '', 'height=650, width=900');

    // Get the content of the form (with dynamic values rendered)
    var content = document.getElementById('printForm-section').innerHTML;

    // Ensure that dynamic form data is included
    var styles = `
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
            .mud-dialog, .mud-paper {
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
                grid-template-columns: repeat(2, 1fr) !important;
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
    `;

    // Write the content and styles to the print window
    printWindow.document.write('<html><head><title>Print</title>' + styles + '</head><body>');
    printWindow.document.write(content);  // Content should reflect the updated form values
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
}

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
                grid-template-columns: repeat(4, 1fr) !important; /* Change to 4 columns */
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
    const dpi = 96;
    const canvasWidth = 2.4 * dpi;
    const canvasHeight = 4 * dpi;

    const canvas = document.createElement('canvas');
    const context = canvas.getContext('2d');
    canvas.width = canvasWidth;
    canvas.height = canvasHeight;

    const img = new Image();
    img.src = imageData;

    img.onload = function () {
        const qrCodeSize = canvasWidth * 0.85;
        const qrCodeX = (canvasWidth - qrCodeSize) / 2;
        const qrCodeY = (canvasHeight - qrCodeSize) / 2;

        context.textAlign = 'center';
        context.fillStyle = 'black';
        context.font = '14px Arial';

        // Top label - reduced gap
        if (partOrLoc) {
            context.fillText(partOrLoc, canvasWidth / 2, qrCodeY - 5);
        }

        // Draw QR code
        context.drawImage(img, qrCodeX, qrCodeY, qrCodeSize, qrCodeSize);

        // Function to wrap text
        function wrapText(text, maxWidth) {
            const words = text.split(' ');
            let line = '';
            const lines = [];
            for (let i = 0; i < words.length; i++) {
                const testLine = line + words[i] + ' ';
                const testWidth = context.measureText(testLine).width;
                if (testWidth > maxWidth && i > 0) {
                    lines.push(line);
                    line = words[i] + ' ';
                } else {
                    line = testLine;
                }
            }
            lines.push(line);
            return lines;
        }

        // Bottom label - reduced gap
        if (customer) {
            const maxWidth = canvasWidth - 40; // padding on both sides
            const lines = wrapText(customer, maxWidth);
            const startY = qrCodeY + qrCodeSize + 18;
            lines.forEach((line, index) => {
                context.fillText(line, canvasWidth / 2, startY + index * 18); // Adjust line height here
            });
        }

        const combinedImage = canvas.toDataURL('image/png');

        const win = window.open('', '', 'height=800,width=800');
        win.document.write('<html><head><title>Print QR Code</title>');
        win.document.write('<style>@media print { @page { margin: 0; size: auto; } body, html { margin: 0; padding: 0; display: flex; justify-content: center; align-items: center; } .print-container { display: flex; flex-direction: column; justify-content: center; align-items: center; } img { max-width: 100%; object-fit: contain; } }</style>');
        win.document.write('</head><body>');
        win.document.write('<div class="print-container">');
        win.document.write(`<img src="${combinedImage}" alt="QR Code" />`);
        win.document.write('</div></body></html>');
        win.document.close();

        setTimeout(() => {
            win.print();
            win.close();
        }, 200);
    };

    img.onerror = function () {
        console.error('Error loading image');
    };
}
