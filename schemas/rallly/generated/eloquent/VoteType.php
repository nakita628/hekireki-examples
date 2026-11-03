<?php

namespace App\Models;

enum VoteType: string
{
    case yes = 'yes';
    case no = 'no';
    case ifNeedBe = 'ifNeedBe';
}