<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class User extends Model
{
    use PrismaDates;

    protected $table = 'User';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'emailVerified',
        'image',
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function sessions(): HasMany
    {
        return $this->hasMany(Session::class, 'userId');
    }

    public function Authenticator(): HasMany
    {
        return $this->hasMany(Authenticator::class, 'userId');
    }
}