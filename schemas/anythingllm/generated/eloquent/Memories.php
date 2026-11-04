<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Memories extends Model
{
    use PrismaDates;

    protected $table = 'memories';

    protected $fillable = [
        'user_id',
        'workspace_id',
        'scope',
        'content',
        'last_used_at',
    ];

    protected $attributes = [
        'scope' => 'workspace',
    ];

    protected $casts = [
        'user_id' => 'integer',
        'workspace_id' => 'integer',
        'last_used_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspaces::class, 'workspace_id');
    }
}