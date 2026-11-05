<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Client extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'clients';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'secret',
        'type',
        'projectId',
        'organizationId',
        'ignoreCorsAndSecret',
    ];

    protected $attributes = [
        'type' => 'write',
        'ignoreCorsAndSecret' => false,
    ];

    protected $casts = [
        'type' => ClientType::class,
        'ignoreCorsAndSecret' => 'boolean',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }
}