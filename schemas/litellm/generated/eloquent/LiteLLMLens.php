<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class LiteLLMLens extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_Lens';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'version',
        'data',
        'due_at',
    ];

    protected $attributes = [
        'version' => 0,
    ];

    protected $casts = [
        'version' => 'integer',
        'data' => 'array',
        'due_at' => 'datetime',
    ];

    public function reviews(): HasMany
    {
        return $this->hasMany(LiteLLMLensReview::class, 'lens_id');
    }
}