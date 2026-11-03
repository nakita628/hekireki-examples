<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserNotificationPreferences extends Model
{
    protected $table = 'UserNotificationPreferences';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'dubLinks',
    ];

    protected $attributes = [
        'dubLinks' => true,
    ];

    protected $casts = [
        'dubLinks' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}