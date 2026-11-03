<?php

namespace App\Models;

enum InAppAgentConversationVisibilityScope: string
{
    case PERSONAL = 'PERSONAL';
    case PROJECT = 'PROJECT';
}