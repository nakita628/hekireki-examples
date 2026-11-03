<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TwoFactorRateLimit extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const ZONED_DATES = ['locked_until', 'updated_at'];

    protected $table = 'two_factor_rate_limit';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'attempts',
        'locked_until',
    ];

    protected $attributes = [
        'attempts' => 0,
    ];

    protected $casts = [
        'attempts' => 'integer',
        'locked_until' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}