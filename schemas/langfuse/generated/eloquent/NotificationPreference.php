<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NotificationPreference extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'notification_preferences';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'project_id',
        'channel',
        'type',
        'enabled',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'channel' => NotificationChannel::class,
        'type' => NotificationType::class,
        'enabled' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}