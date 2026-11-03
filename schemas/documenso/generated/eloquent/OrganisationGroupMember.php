<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrganisationGroupMember extends Model
{
    protected $table = 'OrganisationGroupMember';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'groupId',
        'organisationMemberId',
    ];

    public function group(): BelongsTo
    {
        return $this->belongsTo(OrganisationGroup::class, 'groupId');
    }

    public function organisationMember(): BelongsTo
    {
        return $this->belongsTo(OrganisationMember::class, 'organisationMemberId');
    }
}