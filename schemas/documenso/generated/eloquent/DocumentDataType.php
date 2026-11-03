<?php

namespace App\Models;

enum DocumentDataType: string
{
    case S3_PATH = 'S3_PATH';
    case BYTES = 'BYTES';
    case BYTES_64 = 'BYTES_64';
}