<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamGroup extends Model
{
    protected $table = 'TeamGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'organisationGroupId',
        'teamRole',
        'teamId',
    ];

    protected $casts = [
        'teamRole' => TeamMemberRole::class,
        'teamId' => 'integer',
    ];

    public function organisationGroup(): BelongsTo
    {
        return $this->belongsTo(OrganisationGroup::class, 'organisationGroupId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}