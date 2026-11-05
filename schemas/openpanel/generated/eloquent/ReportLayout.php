<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ReportLayout extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'report_layouts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'reportId',
        'gridColumns',
        'x',
        'y',
        'w',
        'h',
        'minW',
        'minH',
        'maxW',
        'maxH',
    ];

    protected $attributes = [
        'gridColumns' => 12,
        'x' => 0,
        'y' => 0,
        'w' => 8,
        'h' => 6,
        'minW' => 4,
        'minH' => 4,
    ];

    protected $casts = [
        'gridColumns' => 'integer',
        'x' => 'integer',
        'y' => 'integer',
        'w' => 'integer',
        'h' => 'integer',
        'minW' => 'integer',
        'minH' => 'integer',
        'maxW' => 'integer',
        'maxH' => 'integer',
    ];

    public function report(): BelongsTo
    {
        return $this->belongsTo(Report::class, 'reportId');
    }
}