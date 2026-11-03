<?php

namespace App\Models;

enum ConversationVisibility: string
{
    case PRIVATE = 'PRIVATE';
    case PUBLIC_LINK = 'PUBLIC_LINK';
    case PUBLIC_GROUP = 'PUBLIC_GROUP';
    case PUBLIC_DOCUMENT = 'PUBLIC_DOCUMENT';
    case PUBLIC_DATAROOM = 'PUBLIC_DATAROOM';
}