<?php

namespace App\Models;

enum MediaAssociationOrigin: string
{
    case UNKNOWN = 'UNKNOWN';
    case CLIENT_UPLOAD = 'CLIENT_UPLOAD';
    case INGESTION_MEDIA_EXTRACTION = 'INGESTION_MEDIA_EXTRACTION';
    case INGESTION_FIELD_OVERFLOW = 'INGESTION_FIELD_OVERFLOW';
}