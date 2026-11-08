<?php

namespace App\Models;

enum AuthMethod: string
{
    case PASSWORD = 'PASSWORD';
    case GOOGLE_OAUTH = 'GOOGLE_OAUTH';
    case GITHUB_OAUTH = 'GITHUB_OAUTH';
}