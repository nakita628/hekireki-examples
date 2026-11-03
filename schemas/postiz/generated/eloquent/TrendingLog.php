<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class TrendingLog extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    protected $table = 'TrendingLog';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'language',
        'date',
    ];

    protected $casts = [
        'date' => 'datetime',
    ];
}