<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class Trending extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Trending';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'trendingList',
        'language',
        'hash',
        'date',
    ];

    protected $casts = [
        'date' => 'datetime',
    ];
}