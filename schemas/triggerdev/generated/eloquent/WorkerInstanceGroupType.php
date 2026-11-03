<?php

namespace App\Models;

enum WorkerInstanceGroupType: string
{
    case MANAGED = 'MANAGED';
    case UNMANAGED = 'UNMANAGED';
}