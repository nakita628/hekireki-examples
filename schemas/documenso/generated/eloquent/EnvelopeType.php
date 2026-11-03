<?php

namespace App\Models;

enum EnvelopeType: string
{
    case DOCUMENT = 'DOCUMENT';
    case TEMPLATE = 'TEMPLATE';
}