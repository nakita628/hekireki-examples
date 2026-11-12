<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Attachments extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAt', 'updatedAt'];

    protected $table = 'attachments';

    protected $fillable = [
        'isShare',
        'sharePassword',
        'name',
        'path',
        'size',
        'type',
        'noteId',
        'accountId',
        'sortOrder',
        'perfixPath',
        'depth',
        'metadata',
    ];

    protected $attributes = [
        'isShare' => false,
        'sharePassword' => '',
        'name' => '',
        'path' => '',
        'size' => '0',
        'type' => '',
        'sortOrder' => 0,
        'perfixPath' => '',
    ];

    protected $casts = [
        'isShare' => 'boolean',
        'size' => AsDecimal::class,
        'noteId' => 'integer',
        'accountId' => 'integer',
        'sortOrder' => 'integer',
        'depth' => 'integer',
        'metadata' => 'array',
    ];

    public function note(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'noteId');
    }

    public function account(): BelongsTo
    {
        return $this->belongsTo(Accounts::class, 'accountId');
    }
}