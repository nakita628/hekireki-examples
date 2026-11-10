<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Task extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Task';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'description',
        'time',
        'isDone',
    ];

    protected $attributes = [
        'time' => '1',
        'isDone' => false,
    ];

    protected $casts = [
        'isDone' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}