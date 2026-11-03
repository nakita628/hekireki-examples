<?php

namespace App\Models;

enum IdentityProvider: string
{
    case DOCUMENSO = 'DOCUMENSO';
    case GOOGLE = 'GOOGLE';
    case OIDC = 'OIDC';
}