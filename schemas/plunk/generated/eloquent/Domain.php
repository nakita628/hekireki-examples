<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Domain extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'domains';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'domain',
        'verified',
        'dkimTokens',
        'projectId',
    ];

    protected $attributes = [
        'verified' => false,
    ];

    protected $casts = [
        'verified' => 'boolean',
        'dkimTokens' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }
}