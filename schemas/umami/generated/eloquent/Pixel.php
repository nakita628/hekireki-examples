<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Pixel extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at', 'deleted_at'];

    protected $table = 'pixel';

    protected $primaryKey = 'pixel_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'pixel_id',
        'name',
        'slug',
        'user_id',
        'team_id',
        'deleted_at',
    ];

    protected $casts = [
        'deleted_at' => 'datetime',
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