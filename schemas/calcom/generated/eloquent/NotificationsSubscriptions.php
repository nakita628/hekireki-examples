<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NotificationsSubscriptions extends Model
{
    protected $table = 'NotificationsSubscriptions';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'subscription',
    ];

    protected $casts = [
        'userId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}