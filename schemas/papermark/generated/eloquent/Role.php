<?php

namespace App\Models;

enum Role: string
{
    case ADMIN = 'ADMIN';
    case MANAGER = 'MANAGER';
    case MEMBER = 'MEMBER';
    case DATAROOM_MEMBER = 'DATAROOM_MEMBER';
}