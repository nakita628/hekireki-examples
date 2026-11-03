<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Board extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'board';

    protected $primaryKey = 'board_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'board_id',
        'type',
        'name',
        'description',
        'parameters',
        'user_id',
        'team_id',
    ];

    protected $casts = [
        'parameters' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'team_id');
    }
}