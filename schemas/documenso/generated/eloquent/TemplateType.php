<?php

namespace App\Models;

enum TemplateType: string
{
    case PUBLIC = 'PUBLIC';
    case PRIVATE = 'PRIVATE';
    case ORGANISATION = 'ORGANISATION';
}