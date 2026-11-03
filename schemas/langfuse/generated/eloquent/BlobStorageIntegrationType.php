<?php

namespace App\Models;

enum BlobStorageIntegrationType: string
{
    case S3 = 'S3';
    case S3_COMPATIBLE = 'S3_COMPATIBLE';
    case AZURE_BLOB_STORAGE = 'AZURE_BLOB_STORAGE';
    case GOOGLE_CLOUD_STORAGE = 'GOOGLE_CLOUD_STORAGE';
}