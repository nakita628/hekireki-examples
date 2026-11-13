<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CaptureGrant extends Model
{
    protected $table = 'capture_grants';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'kind',
        'client_name',
        'secret_hash',
        'created_at',
        'expires_at',
        'last_used_at',
        'revoked_at',
        'client_id',
        'resource',
        'refresh_hash',
        'prev_refresh_hash',
        'rotated_at',
        'refresh_expires_at',
    ];

    protected $casts = [
        'user_id' => 'integer',
        'created_at' => 'integer',
        'expires_at' => 'integer',
        'last_used_at' => 'integer',
        'revoked_at' => 'integer',
        'rotated_at' => 'integer',
        'refresh_expires_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}