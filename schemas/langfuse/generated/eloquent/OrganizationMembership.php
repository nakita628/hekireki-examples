<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrganizationMembership extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'organization_memberships';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'org_id',
        'user_id',
        'role',
    ];

    protected $casts = [
        'role' => Role::class,
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function ProjectMemberships(): HasMany
    {
        return $this->hasMany(ProjectMembership::class, 'org_membership_id');
    }
}