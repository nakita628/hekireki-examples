<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Session extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'Session';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'session_token',
        'user_id',
        'expires',
    ];

    protected $casts = [
        'expires' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}