<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrganisationGroup extends Model
{
    protected $table = 'OrganisationGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'name',
        'type',
        'organisationRole',
        'organisationId',
    ];

    protected $casts = [
        'type' => OrganisationGroupType::class,
        'organisationRole' => OrganisationMemberRole::class,
    ];

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }

    public function organisationGroupMembers(): HasMany
    {
        return $this->hasMany(OrganisationGroupMember::class, 'groupId');
    }

    public function teamGroups(): HasMany
    {
        return $this->hasMany(TeamGroup::class, 'organisationGroupId');
    }
}