<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ErrorRateLimit extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['bucket'];

    protected $table = 'error_rate_limit';

    protected $primaryKey = 'website_id';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'website_id',
        'bucket',
        'count',
    ];

    protected $casts = [
        'bucket' => 'datetime',
        'count' => 'integer',
    ];
}