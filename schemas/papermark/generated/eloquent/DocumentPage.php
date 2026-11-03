<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DocumentPage extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DocumentPage';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'versionId',
        'pageNumber',
        'embeddedLinks',
        'pageLinks',
        'metadata',
        'file',
        'storageType',
    ];

    protected $attributes = [
        'storageType' => 'VERCEL_BLOB',
    ];

    protected $casts = [
        'pageNumber' => 'integer',
        'pageLinks' => 'array',
        'metadata' => 'array',
        'storageType' => DocumentStorageType::class,
    ];

    public function version(): BelongsTo
    {
        return $this->belongsTo(DocumentVersion::class, 'versionId');
    }
}