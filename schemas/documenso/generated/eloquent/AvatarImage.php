<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class AvatarImage extends Model
{
    protected $table = 'AvatarImage';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'bytes',
    ];

    public function team(): HasMany
    {
        return $this->hasMany(Team::class, 'avatarImageId');
    }

    public function user(): HasMany
    {
        return $this->hasMany(User::class, 'avatarImageId');
    }

    public function organisation(): HasMany
    {
        return $this->hasMany(Organisation::class, 'avatarImageId');
    }
}