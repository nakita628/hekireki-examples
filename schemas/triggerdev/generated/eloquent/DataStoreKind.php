<?php

namespace App\Models;

enum DataStoreKind: string
{
    case CLICKHOUSE = 'CLICKHOUSE';
}