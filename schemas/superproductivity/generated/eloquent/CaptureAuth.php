<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CaptureAuth extends Model
{
    protected $table = 'capture_auth';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'client_id',
        'client_name',
        'redirect_uri',
        'code_challenge',
        'resource',
        'state',
        'scope',
        'code_hash',
        'token_version',
        'expires_at',
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