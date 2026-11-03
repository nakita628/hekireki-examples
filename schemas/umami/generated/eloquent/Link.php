<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Link extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at', 'deleted_at'];

    protected $table = 'link';

    protected $primaryKey = 'link_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'link_id',
        'name',
        'url',
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