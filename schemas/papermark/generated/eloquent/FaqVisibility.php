<?php

namespace App\Models;

enum FaqVisibility: string
{
    case PUBLIC_DATAROOM = 'PUBLIC_DATAROOM';
    case PUBLIC_LINK = 'PUBLIC_LINK';
    case PUBLIC_DOCUMENT = 'PUBLIC_DOCUMENT';
}