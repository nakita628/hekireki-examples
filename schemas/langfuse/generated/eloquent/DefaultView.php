<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DefaultView extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'default_views';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'user_id',
        'view_name',
        'view_id',
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