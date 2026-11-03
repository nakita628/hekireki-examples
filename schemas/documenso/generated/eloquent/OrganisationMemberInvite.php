<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrganisationMemberInvite extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'OrganisationMemberInvite';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'token',
        'status',
        'organisationId',
        'organisationRole',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'status' => OrganisationMemberInviteStatus::class,
        'organisationRole' => OrganisationMemberRole::class,
    ];

    public function organisation(): BelongsTo
    {
        return $this->belongsTo(Organisation::class, 'organisationId');
    }
}