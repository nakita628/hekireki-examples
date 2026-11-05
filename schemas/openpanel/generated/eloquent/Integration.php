<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Integration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'integrations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'config',
        'organizationId',
        'projectId',
    ];

    protected $casts = [
        'config' => 'array',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function notifications(): HasMany
    {
        return $this->hasMany(Notification::class, 'integrationId');
    }

    public function exportWatermarks(): HasMany
    {
        return $this->hasMany(ExportWatermark::class, 'integrationId');
    }

    public function notificationRules(): BelongsToMany
    {
        return $this->belongsToMany(NotificationRule::class, '_IntegrationToNotificationRule', 'A', 'B');
    }
}