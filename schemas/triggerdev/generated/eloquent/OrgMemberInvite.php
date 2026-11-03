<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OrgMemberInvite extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrgMemberInvite';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'token',
        'email',
        'role',
        'rbacRoleId',
        'organizationId',
        'inviterId',
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

    public function inviter(): BelongsTo
    {
        return $this->belongsTo(User::class, 'inviterId');
    }
}