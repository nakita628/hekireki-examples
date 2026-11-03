<?php

namespace App\Models;

enum OrganisationType: string
{
    case PERSONAL = 'PERSONAL';
    case ORGANISATION = 'ORGANISATION';
}