<?php

namespace App\Models;

enum ChallengeStatus: string
{
    case DECLINED = 'DECLINED';
    case BANNED = 'BANNED';
    case PENDING = 'PENDING';
    case ACTIVE = 'ACTIVE';
}