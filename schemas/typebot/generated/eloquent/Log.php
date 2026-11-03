<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Log extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Log';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'resultId',
        'status',
        'context',
        'description',
        'details',
    ];

    public function result(): BelongsTo
    {
        return $this->belongsTo(Result::class, 'resultId');
    }
}