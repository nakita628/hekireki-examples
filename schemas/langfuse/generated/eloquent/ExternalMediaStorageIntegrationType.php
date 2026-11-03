<?php

namespace App\Models;

enum ExternalMediaStorageIntegrationType: string
{
    case S3 = 'S3';
    case S3_COMPATIBLE = 'S3_COMPATIBLE';
}