<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NotificationPreference extends Model
{
    protected $table = 'NotificationPreference';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'projectUserId',
        'linkUsageSummary',
        'domainConfigurationUpdates',
    ];

    protected $attributes = [
        'linkUsageSummary' => true,
        'domainConfigurationUpdates' => true,
    ];

    protected $casts = [
        'linkUsageSummary' => 'boolean',
        'domainConfigurationUpdates' => 'boolean',
    ];

    public function projectUser(): BelongsTo
    {
        return $this->belongsTo(ProjectUsers::class, 'projectUserId');
    }
}