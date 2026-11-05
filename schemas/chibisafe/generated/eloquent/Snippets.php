<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Snippets extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'snippets';

    protected $fillable = [
        'uuid',
        'userId',
        'identifier',
        'name',
        'description',
        'content',
        'language',
        'parentUuid',
        'editedAt',
    ];

    protected $casts = [
        'userId' => 'integer',
        'editedAt' => 'datetime',
    ];
}