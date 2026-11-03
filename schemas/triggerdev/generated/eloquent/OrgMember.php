<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OrgMember extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrgMember';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'organizationId',
        'userId',
        'role',
    ];

    protected $attributes = [
        'role' => 'MEMBER',
    ];

    protected $casts = [
        'role' => OrgMemberRole::class,
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function environments(): HasMany
    {
        return $this->hasMany(RuntimeEnvironment::class, 'orgMemberId');
    }
}