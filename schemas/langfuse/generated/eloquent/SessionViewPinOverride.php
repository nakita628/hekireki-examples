<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SessionViewPinOverride extends \Illuminate\Database\Eloquent\Model
{
    protected $table = 'session_view_pin_overrides';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'project_id',
        'user_id',
        'view_id',
        'is_pinned',
        'position',
    ];

    protected $attributes = [
        'is_pinned' => true,
    ];

    protected $casts = [
        'is_pinned' => 'boolean',
        'position' => 'integer',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}