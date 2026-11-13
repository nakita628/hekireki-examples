<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ConnectRequest extends Model
{
    protected $table = 'connect_requests';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'poll_secret_hash',
        'match_code',
        'device_name',
        'platform',
        'status',
        'user_id',
        'token_version',
        'expires_at',
    ];

    protected $attributes = [
        'status' => 'pending',
    ];

    protected $casts = [
        'user_id' => 'integer',
        'token_version' => 'integer',
        'expires_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}