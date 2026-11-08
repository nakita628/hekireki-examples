<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Role extends Model
{
    use HasVersion4Uuids;

    protected $table = 'Role';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'role',
    ];

    protected $casts = [
        'role' => RoleTypes::class,
    ];

    public function user(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_RoleToUser', 'A', 'B');
    }
}