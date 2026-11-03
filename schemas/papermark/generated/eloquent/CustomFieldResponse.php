<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CustomFieldResponse extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CustomFieldResponse';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'data',
        'viewId',
    ];

    protected $casts = [
        'data' => 'array',
    ];

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }
}