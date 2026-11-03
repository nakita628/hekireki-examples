<?php

namespace App\Models;

enum ViewType: string
{
    case DOCUMENT_VIEW = 'DOCUMENT_VIEW';
    case DATAROOM_VIEW = 'DATAROOM_VIEW';
}