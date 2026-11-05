<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Tags extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'tags';

    protected $fillable = [
        'uuid',
        'userId',
        'name',
        'editedAt',
    ];

    protected $casts = [
        'userId' => 'integer',
        'editedAt' => 'datetime',
    ];

    public function files(): BelongsToMany
    {
        return $this->belongsToMany(Files::class, '_filesTotags', 'B', 'A');
    }
}