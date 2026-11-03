<?php

namespace App\Models;

enum DocumentStorageType: string
{
    case S3_PATH = 'S3_PATH';
    case VERCEL_BLOB = 'VERCEL_BLOB';
}