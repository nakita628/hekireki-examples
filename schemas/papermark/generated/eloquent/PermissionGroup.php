<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class PermissionGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PermissionGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'description',
        'dataroomId',
        'teamId',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'permissionGroupId');
    }

    public function accessControls(): HasMany
    {
        return $this->hasMany(PermissionGroupAccessControls::class, 'groupId');
    }
}