<?php

namespace App\Models;

enum OAuthClientType: string
{
    case CONFIDENTIAL = 'confidential';
    case PUBLIC = 'public';
}