<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class SecretStore extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SecretStore';

    protected $primaryKey = 'key';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'key',
        'value',
        'version',
    ];

    protected $attributes = [
        'version' => '1',
    ];

    protected $casts = [
        'value' => 'array',
    ];
}