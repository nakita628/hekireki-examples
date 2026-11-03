<?php

namespace App\Models;

enum OidcSigningKeyPurpose: string
{
    case EXTERNAL_OIDC = 'EXTERNAL_OIDC';
    case INTERNAL_RUN = 'INTERNAL_RUN';
}