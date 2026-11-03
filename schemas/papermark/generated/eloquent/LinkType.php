<?php

namespace App\Models;

enum LinkType: string
{
    case DOCUMENT_LINK = 'DOCUMENT_LINK';
    case DATAROOM_LINK = 'DATAROOM_LINK';
    case WORKFLOW_LINK = 'WORKFLOW_LINK';
}