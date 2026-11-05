<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Notification extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'notifications';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'title',
        'message',
        'isReadAt',
        'sendToApp',
        'sendToEmail',
        'integrationId',
        'notificationRuleId',
        'payload',
    ];

    protected $attributes = [
        'sendToApp' => false,
        'sendToEmail' => false,
    ];

    protected $casts = [
        'isReadAt' => 'datetime',
        'sendToApp' => 'boolean',
        'sendToEmail' => 'boolean',
        'payload' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function notificationRule(): BelongsTo
    {
        return $this->belongsTo(NotificationRule::class, 'notificationRuleId');
    }
}