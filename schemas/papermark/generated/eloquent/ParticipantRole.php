<?php

namespace App\Models;

enum ParticipantRole: string
{
    case OWNER = 'OWNER';
    case PARTICIPANT = 'PARTICIPANT';
}