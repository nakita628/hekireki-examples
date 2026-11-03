<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Tag extends Model
{
    use HasVersion4Uuids;

    protected $table = 'Tag';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'name',
        'userId',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(TagsOnAccounts::class, 'tagId');
    }

    public function activities(): BelongsToMany
    {
        return $this->belongsToMany(Order::class, '_OrderToTag', 'B', 'A');
    }
}