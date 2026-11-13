<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Passkey extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'passkeys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'credential_id',
        'public_key',
        'counter',
        'transports',
        'last_used_at',
        'user_id',
    ];

    protected $attributes = [
        'counter' => 0,
    ];

    protected $casts = [
        'credential_id' => AsBytes::class,
        'public_key' => AsBytes::class,
        'counter' => 'integer',
        'last_used_at' => 'datetime',
        'user_id' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}