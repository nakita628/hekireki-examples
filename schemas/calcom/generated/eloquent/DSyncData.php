<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DSyncData extends Model
{
    protected $table = 'DSyncData';

    public $timestamps = false;

    protected $fillable = [
        'directoryId',
        'tenant',
        'organizationId',
    ];

    protected $casts = [
        'organizationId' => 'integer',
    ];

    public function org(): BelongsTo
    {
        return $this->belongsTo(OrganizationSettings::class, 'organizationId', 'organizationId');
    }

    public function teamGroupMapping(): HasMany
    {
        return $this->hasMany(DSyncTeamGroupMapping::class, 'directoryId', 'directoryId');
    }

    public function createdAttributeToUsers(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'createdByDSyncId', 'directoryId');
    }

    public function updatedAttributeToUsers(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'updatedByDSyncId', 'directoryId');
    }
}