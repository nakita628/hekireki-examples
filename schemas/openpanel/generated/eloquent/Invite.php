<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Invite extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'invites';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'email',
        'createdById',
        'organizationId',
        'projectAccess',
        'projectScope',
        'expiresAt',
        'role',
    ];

    protected $attributes = [
        'projectAccess' => '[]',
        'projectScope' => 'all',
    ];

    protected $casts = [
        'projectAccess' => 'array',
        'projectScope' => ProjectScope::class,
        'expiresAt' => 'datetime',
    ];

    public function createdBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdById');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}