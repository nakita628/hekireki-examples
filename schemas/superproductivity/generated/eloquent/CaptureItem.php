<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CaptureItem extends Model
{
    protected $table = 'capture_items';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'grant_id',
        'ciphertext',
        'task_id',
        'created_at',
        'lease_device_id',
        'lease_until',
    ];

    protected $casts = [
        'user_id' => 'integer',
        'created_at' => 'integer',
        'lease_until' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}