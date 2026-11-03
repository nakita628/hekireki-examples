<?php

namespace App\Models;

enum TagType: string
{
    case LINK_TAG = 'LINK_TAG';
    case DOCUMENT_TAG = 'DOCUMENT_TAG';
    case DATAROOM_TAG = 'DATAROOM_TAG';
}