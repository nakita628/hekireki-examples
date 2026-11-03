<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class DataMigration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataMigration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'completedAt',
    ];

    protected $casts = [
        'completedAt' => 'datetime',
    ];
}