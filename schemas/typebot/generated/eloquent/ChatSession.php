<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ChatSession extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ChatSession';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'state',
        'isReplying',
    ];

    protected $casts = [
        'state' => 'array',
        'isReplying' => 'boolean',
    ];
}