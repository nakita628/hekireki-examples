<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TwoFactorAuth extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'two_factor_auth';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'secret',
        'is_enabled',
    ];

    protected $attributes = [
        'is_enabled' => false,
    ];

    protected $casts = [
        'is_enabled' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}