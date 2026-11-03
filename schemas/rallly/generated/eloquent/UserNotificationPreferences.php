<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserNotificationPreferences extends Model
{
    use PrismaDates;

    protected $table = 'user_notification_preferences';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'prefs',
    ];

    protected $attributes = [
        'prefs' => '{}',
    ];

    protected $casts = [
        'prefs' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}