<?php

namespace App\Models;

enum BlobStorageExportMode: string
{
    case FULL_HISTORY = 'FULL_HISTORY';
    case FROM_TODAY = 'FROM_TODAY';
    case FROM_CUSTOM_DATE = 'FROM_CUSTOM_DATE';
}