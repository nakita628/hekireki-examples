<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Project extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'projects';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'organizationId',
        'eventsCount',
        'types',
        'domain',
        'cors',
        'crossDomain',
        'allowUnsafeRevenueTracking',
        'aiStoreContent',
        'filters',
        'firstEventAt',
        'noDataNotifiedAt',
        'dataStoppedNotifiedAt',
        'deleteAt',
    ];

    protected $attributes = [
        'eventsCount' => 0,
        'crossDomain' => false,
        'allowUnsafeRevenueTracking' => false,
        'aiStoreContent' => true,
        'filters' => '[]',
    ];

    protected $casts = [
        'eventsCount' => 'integer',
        'crossDomain' => 'boolean',
        'allowUnsafeRevenueTracking' => 'boolean',
        'aiStoreContent' => 'boolean',
        'filters' => 'array',
        'firstEventAt' => 'datetime',
        'noDataNotifiedAt' => 'datetime',
        'dataStoppedNotifiedAt' => 'datetime',
        'deleteAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function share(): HasOne
    {
        return $this->hasOne(ShareOverview::class, 'projectId');
    }

    public function gscConnection(): HasOne
    {
        return $this->hasOne(GscConnection::class, 'projectId');
    }

    public function clients(): HasMany
    {
        return $this->hasMany(Client::class, 'projectId');
    }

    public function reports(): HasMany
    {
        return $this->hasMany(Report::class, 'projectId');
    }

    public function dashboards(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'projectId');
    }

    public function shareDashboards(): HasMany
    {
        return $this->hasMany(ShareDashboard::class, 'projectId');
    }

    public function shareReports(): HasMany
    {
        return $this->hasMany(ShareReport::class, 'projectId');
    }

    public function shareWidgets(): HasMany
    {
        return $this->hasMany(ShareWidget::class, 'projectId');
    }

    public function meta(): HasMany
    {
        return $this->hasMany(EventMeta::class, 'projectId');
    }

    public function customEvents(): HasMany
    {
        return $this->hasMany(CustomEvent::class, 'projectId');
    }

    public function references(): HasMany
    {
        return $this->hasMany(Reference::class, 'projectId');
    }

    public function memberProjects(): HasMany
    {
        return $this->hasMany(MemberProject::class, 'projectId');
    }

    public function notificationRules(): HasMany
    {
        return $this->hasMany(NotificationRule::class, 'projectId');
    }

    public function notifications(): HasMany
    {
        return $this->hasMany(Notification::class, 'projectId');
    }

    public function imports(): HasMany
    {
        return $this->hasMany(Import::class, 'projectId');
    }

    public function cohorts(): HasMany
    {
        return $this->hasMany(Cohort::class, 'projectId');
    }

    public function exportWatermarks(): HasMany
    {
        return $this->hasMany(ExportWatermark::class, 'projectId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(Integration::class, 'projectId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'projectId');
    }
}