function printForm() {
    // Clone the form section
    var formSection = document.getElementById('printForm-section').cloneNode(true);

    // Find all input fields (TextField uses <input>)
    var inputs = formSection.querySelectorAll('input');

    inputs.forEach(function (input) {
        var value = input.value; // Get current user-typed value
        var textNode = document.createTextNode(value ? value : ''); // Create text node with value

        // Replace the input field with its value
        input.parentNode.replaceChild(textNode, input);
    });

    // Now formSection has input values replaced with text
    var content = formSection.innerHTML;

    var styles = `
    <style>
        body, * {
             font-family: Arial, sans-serif !important;
            font-size: 18px !important;
            background-color: white;
            color: black;
            margin: 2px;
            padding: 0;

        }


    body {
        border: 2px solid black;
        padding: 20px;
        box-sizing: border-box;
        margin: 0;
    }
 
 .print-wrapper {
    width: 100% !important;
      overflow: hidden !important;
}

        .mud-grid {
                display: grid !important;
                grid-template-columns: repeat(3, 1fr) !important; /* Change to 4 columns */
                gap: 12px;
            }

            .mud-item {
                padding: 4px;
               
            }

                .print-footer {
        margin-top: 40px;
        text-align: center;
        font-size: 16px;
    }

            table {
                width: 100% !important;
            border-collapse: collapse;
            margin: 0 auto;
            }

            th, td {
                padding: 8px 12px;
                border: 1px solid #000;
                text-align: center;
                  white-space: nowrap;                
            }
        }
    </style>
`;

  // var logoHtml = '  <img src="" alt="Logo" class="px-4 mt-2 mb-2" Style = "width:200px;height:40px;" />';
    var headerHtml = `
  <div style="display: flex; align-items: center; margin-bottom: 20px;">
      <img id="logo" src="Image/MAAG-removebg-preview.png" alt="Logo" style="height: 80px; margin-right: 20px;" />
            <h1 style="font-size: 24px; margin: 0;">SERVICE ROTOR FINAL INSPECTION</h1>
        </div>
`;

    var footerHtml = `
    <div class="print-footer">
        <h5>For New Rotors and Spare Parts</h5>
                            <p Class="text-center">Please Call</p>
                            <h6>1.800.844.2927 or 330.677.2225</h6>
                            <p>8:00 am - 5:00 pm, M-F</p>
    </div>
`;

    //  var printWindow = window.open('', '_blank');
    var printWindow = window.open('', 'PrintWindow', 'width=1200,height=650,top=100,left=100,scrollbars=yes,resizable=yes');

   // var printWindow = window.open('', 'PrintWindow', 'width=100%,height=650,top=100,left=100,scrollbars=yes,resizable=no');


   // var printWindow = window.open('', '', 'height=650,width=900');
    //printWindow.document.write('<html><head><title> </title>');
    //printWindow.document.write(styles);
    //printWindow.document.write('</head><body>');
    //printWindow.document.write(headerHtml);
    //printWindow.document.write(`<div class="print-wrapper">${content}</div>`);

    printWindow.document.write('<html><head><title> </title>');
    printWindow.document.write(styles);
    printWindow.document.write('</head><body>');
    printWindow.document.write(`<div class="print-wrapper"><div class="print-container">${headerHtml}${content}${footerHtml}</div></div>`);
    printWindow.document.write('</body></html>');


  //  printWindow.document.write(content);
    printWindow.document.write('</body></html>');

    printWindow.document.close();
    // Wait until all content (including images) is loaded
    printWindow.onload = function () {
        const logoImg = printWindow.document.getElementById('logo');
        if (logoImg.complete) {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        } else {
            logoImg.onload = function () {
                printWindow.focus();
                printWindow.print();
                printWindow.close();
            };
        }
    };
}

function printincomingImage(imageDataUrl, labelText) {
    const previewContent = document.getElementById("print-section");

    const htmlContent = `
       <html>
    <head>
        <style>
            body, * {
                font-family: Arial, sans-serif !important;
                font-size: 14px !important;
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

         .content-row {
    display: flex;
    align-items: flex-start;
    gap: 15px;
}

.qr-container {
    flex: 0 0 auto;
    text-align: center;
}

.content-area {
    flex: 1;
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
    <h2 class="dialog-title">INCOMING ROTOR INSPECTION</h2>
    <div class="content-row">
        <div class="qr-container">
            <img id="qr-img" src="${imageDataUrl}" alt="QR Code" />
        </div>
        <div class="content-area">
            ${previewContent ? previewContent.innerHTML : ""}
        </div>
    </div>
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

    const printWindow = window.open('', 'PrintWindow', 'width=900,height=700,left=100,top=100,toolbar=no,scrollbars=no,resizable=no');
    printWindow.document.write(htmlContent);
    printWindow.document.close();

    //const printWindow = window.open('', '_blank');



    //printWindow.document.write(htmlContent);
    //printWindow.document.close();

}
function downloadFile(fileName, base64, mimeType) {
    const link = document.createElement('a');
    link.download = fileName;
    link.href = `data:${mimeType};base64,${base64}`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function printImage(imageData, partOrLoc, customer, date, reportnumber) {
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

        let nextLineY = qrCodeY - 45;

        if (date) {
            const dateObj = new Date(date);
            const options = {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: true,
            };
            const formatted = dateObj.toLocaleString('en-US', options);
            context.fillText(formatted, canvasWidth / 2, nextLineY);
            nextLineY += 18; // Move down for next line
        }

        if (reportnumber) {
            context.fillText(reportnumber, canvasWidth / 2, nextLineY);
            nextLineY += 18;
        }

        if (partOrLoc) {
            context.fillText(partOrLoc, canvasWidth / 2, nextLineY);
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
