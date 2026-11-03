<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Sheet extends Model
{
    use PrismaDates;

    protected $table = 'sheets';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'space_id',
        'host_id',
        'title',
        'description',
        'url_id',
        'deleted',
        'deleted_at',
    ];

    protected $attributes = [
        'deleted' => false,
    ];

    protected $casts = [
        'deleted' => 'boolean',
        'deleted_at' => 'datetime',
    ];

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function host(): BelongsTo
    {
        return $this->belongsTo(User::class, 'host_id');
    }

    public function slots(): HasMany
    {
        return $this->hasMany(SheetSlot::class, 'sheet_id');
    }
}