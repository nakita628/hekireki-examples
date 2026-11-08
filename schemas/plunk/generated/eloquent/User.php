<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'users';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'email',
        'password',
        'type',
        'emailVerified',
    ];

    protected $attributes = [
        'emailVerified' => false,
    ];

    protected $casts = [
        'type' => AuthMethod::class,
        'emailVerified' => 'boolean',
    ];

    public function memberships(): HasMany
    {
        return $this->hasMany(Membership::class, 'userId');
    }
}