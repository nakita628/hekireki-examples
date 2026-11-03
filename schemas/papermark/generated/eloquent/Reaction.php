<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Reaction extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Reaction';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'viewId',
        'pageNumber',
        'type',
    ];

    protected $casts = [
        'pageNumber' => 'integer',
    ];

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }
}