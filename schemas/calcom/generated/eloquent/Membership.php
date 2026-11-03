<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Membership extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Membership';

    protected $fillable = [
        'teamId',
        'userId',
        'accepted',
        'role',
        'customRoleId',
    ];

    protected $attributes = [
        'accepted' => false,
    ];

    protected $casts = [
        'teamId' => 'integer',
        'userId' => 'integer',
        'accepted' => 'boolean',
        'role' => MembershipRole::class,
    ];

    public function customRole(): BelongsTo
    {
        return $this->belongsTo(Role::class, 'customRoleId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function AttributeToUser(): HasMany
    {
        return $this->hasMany(AttributeToUser::class, 'memberId');
    }

    public function Host(): HasMany
    {
        return $this->hasMany(Host::class, 'memberId');
    }
}