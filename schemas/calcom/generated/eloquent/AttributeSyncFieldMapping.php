<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AttributeSyncFieldMapping extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'AttributeSyncFieldMapping';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'integrationFieldName',
        'attributeId',
        'enabled',
        'integrationAttributeSyncId',
    ];

    protected $casts = [
        'enabled' => 'boolean',
    ];

    public function attribute(): BelongsTo
    {
        return $this->belongsTo(Attribute::class, 'attributeId');
    }

    public function integrationAttributeSync(): BelongsTo
    {
        return $this->belongsTo(IntegrationAttributeSync::class, 'integrationAttributeSyncId');
    }
}