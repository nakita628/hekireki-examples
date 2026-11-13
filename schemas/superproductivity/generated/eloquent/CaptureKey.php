<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CaptureKey extends Model
{
    protected $table = 'capture_keys';

    protected $primaryKey = 'user_id';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'public_key',
        'fingerprint',
        'created_at',
        'disabled_at',
    ];

    protected $casts = [
        'created_at' => 'integer',
        'disabled_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}