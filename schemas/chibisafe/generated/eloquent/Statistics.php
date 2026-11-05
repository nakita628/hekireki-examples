<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Statistics extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'statistics';

    protected $fillable = [
        'batchId',
        'type',
        'data',
    ];

    protected $casts = [
        'batchId' => 'integer',
    ];
}