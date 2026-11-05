<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class NotificationRule extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'notification_rules';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'projectId',
        'sendToApp',
        'sendToEmail',
        'config',
        'template',
    ];

    protected $attributes = [
        'sendToApp' => false,
        'sendToEmail' => false,
    ];

    protected $casts = [
        'sendToApp' => 'boolean',
        'sendToEmail' => 'boolean',
        'config' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function notifications(): HasMany
    {
        return $this->hasMany(Notification::class, 'notificationRuleId');
    }

    public function integrations(): BelongsToMany
    {
        return $this->belongsToMany(Integration::class, '_IntegrationToNotificationRule', 'B', 'A');
    }
}