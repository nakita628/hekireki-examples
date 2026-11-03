<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Website extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['reset_at', 'created_at', 'updated_at', 'deleted_at'];

    protected $table = 'website';

    protected $primaryKey = 'website_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'website_id',
        'name',
        'domain',
        'reset_at',
        'user_id',
        'team_id',
        'created_by',
        'deleted_at',
        'recorder_enabled',
        'replay_config',
    ];

    protected $attributes = [
        'recorder_enabled' => false,
    ];

    protected $casts = [
        'reset_at' => 'datetime',
        'deleted_at' => 'datetime',
        'recorder_enabled' => 'boolean',
        'replay_config' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function createUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'team_id');
    }

    public function annotations(): HasMany
    {
        return $this->hasMany(Annotation::class, 'website_id');
    }

    public function eventData(): HasMany
    {
        return $this->hasMany(EventData::class, 'website_id');
    }

    public function reports(): HasMany
    {
        return $this->hasMany(Report::class, 'website_id');
    }

    public function revenue(): HasMany
    {
        return $this->hasMany(Revenue::class, 'website_id');
    }

    public function segments(): HasMany
    {
        return $this->hasMany(Segment::class, 'website_id');
    }

    public function sessionData(): HasMany
    {
        return $this->hasMany(SessionData::class, 'website_id');
    }

    public function sessionReplays(): HasMany
    {
        return $this->hasMany(SessionReplay::class, 'website_id');
    }

    public function sessionReplaysSaved(): HasMany
    {
        return $this->hasMany(SessionReplaySaved::class, 'website_id');
    }

    public function heatmapEvents(): HasMany
    {
        return $this->hasMany(HeatmapEvent::class, 'website_id');
    }
}