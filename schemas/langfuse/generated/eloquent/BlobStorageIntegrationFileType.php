<?php

namespace App\Models;

enum BlobStorageIntegrationFileType: string
{
    case JSON = 'JSON';
    case CSV = 'CSV';
    case JSONL = 'JSONL';
    case PARQUET = 'PARQUET';
}