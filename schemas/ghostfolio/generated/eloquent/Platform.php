<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Platform extends Model
{
    use HasVersion4Uuids;

    protected $table = 'Platform';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'name',
        'url',
    ];

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'platformId');
    }
}