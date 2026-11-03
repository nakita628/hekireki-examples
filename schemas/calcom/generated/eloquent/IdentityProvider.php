<?php

namespace App\Models;

enum IdentityProvider: string
{
    case CAL = 'CAL';
    case GOOGLE = 'GOOGLE';
    case SAML = 'SAML';
    case AZUREAD = 'AZUREAD';
}