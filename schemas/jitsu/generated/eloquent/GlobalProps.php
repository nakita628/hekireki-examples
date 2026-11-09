<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class GlobalProps extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'GlobalProps';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'value',
    ];

    protected $casts = [
        'value' => 'array',
    ];
}