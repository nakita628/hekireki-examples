<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Tag extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'tag';

    protected $fillable = [
        'name',
        'icon',
        'parent',
        'accountId',
        'sortOrder',
    ];

    protected $attributes = [
        'name' => '',
        'icon' => '',
        'parent' => 0,
        'sortOrder' => 0,
    ];

    protected $casts = [
        'parent' => 'integer',
        'accountId' => 'integer',
        'sortOrder' => 'integer',
    ];

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }

    public function tagsToNote(): HasMany
    {
        return $this->hasMany(TagsToNote::class, 'tagId');
    }
}