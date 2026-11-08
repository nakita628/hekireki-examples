<?php

namespace App\Models;

enum TemplateType: string
{
    case TRANSACTIONAL = 'TRANSACTIONAL';
    case MARKETING = 'MARKETING';
    case HEADLESS = 'HEADLESS';
}