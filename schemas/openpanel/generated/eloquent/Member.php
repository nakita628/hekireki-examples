<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Member extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'members';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'role',
        'email',
        'userId',
        'invitedById',
        'organizationId',
        'projectScope',
        'meta',
    ];

    protected $attributes = [
        'projectScope' => 'all',
    ];

    protected $casts = [
        'projectScope' => ProjectScope::class,
        'meta' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function invitedBy(): BelongsTo
    {
        return $this->belongsTo(User::class, 'invitedById');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function projects(): HasMany
    {
        return $this->hasMany(MemberProject::class, 'memberId');
    }
}