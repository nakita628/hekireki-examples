<?php

namespace App\Models;

enum DocumentSource: string
{
    case DOCUMENT = 'DOCUMENT';
    case TEMPLATE = 'TEMPLATE';
    case TEMPLATE_DIRECT_LINK = 'TEMPLATE_DIRECT_LINK';
}