<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DashboardSection extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DashboardSection';

    protected $fillable = [
        'userId',
        'collectionId',
        'type',
        'order',
    ];

    protected $casts = [
        'userId' => 'integer',
        'collectionId' => 'integer',
        'type' => DashboardSectionType::class,
        'order' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function collection(): BelongsTo
    {
        return $this->belongsTo(Collection::class, 'collectionId');
    }
}