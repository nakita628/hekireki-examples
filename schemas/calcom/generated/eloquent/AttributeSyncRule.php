<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AttributeSyncRule extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AttributeSyncRule';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'integrationAttributeSyncId',
        'rule',
    ];

    protected $casts = [
        'rule' => 'array',
    ];

    public function integrationAttributeSync(): BelongsTo
    {
        return $this->belongsTo(IntegrationAttributeSync::class, 'integrationAttributeSyncId');
    }
}