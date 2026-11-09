<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class InvitationToken extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'InvitationToken';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workspaceId',
        'email',
        'token',
        'usedBy',
        'role',
    ];

    protected $attributes = [
        'role' => 'owner',
    ];

    protected $casts = [
        'role' => WorkspaceRole::class,
    ];
}