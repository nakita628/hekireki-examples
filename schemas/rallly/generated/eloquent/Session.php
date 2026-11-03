<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Session extends Model
{
    use PrismaDates;

    protected $table = 'sessions';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'expires_at',
        'token',
        'ip_address',
        'user_agent',
        'user_id',
        'impersonated_by',
    ];

    protected $casts = [
        'expires_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function impersonatedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'impersonated_by');
    }
}