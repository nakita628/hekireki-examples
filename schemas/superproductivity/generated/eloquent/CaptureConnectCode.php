<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CaptureConnectCode extends Model
{
    protected $table = 'capture_connect_codes';

    protected $primaryKey = 'user_id';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'code_hash',
        'token_version',
        'expires_at',
    ];

    protected $casts = [
        'token_version' => 'integer',
        'expires_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}