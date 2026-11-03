<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Role extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Role';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'color',
        'description',
        'teamId',
        'type',
    ];

    protected $attributes = [
        'type' => 'CUSTOM',
    ];

    protected $casts = [
        'teamId' => 'integer',
        'type' => RoleType::class,
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function permissions(): HasMany
    {
        return $this->hasMany(RolePermission::class, 'roleId');
    }

    public function memberships(): HasMany
    {
        return $this->hasMany(Membership::class, 'customRoleId');
    }
}