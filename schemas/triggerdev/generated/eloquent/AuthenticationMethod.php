<?php

namespace App\Models;

enum AuthenticationMethod: string
{
    case GITHUB = 'GITHUB';
    case MAGIC_LINK = 'MAGIC_LINK';
    case GOOGLE = 'GOOGLE';
    case SSO = 'SSO';
}