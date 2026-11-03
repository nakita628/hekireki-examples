<?php

namespace App\Models;

enum OAuthClientStatus: string
{
    case PENDING = 'pending';
    case APPROVED = 'approved';
    case REJECTED = 'rejected';
}