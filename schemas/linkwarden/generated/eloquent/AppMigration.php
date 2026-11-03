<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class AppMigration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'AppMigration';

    protected $fillable = [
        'name',
        'status',
        'finishedAt',
    ];

    protected $casts = [
        'status' => AppMigrationStatus::class,
        'finishedAt' => 'datetime',
    ];
}