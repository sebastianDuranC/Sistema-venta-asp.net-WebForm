# Create directories
New-Item -ItemType Directory -Force -Path "CapaPresentacion/Scripts/DataTables"
New-Item -ItemType Directory -Force -Path "CapaPresentacion/Content/DataTables/css"

# Download jQuery
Invoke-WebRequest -Uri "https://code.jquery.com/jquery-3.7.1.min.js" -OutFile "CapaPresentacion/Scripts/jquery-3.7.1.min.js"

# Download DataTables files
$dataTablesFiles = @(
    "https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js",
    "https://cdn.datatables.net/1.13.7/css/jquery.dataTables.min.css",
    "https://cdn.datatables.net/buttons/2.4.2/js/dataTables.buttons.min.js",
    "https://cdn.datatables.net/buttons/2.4.2/css/buttons.dataTables.min.css",
    "https://cdn.datatables.net/buttons/2.4.2/js/buttons.html5.min.js",
    "https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js",
    "https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js",
    "https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"
)

foreach ($file in $dataTablesFiles) {
    $fileName = Split-Path $file -Leaf
    if ($file -like "*.css") {
        Invoke-WebRequest -Uri $file -OutFile "CapaPresentacion/Content/DataTables/css/$fileName"
    } else {
        Invoke-WebRequest -Uri $file -OutFile "CapaPresentacion/Scripts/DataTables/$fileName"
    }
}

# Download Spanish language file
Invoke-WebRequest -Uri "https://cdn.datatables.net/plug-ins/1.13.7/i18n/es-ES.json" -OutFile "CapaPresentacion/Scripts/DataTables/es-ES.json" 