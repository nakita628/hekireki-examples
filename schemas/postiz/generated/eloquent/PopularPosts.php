<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class PopularPosts extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'PopularPosts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'category',
        'topic',
        'content',
        'hook',
    ];
}